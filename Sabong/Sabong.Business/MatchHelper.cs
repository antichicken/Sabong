using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

namespace Sabong.Business
{
    public static class MatchHelper
    {
        static readonly MatchRepository MatchRepository = new MatchRepository();
        public static bool AreEqual(this view_matchdetail m1, view_matchdetail m2)
        {
            if (m1 == null || m2 == null)
                return m1 == m2;
            
            if (m1.fslno != m2.fslno)
                return false;
            if (m1.GetMatchStatus() != m2.GetMatchStatus())
                return false;
            if (m1.C1odds !=m2.C1odds 
                || m1.C2odds!=m2.C2odds 
                || m1.drawwodds!=m2.drawwodds 
                || m1.ftd!=m2.ftd
                || m1.cock_type!=m2.cock_type
                || m1.agimage !=m2.agimage
                ||m1.agname!=m2.agname
                || m1.cname!=m2.cname
                ||m1.cimage!=m2.cimage)
            {
                return false;
            }
            return true;
        }

        public static MatchStatus GetMatchStatus(this view_matchdetail viewMatchDetail)
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
                if (MatchRepository.IsMatchStart(viewMatchDetail.fslno, out matchEnd))
                {
                    return !matchEnd ? MatchStatus.MatchStarted : MatchStatus.MatchEnd;
                }
                return MatchStatus.StopBet;

            }

            return matchstatus;


        }
    }
}