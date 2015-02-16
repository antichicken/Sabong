using System;

namespace Sabong.Business
{
    public class Sessions
    {
        public long MemberId { get; set; }

        public string MemberUserName { get; set; }

        public string SessionId { get; set; }

        public DateTime LastActived { get; set; }

        public Sessions()
        {
            MemberUserName = "";
            SessionId = "";
            LastActived = DateTime.Now;
        }
    }
}