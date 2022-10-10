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

namespace DigitalMineServer.ParseMessage
{
    //用户端数据处理软件上传文件
    internal class MonitorFileMessage
    {
        private readonly MySqlHelper mysql = new MySqlHelper();

        //文件真实路径
        private readonly string FilePath = ConfigurationManager.AppSettings["FilePath"] + "Monitor/";

        //文件对外虚拟路径
        private readonly string VritualPath = ConfigurationManager.AppSettings["VritualPath"] + "Monitor/";

        public void ParseOrder(MonitorFileSession Session, byte[] buffer)
        {
            //判断是否接受了下位机上传的文件信息
            if (!Session.HasHeader)
            {
                string[] info = Encoding.UTF8.GetString(buffer).Split('!');
                Session.Company = info[0];
                Session.FileName = info[1];
                Session.md5Name = Utils.Util.GetMd5(info[1].Split('.')[0]);
                Session.RealFilePath = FilePath + Utils.Util.GetChsSpell(info[0]);
                Session.VritualPath = VritualPath + Utils.Util.GetChsSpell(info[0]) + '/';
                Session.TotalSize = int.Parse(info[2]);
                Session.ReceSize = 0;
                Session.FileType = "pic";
                if (Utils.Util.DirExit(Session.RealFilePath, true))
                {
                    Session.fs = Utils.Util.FileCreat(Session.RealFilePath + '/' + Session.md5Name + ".jpg");
                    Session.HasHeader = true;
                }
                else
                {
                    Session.Close();
                }
            }
            else
            {
                //文件数据流暂存
                Session.FileByteList.Add(buffer);
                //更改接收体积
                Session.ReceSize += buffer.Length;
                //检查文件体积与已经接收的体积
                if (Session.ReceSize == Session.TotalSize)
                {
                    if (Session.fs != null)
                    {
                        try
                        {
                            foreach (var iten in Session.FileByteList)
                            {
                                Session.fs.Write(iten, 0, iten.Length);
                            }
                            Session.fs.Close();
                            string md5Name = Session.md5Name + ".jpg";
                            //真实完整路径
                            string path = Session.RealFilePath + "/" + md5Name;
                            //对外虚拟完整路径
                            string vritualPath = Session.VritualPath + md5Name;
                            //更新信息写入数据库
                            string sql = "select COUNT(ID) as Count from list_monitor_file where COMPANY='" + Session.Company + "' and FILENAME='" + Session.FileName + "'";
                            if (mysql.GetCount(sql) == 0)
                            {
                                sql = "select Count(ID) as Count from list_monitor where NAME='" + Session.FileName.Split('.')[0] + "' and COMPANY='" + Session.Company + "'";
                                if (mysql.GetCount(sql) > 0)
                                {
                                    sql = "INSERT INTO `list_monitor_file`(`FILENAME`,`VIRTUALPATH`,`PATH`, `TYPE`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + Session.FileName + "','" + vritualPath + "', '" + path + "', '" + Session.FileType + "', '" + Session.Company + "', '" + DateTime.Now + "', NULL, NULL, NULL, NULL);";
                                    if (mysql.UpdOrInsOrdel(sql) == 0)
                                    {
                                        File.Delete(path);
                                    }
                                }
                                else
                                {
                                    File.Delete(path);
                                }
                            }
                            else
                            {
                                sql = "UPDATE `list_monitor_file` SET `ADD_TIME` = '" + DateTime.Now + "' WHERE COMPANY='" + Session.Company + "' and FILENAME='" + Session.FileName + "'";
                                if (mysql.UpdOrInsOrdel(sql) == 0)
                                {
                                    File.Delete(path);
                                };
                            }
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteLog("文件写入错误", e);
                            File.Delete(Session.RealFilePath + "/" + Session.FileName);
                        }
                        finally
                        {
                            Session.fs.Close();
                            Session.fs.Dispose();
                        }
                    }
                    Session.Close();
                }
            }
        }
    }
}