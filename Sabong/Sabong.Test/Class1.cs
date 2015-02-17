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
    public class TestDB
    {
        [Test]
        public void LoginFixture()
        {
            Repository.Repo.UserRepository user=new UserRepository();
            var xx= user.Login("ttt","qq123456");
           // Assert.Equals(xx, null);
        }

        [Test]
        public void GetMatchRepoFixture()
        {
            Repository.Repo.MatchRepository xxx = new MatchRepository();
            var date=DateTime.Now.AddDays(-2).ToString("dd-MM-yyyy");
            var xx = xxx.GetFightAssignsByDate(date);
            // Assert.Equals(xx, null);
        }

         [Test]
        public void GetDateIfNoMatch()
        {
            Repository.Repo.MatchRepository xxx = new MatchRepository();

             var xx = xxx.GetCurrentMatch();
        }

        [Test]
        public void TestCurrentDateToString()
        {
            var xxx=DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
