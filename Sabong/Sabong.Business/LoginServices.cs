using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

namespace Sabong.Business
{

    public class UserServices
    {
        readonly Repository.Repo.UserRepository _user = new UserRepository();
        public double GetCreditBalance(int userId)
        {
            
            return _user.GetCreditBalance(userId);
        }

        public double GetCashBalance(int userId)
        {

            return _user.GetCashBalance(userId);
        }

        public string GetCurrencyName(int currentId)
        {

            return _user.GetCurrencyName(currentId);
        }

        public playerbet_limit GetUserLimit(int userId)
        {
           return
            _user.GetPlayerbetLimit(userId);
        }

        public string GetCurrencyValueByUserId(int memberId)
        {
            return _user.GetCurrencyValueByUserId(memberId);
        }
    }
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

            if (u1.user_status == 1)
            {
                loginResult = LoginResult.Closed;
                return;
            }
            if(u1.user_status!=0)
            {
                loginResult=LoginResult.Suspended;
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