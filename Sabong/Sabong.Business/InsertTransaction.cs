using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabong.Repository;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

namespace Sabong.Business
{
    public  class InsertTransaction
    {
       
    }

    public class TransationServices
    {
        public void Insert(PlaceBet placeBet)
        {
            //insert into `transaction` set `playerid`='$_SESSION[useridval]',`matchno`='$match_slno',`acceptedamount`='$sacceptval',`cocktype`='$cocktype1',`odds`='$wodds',`date`='$date',`cockid`='$cockid',`multiplier`='$multiplier',`ip`='$ip'

            //do validate, calculate odd..jump odd before insert ...
            TransactionRepository transRepo=new TransactionRepository();
         //   string oddsString = placeBet.OddsRate.ToString().PadLeft(4);
          //  string oddsString1 = placeBet.OddsRate.ToString().PadRight(4);
            string oddsString;
            if (placeBet.BetType == BetType.Meron || placeBet.BetType == BetType.Wala)
            {
                oddsString = placeBet.OddsRate.ToString().Substring(0, 4);
            }
            else
            {
                oddsString = placeBet.OddsRateInString;
            }
            //$multiplier=1/$crncy_value;
            UserServices userServices=new UserServices();
            var value=userServices.GetCurrencyValueByUserId(placeBet.MemberId);
            var multipliers = 0;
            if (!string.IsNullOrEmpty(value))
            {
                multipliers = int.Parse(value);
            }
            transaction trans=new transaction
                              {
                                  playerid = placeBet.MemberId,
                                  matchno = placeBet.MatchId,
                                  acceptedamount = placeBet.Stake,
                                  cocktype = placeBet.BetType.ToString(),
                                  odds = oddsString,
                                  date = placeBet.PlaceTime,
                                  time = DateTime.Now,
                                  cockid = placeBet.Cockid,
                                  ip = placeBet.ip,
                                  betstatus = ""
                                 ,multiplier = multipliers
                                 
                              };

            trans=transRepo.GetBetComUserId(trans);

            transRepo.Insert(trans);

        }
    }
}
