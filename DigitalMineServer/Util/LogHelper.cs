﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.Util
{
    class LogHelper
    {
        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("infoAppender");
        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("errorAppender");
        public static void WriteLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public static void WriteLog(string info, Exception ex)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, ex);
            }
        }
    }
}
