using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

namespace Sabong.Business
{
    public class SystemConfig
    {
        static MatchRepository matchRepos=new MatchRepository();
        public static float OpeningOddRate { get; set; }
        private static view_matchdetail _currentMatch;

        public static view_matchdetail CurrentMatch
        {
            get
            {
                if (_currentMatch==null)
                {
                    string anything;
                    _currentMatch = matchRepos.GetCurrentMatch(out anything);
                }
                return _currentMatch;
            }
            set
            {
                _currentMatch = value;
            }
        }
    }
}
