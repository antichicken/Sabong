using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Sabong.Util
{
    public static class LogHelper
    {
        private static ILog log = LogManager.GetLogger("SiteLog");

        public static ILog Logger
        {
            get
            {
                return log;
            }
        }

        public static void LogError(this Exception ex)
        {
            log.Error(ex);
        }

        public static void LogError(this Exception ex, string messame) 
        {
            log.Error(messame,ex);
        }
    }
}
