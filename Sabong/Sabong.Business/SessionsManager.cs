using System;
using System.Collections.Generic;
using System.Threading;

namespace Sabong.Business
{
    public class SessionsManager
    {
        private static readonly IDictionary<long, Sessions> Sessionses = new Dictionary<long, Sessions>();
        // private static readonly ISessionsRepository SessionsRepository = RepositoryContainer.Factory.Get<ISessionsRepository>();

        static SessionsManager()
        {
            var thread = new Thread(UpdateSessionToDb);
            thread.Start();
        }
        /// <summary>
        /// Update lastActive để ra hạn Session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="violation"></param>
        public void Update(string sessionId, out SessionViolation violation)
        {
            //var tem = SessionsRepository.GetById(sessionId);

            //if (tem != null && tem.MemberId > 0)
            //{
            //    if (DateTime.Now.AddMinutes(Global.GetSecctionTimeOut() * -1) >= tem.LastActived)
            //    {
            //        violation = SessionViolation.SessionExprire;
            //        Delete(sessionId);
            //        return;
            //    }
            //    tem.LastActived = DateTime.Now;
            //    SessionsRepository.Update(tem);
            //    violation = SessionViolation.NoViolation;
            //    return;
            //}
            violation = SessionViolation.SessionNotExists;
        }

        /// <summary>
        /// Login thành công thì add session
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string Add(long memberId, string userName)
        {
            if (Sessionses.ContainsKey(memberId))
            {
                Sessionses.Remove(memberId);
                //SessionsRepository.Delete(memberId);
            }
            var sessions = new Sessions()
                           {
                               LastActived = DateTime.Now,
                               MemberId = memberId,
                               SessionId = Guid.NewGuid().ToString(),
                               MemberUserName = userName
                           };
            //sessions.SessionId = SessionsRepository.Insert(sessions);
            Sessionses.Add(memberId, sessions);
            return sessions.SessionId;
        }

        /// <summary>
        /// Kiểm Tra Seccion nhưng ko update lastActive cho session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool CheckSession(string sessionId)
        {
            //var session = SessionsRepository.GetById(sessionId);
            //if (session != null && session.MemberId > 0 && DateTime.Now.AddSeconds(Global.GetSecctionTimeOut() * -1) < session.LastActived)
            //{
            //    return true;
            //}
            //if (session != null)
            //{
            //    Delete(sessionId);
            //}
            return false;
        }

        /// <summary>
        /// Logout thì delete Session
        /// </summary>
        /// <param name="sessionId"></param>
        public void Delete(string sessionId)
        {
            //var tem = SessionsRepository.GetById(sessionId);

            //if (tem != null && tem.MemberId > 0)
            //{
            //    Sessionses.Remove(tem.MemberId);
            //    SessionsRepository.Delete(tem.MemberId);
            //}
        }

        public bool IsExistSessionId(string sessionId)
        {
            //var temp = SessionsRepository.GetById(sessionId);
            //return temp != null;
            return false;
        }

        /// <summary>
        /// Kiểm tra xem user đã đă nhập chưa
        /// true: Nếu đã đăng nhập và chưa hết hạn 
        /// false: Nếu Chưa đăng nhập hoặc đã hết hạn
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool CheckLogin(string sessionId)
        {
            //var tem = SessionsRepository.GetById(sessionId);

            //if (tem != null && tem.MemberId > 0 && DateTime.Now.AddMinutes(Global.GetSecctionTimeOut() * -1) < tem.LastActived)
            //{
            //    //Ra hạn cho session
            //    var violation = new SessionViolation();
            //    Update(tem.SessionId, out violation);
            //    return true;
            //}

            //if (tem != null)
            //{
            //    Delete(sessionId);
            //}

            return false;
        }

        private static void UpdateSessionToDb()
        {
            foreach (var session in Sessionses)
            {
                //SessionsRepository.Update(session.Value);
            }
            Thread.Sleep(new TimeSpan(0, 0, 0, 30));
        }
    }
}