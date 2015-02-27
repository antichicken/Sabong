using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sabong.Repository;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;
using Sabong.Business;


namespace Sabong.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void GetMatchStats()
        {
            Business.MatchWorkFlow matchWorkFlow=new MatchWorkFlow();

            matchWorkFlow.GetCurrentMatch();
        }

        ////1484
        /// 
       [Test]
        public void TestStoreGetComm()
        {
            Repository.Repo.UserRepository user = new UserRepository();
            // user.GetBetComUserId(1484);
        }
        [Test]
        public void TextLogin()
        {
            Repository.Repo.UserRepository user=new UserRepository();
            var xx= user.Login("t","qq123456");
        }

        [Test]
        public void TextInsert()
        {
            Repository.Repo.TransactionRepository user = new TransactionRepository();
            Repository.EntityModel.test xxTest=new test();
            xxTest.nn = "Fcuk1";
            user.InsertTest(xxTest);
        }

        [Test]
        public void UpdateTest()
        {
            Repository.Repo.TransactionRepository user = new TransactionRepository();
            Repository.EntityModel.test xxTest = new test();
            xxTest.idd = 1;
            xxTest.nn = "Fcuk123";
            user.UpdateTest(xxTest);
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
        public void GetBarcaratChart()
        {
            var match = new MatchRepository();
            string status = "";
            var xxx = match.GetFightAssignsByDate();

          


            bool matchEnd;
           

        }

        [Test]
        public void GetAnnoucement()
        {
            var match = new AnnoucementRepository();
            var xx = match.GetAll();
            var test = xx.Count;
            var test1 = xx[0].subject;
            var test2 = xx[0].time;
            

        }

        [Test]
        public void GetAnnoucementRunning()
        {
            var match = new AnnoucementRunningRepository();
            var xx = match.GetAll();
            var test = xx;
            //var test1 = xx[0];
          //  var test2=t
        }

        [Test]
        public void PostToNode()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8888/push");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"user\":\"test\"," +
                              "\"password\":\"bla\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        [Test]
        public void TestDate()
        {
            var date = DateTime.Now;
            var x = date.ToString("MMM dd, yyyy");
            Console.WriteLine(x);
        }
    }
}
