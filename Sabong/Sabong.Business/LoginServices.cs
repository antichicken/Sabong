using Sabong.Repository.Repo;

namespace Sabong.Business
{
    public class LoginServices
    {

        SessionsManager _sessionManager = new SessionsManager();
        UserRepository _memberRepo=new UserRepository();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="loginResult"></param>
        /// <param name="sessionId">using to store in httpContext.Session</param>
        public void DoLogin(string userName, string password, out LoginResult loginResult, out string sessionId)
        {
            sessionId = "";
            var u1 = _memberRepo.Login(userName,password);
            //FootBall.Betting.BussinessObject.Members  u = _memberRepo.GetByUserNameAndPassword(userName, password);

            if (u1 == null)
            {
                loginResult = LoginResult.WrongUserNameOrPassword;
                return;
            }

            if (u1.type != "")
            {
                //ko phai member
                loginResult = LoginResult.NotAllowAccessPlayerSite;
                return;
            }
               
              
           

          
            loginResult = LoginResult.Successful;

        }

        public void CheckSession(string sessionId, out LoginResult loginResult)
        {
            if (!_sessionManager.IsExistSessionId(sessionId))
            {
                loginResult = LoginResult.NotExist;
                return;
            }

            if (!_sessionManager.CheckLogin(sessionId))
            {
                loginResult = LoginResult.Expired;
                return;
            }

            loginResult = LoginResult.Successful;
        }

        public void DoLogout(string sessionId)
        {
            _sessionManager.Delete(sessionId);
        }

    }
}