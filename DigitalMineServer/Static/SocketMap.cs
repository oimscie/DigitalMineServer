using DigitalMineServer.SuperSocket;
using JtLibrary.PacketBody;
using JtLibrary.Structures;
using JtLibrary.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

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

            equipVersion = new ConcurrentDictionary<string, (int, int, int)>();
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
        public static ConcurrentDictionary<string, ValueTuple< string, string, string, string, string, List<Point>>> fenceFanbidInInfo;
        /// <summary>
        /// 禁止驶出围栏信息，sim为key
        /// item1:围栏名称
        /// item2:所属公司
        /// item3:车辆类型
        /// item4:车辆编号
        /// item5:司机
        /// item6:点集
        /// </summary>
        public static ConcurrentDictionary<string, ValueTuple<string,string, string, string, string, List<Point>>> fenceFanbidOutInfo;
        /// <summary>
        /// 终端版本字典<终端SIM号，版本信息>
        /// item1:808版本
        /// item2:主动安全版本
        /// item3:1078版本
        /// </summary>
        public static ConcurrentDictionary<string, ValueTuple<int, int, int>> equipVersion;
    }
}
