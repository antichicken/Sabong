using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sabong.Repository;
using Sabong.Repository.Repo;


namespace Sabong.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Text1()
        {
            Repository.Repo.UserRepository user=new UserRepository();
            var xx= user.Login("t","qq123456");
        }


        [Test]
        public void GetCurrentMatch()
        {
            var match = new MatchRepository();
            string status = "";
           var xxx=  match.GetCurrentMatch(out status);

            var matchNo = xxx.match_no;

            var matchId = xxx.fslno;


            bool matchEnd;
            var xyz = match.IsMatchStart(matchId, out matchEnd);

        }

        [Test]
        public void GetAnnoucement()
        {
            var match = new AnnoucementRepository();
            var xx = match.GetAll();
            
        }

        [Test]
        public void GetAnnoucementRunning()
        {
            var match = new AnnoucementRunningRepository();
            var xx = match.GetAll();
        }
    }
}
