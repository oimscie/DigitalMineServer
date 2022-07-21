using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.OrderMessage
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public struct OrderMessageType
    {
        /// <summary>
        /// 音视频请求
        /// </summary>
        public const string AudioAndVideo = "1000";
        /// <summary>
        /// 音视频控制
        /// </summary>
        public const string AudioAndVideoControl = "1001";
        /// <summary>
        /// 车载历史音视频请求
        /// </summary>
        public const string HisVideoAndAudio = "2000";
        /// <summary>
        /// 车载历史音视频控制请求
        /// </summary>
        public const string HisVideoAndAudioControl = "2001";
      /*  /// <summary>
        /// 车载实时视频请求
        /// </summary>
        public const int video = 2100;
        /// <summary>
        /// 车载实时视频控制请求
        /// </summary>
        public const int videoControl = 2101;*/
        /// <summary>
        /// 客户端登录
        /// </summary>
        public const string Login = "3000";
        /// <summary>
        /// 客户端心跳
        /// </summary>
        public const string ClientHeart = "3001";
        /// <summary>
        /// 用户本地数据终端心跳
        /// </summary>
        public const string LocalHeart = "4000";
        /// <summary>
        /// 本地数据终端上报所属公司
        /// </summary>
        public const string LocalReportCompany = "4001";
        /// <summary>
        /// 客户端打开监控请求
        /// </summary>
        public const string MonitorOpen = "5000";
        /// <summary>
        /// 客户端监控视频控制指令
        /// </summary>
        public const string MonitorControl = "5001";
        /// <summary>
        /// 本地数据终端监控视频上传请求
        /// </summary>
        public const string MonitorUpload = "5100";
    }
}
