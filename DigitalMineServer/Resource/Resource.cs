using DigitalMineServer.SuperSocket;
using JtLibrary.PacketBody;
using JtLibrary.Structures;
using JtLibrary.Utils;
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

            OriginalDataQueues = new ConcurrentQueue<(byte[], Jt808Session)>();

            Body0200Queues = new ConcurrentQueue<PacketMessage>();

            InsertQueues = new ConcurrentQueue<ValueTuple<string, PB0200>>();

            VehicleList = new ConcurrentDictionary<string, (string, string, string, string, string, string)>();

            fenceFanbidInInfo = new ConcurrentDictionary<string, (string, string, string, string, string, List<Point>)>();

            fenceFanbidOutInfo = new ConcurrentDictionary<string, (string, string, string, string, string, List<Point>)>();

            equipVersion = new ConcurrentDictionary<string, (string, string, string, int)>();

            msgSerialnumberDic = new ConcurrentDictionary<ushort, string>();

            WarnIdDic = new ConcurrentDictionary<string, ValueTuple<byte[], DateTime>>();

            ServerIp = ConfigurationManager.AppSettings["ServerIp"];
        }

        /// <summary>
        ///终端上传原始数据数据队列(原始数据--session)
        /// </summary>
        public static ConcurrentQueue<ValueTuple<byte[], Jt808Session>> OriginalDataQueues;

        /// <summary>
        ///0200数据包队列
        /// </summary>
        public static ConcurrentQueue<PacketMessage> Body0200Queues;

        /// <summary>
        /// 解析后将入库的0200队列
        /// </summary>
        public static ConcurrentQueue<ValueTuple<string, PB0200>> InsertQueues;

        /// <summary>
        /// 服务器存储的车辆消息
        /// item1：车辆编号外键
        /// item2：车辆类型
        /// item3：所属公司
        /// item4：超速阈值
        /// item5：车辆编号
        /// item6：司机
        /// </summary>
        public static ConcurrentDictionary<string, ValueTuple<string, string, string, string, string, string>> VehicleList;

        //是否正在更新车辆信息
        public static bool isVehicleUpdate;

        /// <summary>
        /// 禁止驶入围栏信息，sim为key
        /// item1:围栏名称
        /// item2:所属公司
        /// item3:车辆类型
        /// item4:车辆编号
        /// item5:司机
        /// item6:点集
        /// </summary>
        public static ConcurrentDictionary<string, ValueTuple<string, string, string, string, string, List<Point>>> fenceFanbidInInfo;

        /// <summary>
        /// 禁止驶出围栏信息，sim为key
        /// item1:围栏名称
        /// item2:所属公司
        /// item3:车辆类型
        /// item4:车辆编号
        /// item5:司机
        /// item6:点集
        /// </summary>
        public static ConcurrentDictionary<string, ValueTuple<string, string, string, string, string, List<Point>>> fenceFanbidOutInfo;

        /// <summary>
        /// 终端版本字典，sim为key
        /// item1:808版本
        /// item2:1078版本
        /// item3:主动安全版本
        /// Item4:协议版本号，19版开始每次关键修订递增，初始版本为1
        /// </summary>
        public static ConcurrentDictionary<string, ValueTuple<string, string, string, int>> equipVersion;

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