using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabong.Business.BO;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

namespace Sabong.Business
{
    public static class UserHelper
    {
        private static UserServices _userServices = new UserServices();
        private static UserRepository _userRepository=new UserRepository();
        static readonly TransactionRepository  repository = new TransactionRepository();
        static readonly MatchRepository matchRepo = new MatchRepository();
        public static double GetCreditBalance(this user user)
        {
            return _userServices.GetCreditBalance(user.slno);
        }

        public static double GetUpdatedCredit(this user user)
        {
            return _userRepository.GetUpdatedCreditBalance(user.slno);
        }
        public static double GetCashBalance(this user user)
        {
            return _userServices.GetCashBalance(user.slno);
        }

        public static string GetCurencyName(this user user)
        {
            if (string.IsNullOrEmpty(user.currency_type))
            {
                return _userServices.GetCurrencyName(1);
            }
            return _userServices.GetCurrencyName(Int16.Parse(user.currency_type));
        }

        public static UserCredit UserCredit(this user user)
        {
            var userCredit = new UserCredit();
            userCredit.GivenCredit = user.GetCreditBalance();
            userCredit.Profit = user.GetCashBalance();

            
            string status;
            var currentMacth = matchRepo.GetCurrentMatch(out status);
            userCredit.BetCredit = userCredit.GivenCredit + userCredit.Profit;
            if (currentMacth != null)
            {
                var trans = repository.GetAcceptedTransactions(user.slno, currentMacth.fslno);
                foreach (var transaction in trans)
                {
                    userCredit.BetCredit -= transaction.acceptedamount;
                }
            }
            return userCredit;
        }
    }
}
