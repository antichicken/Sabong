using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Sabong.Repository;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;
using Sabong.Util;

namespace Sabong.Business
{
    public class SessionContainer
    {
        private static readonly Dictionary<string, SessionInfo> Session;
        static SessionContainer()
        {
            if (Session==null)
            {
                Session = new Dictionary<string, SessionInfo>();
            }
        }
        public static SessionInfo Get(string key)
        {
            SessionInfo info;
            if (Session.TryGetValue(key, out info))
            {
                if (info.LastUpdate.AddHours(24) < DateTime.Now)
                {
                    Session.Remove(key);
                    return null;
                }
                info.FetchNewUserInfo();
            }

            return info;
        }

        public static void Add(SessionInfo info)
        {
            var x = Exist(info.User.slno);
            if (x != null)
            {
                OnOtherLogin(x);
                Delete(x.SessionId);
            }

            info.LastUpdate = DateTime.Now;
            Session[info.SessionId] = info;
        }

        private static void OnOtherLogin(SessionInfo sessionInfo)
        {
            try
            {
                NodeHelper.SendToNode(new
                {
                    type = "loginlogout",
                    user = sessionInfo.SessionId,
                    client = sessionInfo.SessionId,
                    message = new
                    {
                        vi = "Tai khoan bi dang nhap boi nguoi dung khac",
                        en = "Account was logged in by another"
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.Logger.Error(ex);
            }
            
        }

        public static SessionInfo Update(string key)
        {
            SessionInfo tmp;
            if (Session.TryGetValue(key, out tmp))
            {
                tmp.LastUpdate = DateTime.Now;
                return tmp;
            }
            return null;
        }

        private static SessionInfo Exist(int userId)
        {
            foreach (var info in Session)
            {
                if (info.Value.User.slno == userId)
                {
                    return info.Value;
                }
            }
            return null;
        }

        public static void Delete(string key)
        {
            SessionInfo tmp;
            if (Session.TryGetValue(key, out tmp))
            {
                tmp.LastUpdate = DateTime.Now;
                Session.Remove(key);
            }
        }
    }

    public class SessionInfo
    {
        private readonly UserRepository _userRepo = new UserRepository();
        private user _user;
        public user User
        {
            get
            {
                if (_user == null)
                {
                    _user = _userRepo.GetUser(UserId);
                }
                return _user;
            }
            set { _user = value; }
        }
        public int UserId { get; set; }
        public string SessionId { get; set; }
        public DateTime LastUpdate { get; set; }

        public void FetchNewUserInfo()
        {
            _user = null;
        }
    }

    public enum UserStatus
    {
        Open = 0,
        Lock
    }

    public enum UserRole
    {
        Admin = 1,
        Agent,
        Customer
    }
}
