using System;

namespace Sabong.Business
{
    public static class Utility
    {
        //2015-02-15   yyyy-mm-dd
        public static string CurrentDateToString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}