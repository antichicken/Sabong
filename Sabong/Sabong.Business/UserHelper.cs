using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabong.Repository.EntityModel;

namespace Sabong.Business
{
    public static class UserHelper
    {
        private static UserServices _userServices = new UserServices();
        public static double GetCreditBalance(this user user)
        {
            return _userServices.GetCreditBalance(user.slno);
        }

        public static double GetCashBalance(this user user)
        {
            return _userServices.GetCashBalance(user.slno);
        }
    }
}
