using DigitalMineServer.SuperSocket;
using JtLibrary.PacketBody;
using JtLibrary.Structures;
using JtLibrary.Utils;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.Static
{
    public class Resource
    {
        public Resource()
        {
            isVehicleUpdate = false;

            isPersonUpdate = false;

            OriginalDataQueues = new ConcurrentQueue<(byte[], Jt808Session)>();

            OriginalWatchDataQueues = new ConcurrentQueue<(byte[], F10WatchSession)>();

            Body0200Queues = new ConcurrentQueue<PacketMessage>();

            Vehicle0200DataQueues = new ConcurrentQueue<ValueTuple<string, PB0200>>();

            Person0200DataQueues = new ConcurrentQueue<ValueTuple<string, PB0200>>();

            msgSerialnumberDic = new ConcurrentDictionary<ushort, string>();

            WarnIdDic = new ConcurrentDictionary<string, ValueTuple<byte[], DateTime>>();

            ServerIp = ConfigurationManager.AppSettings["ServerIp"];
        }

        /// <summary>
        /// 全局运行标志，false时软件停止状态
        /// </summary>
        public static bool IsActive = true;

        /// <summary>
        ///终端上传原始数据数据队列(原始数据--session)
        /// </summary>
        public static ConcurrentQueue<ValueTuple<byte[], Jt808Session>> OriginalDataQueues;

        /// <summary>
        ///F10智能手表上传原始数据数据队列(原始数据--session)
        /// </summary>
        public static ConcurrentQueue<ValueTuple<byte[], F10WatchSession>> OriginalWatchDataQueues;

        /// <summary>
        ///0200数据包队列
        /// </summary>
        public static ConcurrentQueue<PacketMessage> Body0200Queues;

        /// <summary>
        /// 车辆数据解析后将入库的0200数据队列
        /// </summary>
        public static ConcurrentQueue<ValueTuple<string, PB0200>> Vehicle0200DataQueues;

        /// <summary>
        /// 人员数据解析后将入库的0200数据队列
        /// </summary>
        public static ConcurrentQueue<ValueTuple<string, PB0200>> Person0200DataQueues;

        //是否正在更新车辆信息
        public static bool isVehicleUpdate;

        //是否正在更新人员信息
        public static bool isPersonUpdate;

        /// <summary>
        /// 终端录像查询指令流水号<流水号，终端sim>
        /// </summary>
        public static ConcurrentDictionary<ushort, string> msgSerialnumberDic;

        /// <summary>
        /// 平台分配的主动安全报警唯一标识号
        /// key：sim
        /// item1：平台分配的唯一报警id（32位）
        /// item2：时间
        /// item3:文件信息字典
        /// </summary>
        public static ConcurrentDictionary<string, ValueTuple<byte[], DateTime>> WarnIdDic;

        /// <summary>
        /// 服务器中心IP
        /// </summary>
        public static string ServerIp;
    }
}