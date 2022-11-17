using DigitalMineServer.Utils;
using DigitalMineServer.Mysql;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using DigitalMineServer.SuperSocket.SocketSession;
using DigitalMineServer.Util;
using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using JtLibrary;
using JtLibrary.Providers;
using JtLibrary.Structures;
using ActionSafe.AcSafe_Su.Reponse_Su_2013;
using MySqlX.XDevAPI;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;
using DigitalMineServer.Static;
using ActionSafe.AcSafe_Su.PacketBody;
using FileStream = System.IO.FileStream;
using static DigitalMineServer.Structures.Comprehensive;
using static ServiceStack.Script.Lisp;
using DigitalMineServer.Redis;

namespace DigitalMineServer.ParseMessage
{
    //用户端数据处理软件上传文件
    internal class AcSafeFileMessage
    {
        private readonly MySqlHelper mysql = new MySqlHelper();

        private readonly RedisHelper Redis = new RedisHelper();

        //文件真实路径
        private readonly string FilePath = ConfigurationManager.AppSettings["FilePath"] + "AcSafeFile/";

        //文件对外虚拟路径
        private readonly string VritualPath = ConfigurationManager.AppSettings["VritualPath"] + "AcSafeFile/";

        private readonly IPacketProvider pConvert = PacketProvider.CreateProvider();

        /// <summary>
        /// 可存储文件格式
        /// </summary>
        private readonly List<string> allowWrite = new List<string>() { ".mp4", ".jpg", ".jpeg", ".h264", ".png" };

        /// <summary>
        /// 过滤消息数据流
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="buffer"></param>
        public void FilterMessage(AcSafeFileSession Session, byte[] buffer)
        {
            if (buffer[0] == 0x7e)
            {
                DealWithOrder(buffer, Session);
            }
            else
            {
                Session.FileByteList.Add(buffer);
            }
        }

        private void DealWithOrder(byte[] buffer, AcSafeFileSession Session)
        {
            try
            {
                PacketMessage msg = pConvert.Decode(buffer, 0, buffer.Length);
                ValueTuple<string, string, string, string, string, string> val = Redis.GetVehicleList(Extension.BCDToString(msg.pmPacketHead.hSimNumber) + Redis_key_ext.vehicle);
                if (val.Item1 == null)
                {
                    return;
                }
                switch (msg.pmPacketHead.phMessageId)
                {
                    case AcSafeCmd.RSP_1210:
                        PB1210 PB1210 = new REP_1210().Decode(msg.pmMessageBody);
                        Session.WarnId = Utils.Util.GetMd5(Utils.Util.GetStringHex(PB1210.warnId));
                        Dealwith1210(msg, pConvert, Session);
                        break;

                    case AcSafeCmd.RSP_1211:
                        PB1211 Pb1211 = new REP_1211().Decode(msg.pmMessageBody);
                        new REQ_8001_AcSafe().Default(msg, pConvert, Session);
                        Session.FileName = Pb1211.fileName;
                        Session.FileType = Pb1211.type;
                        Session.md5Name = Utils.Util.GetMd5(Pb1211.fileName);
                        Session.Company = val.Item3;
                        Session.RealFilePath = FilePath + Utils.Util.GetChsSpell(Session.Company);
                        Session.VritualPath = VritualPath + Utils.Util.GetChsSpell(Session.Company) + '/';
                        break;

                    case AcSafeCmd.RSP_1212:
                        PB1212 PB1212 = new REP_1212().Decode(msg.pmMessageBody);
                        List<byte[]> list = Session.FileByteList;
                        Session.FileByteList = new List<byte[]>();
                        new REQ_9212().Default(msg, pConvert, new PB9212()
                        {
                            fileNameLength = PB1212.fileNameLength,
                            fileName = PB1212.fileName,
                            type = PB1212.type,
                            result = 0,
                            fillCount = 0,
                            fillStructureList = new List<FillStructure>()
                        }, Session);
                        FileInfo fileInfo = new FileInfo()
                        {
                            FileName = Session.FileName,
                            RealFilePath = Session.RealFilePath,
                            VritualPath = Session.VritualPath,
                            md5Name = Session.md5Name,
                            Company = Session.Company,
                            WarnId = Session.WarnId
                        };
                        ThreadPool.QueueUserWorkItem(WriteFile, new List<object>() { fileInfo, list });
                        break;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 1210信息处理
        /// </summary>
        /// <param name="PB1210"></param>
        public void Dealwith1210(PacketMessage msg, IPacketProvider pConvert, AcSafeFileSession Session)
        {
            string sim = Extension.BCDToString(msg.pmPacketHead.hSimNumber);
            if (!Resource.WarnIdDic.ContainsKey(sim))
            {
                return;
            }
            if (Utils.Util.GetTimeDifference(Resource.WarnIdDic[sim].Item2, DateTime.Now) > 600)
            {
                Resource.WarnIdDic.TryRemove(sim, out _);
                return;
            }
            new REQ_8001_AcSafe().Default(msg, pConvert, Session);
        }

        public void WriteFile(object obj)
        {
            List<object> list = obj as List<object>;
            FileInfo fileInfo = (FileInfo)list[0];
            string suffix = "." + fileInfo.FileName.Split('.')[1];
            if (!allowWrite.Contains(suffix))
            {
                return;
            }
            List<byte[]> BufferList = list[1] as List<byte[]>;
            FileStream FileStream;
            if (Utils.Util.DirExit(fileInfo.RealFilePath, true))
            {
                FileStream = Utils.Util.FileCreat(fileInfo.RealFilePath + '/' + fileInfo.md5Name + suffix);
            }
            else
            {
                return;
            }
            try
            {
                foreach (var iten in BufferList)
                {
                    FileStream.Write(iten, 62, iten.Length - 62);
                }
                FileStream.Close();
                string md5Name = fileInfo.md5Name + suffix;
                //真实完整路径
                string path = fileInfo.RealFilePath + "/" + md5Name;
                //对外虚拟完整路径
                string vritualPath = fileInfo.VritualPath + md5Name;
                //更新信息写入数据库
                string sql = "INSERT INTO `list_acsafe_file`( `WARN_NUMBER`, `UUID`, `FILE_NAME`, `PATHS`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + fileInfo.WarnId + "', '" + fileInfo.md5Name + "', '" + fileInfo.FileName + "', '" + vritualPath + "', '" + fileInfo.Company + "', '" + DateTime.Now.ToString() + "', NULL, NULL, NULL, NULL);";
                if (mysql.UpdOrInsOrdel(sql) == 0)
                {
                    File.Delete(path);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("文件写入错误", e);
                File.Delete(fileInfo.RealFilePath + "/" + fileInfo.md5Name + suffix);
            }
            finally
            {
                FileStream.Close();
                FileStream.Dispose();
            }
        }

        private struct FileInfo
        {
            public string FileName;
            public string RealFilePath;
            public string VritualPath;
            public string md5Name;
            public string Company;
            public string WarnId;
        }
    }
}