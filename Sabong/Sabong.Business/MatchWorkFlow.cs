using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

namespace Sabong.Business
{
    public class MatchWorkFlow
    {

        //set warning update `fight_assign` set `block_warning_time`='$time',`block_warning_entry_time`='$now' where `slno`='$matchno'
        //fight_assign set block='1' ---> stop bet
        // start match  --select `matchstarttime` where `matchno`='$matchno',`starttime`='$now'
        //Stop match  --update `matchstarttime` set `matchno`='$matchno',`stoptime`='$now' where `matchno`='$matchno  

        //update `fight_assign` set `cancelmatch`='1'   ---> cancel match

        //start time & stop time is varchar
        //block_warning_time float  ..block_warning_entry_time varchar
        //set winner_id or cancel match
        readonly MatchRepository _match = new MatchRepository();
        public MatchStatus GetMatchStatus(int slno)
        {
            var viewMatchDetail= _match.GetMatchStatus(slno);
            MatchStatus matchstatus;
            if (viewMatchDetail == null)
                return MatchStatus.MatchNotFound;
            if (string.IsNullOrEmpty(viewMatchDetail.cock_type) && string.IsNullOrEmpty(viewMatchDetail.against_type))
            {
                matchstatus = MatchStatus.UnConfirmed;
            }
            else
            {
                matchstatus = MatchStatus.Confirmed;
            }
            if (viewMatchDetail.cancelmatch == 1)
            {
                return MatchStatus.Cancel;
            }

            if (viewMatchDetail.block == 1)
            {
                bool matchEnd;
                if (_match.IsMatchStart(slno, out matchEnd))
                {
                    return !matchEnd ? MatchStatus.MatchStarted : MatchStatus.MatchEnd;
                }
                return MatchStatus.StopBet;
                
            }

            return matchstatus;

        
        }

        public MatchStatus GetMatchStatus(view_matchdetail viewMatchDetail)
        {
          //  var viewMatchDetail = _match.GetMatchStatus(slno);
            MatchStatus matchstatus;
            if (viewMatchDetail == null)
                return MatchStatus.MatchNotFound;
            if (string.IsNullOrEmpty(viewMatchDetail.cock_type) && string.IsNullOrEmpty(viewMatchDetail.against_type))
            {
                matchstatus = MatchStatus.UnConfirmed;
            }
            else
            {
                matchstatus = MatchStatus.Confirmed;
            }
            if (viewMatchDetail.cancelmatch == 1)
            {
                return MatchStatus.Cancel;
            }

            if (viewMatchDetail.block == 1)
            {
                bool matchEnd;
                if (_match.IsMatchStart(viewMatchDetail.fslno, out matchEnd))
                {
                    return !matchEnd ? MatchStatus.MatchStarted : MatchStatus.MatchEnd;
                }
                return MatchStatus.StopBet;

            }

            return matchstatus;


        }
        public void GetCurrentMatch()
        {
           
            string meronWalaConfirmedMessage;
            var viewMatchDetail=_match.GetCurrentMatch(out meronWalaConfirmedMessage);
            var matchStatus = GetMatchStatus(viewMatchDetail);


        }
        //void 
    }
}