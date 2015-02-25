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
            transaction trans=new transaction
                              {
                                  playerid = placeBet.MemberId,
                                  matchno = placeBet.MatchId,
                                  acceptedamount = placeBet.Stake,
                                  cocktype = placeBet.BetType.ToString(),
                                  odds = placeBet.OddsRate.ToString(),
                                  date = placeBet.PlaceTime,
                                  cockid = placeBet.Cockid,
                                  ip = placeBet.ip
                              };

            trans=transRepo.GetBetComUserId(trans);

            transRepo.Insert(trans);

        }
    }
}
