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
            Business.MatchWorkFlow matchWorkFlow = new MatchWorkFlow();

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
            Repository.Repo.UserRepository user = new UserRepository();
            var xx = user.Login("i051111i7", "AA123456");

            var playerlimit=user.GetPlayerbetLimit(xx.slno);
            var minbet = playerlimit.minbet_draw;
        }

        [Test]
        public void TextInsert()
        {
            Repository.Repo.TransactionRepository user = new TransactionRepository();
            Repository.EntityModel.test xxTest = new test();
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
        public void ValidateOddAcceptBet()
        {
            double fuckdouble = 0.95f;
            float fuckfloat = 0.95f;
            var match = new MatchRepository();
            string status = "";
            var xxx = match.GetCurrentMatch(out status);

            var matchNo = xxx.match_no;

            var matchId = xxx.fslno;


            //bool matchEnd;
            //var xyz = match.IsMatchStart(matchId, out matchEnd);

            PlaceBet oddPlaceBet = new PlaceBet();
            oddPlaceBet.MatchId = matchId;
            oddPlaceBet.MemberId = 4908;
            oddPlaceBet.Stake = 30;
            oddPlaceBet.BetType = BetType.Meron;
            oddPlaceBet.OddsRate = 0.95f;
            oddPlaceBet.PlaceTime = DateTime.Now;
            oddPlaceBet.ip = "192.168.1.1";


            PlaceBet oddPlaceBetSecon = new PlaceBet();
            oddPlaceBetSecon.MatchId = matchId;
            oddPlaceBetSecon.MemberId = 4908;
            oddPlaceBetSecon.Stake = 500;
            oddPlaceBetSecon.BetType = BetType.Meron;
            oddPlaceBetSecon.OddsRate = 0.95f;

            PlaceBet oddPlaceBetThird = new PlaceBet();
            oddPlaceBetThird.MatchId = matchId;
            oddPlaceBetThird.MemberId = 4908;
            oddPlaceBetThird.Stake = 3000;
            oddPlaceBetThird.BetType = BetType.Meron;
            oddPlaceBetThird.OddsRate = 0.95f;

            var xxxdfx = Math.Round((float)oddPlaceBetThird.OddsRate, 4, MidpointRounding.AwayFromZero);
            //RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBet);

            //var x1 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);
            //RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBetSecon);
            //var x2 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);
            //RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBetThird);
            //var x3 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);

            IPlaceBetService placebet=new PlaceBetService();

            var cockodd=placebet.ValidateOdd(oddPlaceBet);

            if (Assert.Equals(cockodd.TransactionStatus, TransactionStatus.AcceptBet)) ;

        }

        [Test]
        public void ValidateOddAcceptAndREbet()
        {
            double fuckdouble = 0.95f;
            float fuckfloat = 0.95f;
            var match = new MatchRepository();
            string status = "";
            var xxx = match.GetCurrentMatch(out status);

            var matchNo = xxx.match_no;

            var matchId = xxx.fslno;


            //bool matchEnd;
            //var xyz = match.IsMatchStart(matchId, out matchEnd);

            PlaceBet oddPlaceBet = new PlaceBet();
            oddPlaceBet.MatchId = matchId;
            oddPlaceBet.MemberId = 4908;
            oddPlaceBet.Stake = 3050;
            oddPlaceBet.BetType = BetType.Meron;
            oddPlaceBet.OddsRate = 0.95f;
            oddPlaceBet.PlaceTime = DateTime.Now;
            oddPlaceBet.ip = "192.168.1.1";


            PlaceBet oddPlaceBetSecon = new PlaceBet();
            oddPlaceBetSecon.MatchId = matchId;
            oddPlaceBetSecon.MemberId = 4908;
            oddPlaceBetSecon.Stake = 500;
            oddPlaceBetSecon.BetType = BetType.Meron;
            oddPlaceBetSecon.OddsRate = 0.95f;

            PlaceBet oddPlaceBetThird = new PlaceBet();
            oddPlaceBetThird.MatchId = matchId;
            oddPlaceBetThird.MemberId = 4908;
            oddPlaceBetThird.Stake = 3000;
            oddPlaceBetThird.BetType = BetType.Meron;
            oddPlaceBetThird.OddsRate = 0.95f;

            var xxxdfx = Math.Round((float)oddPlaceBetThird.OddsRate, 4, MidpointRounding.AwayFromZero);
            //RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBet);

            //var x1 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);
            //RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBetSecon);
            //var x2 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);
            //RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBetThird);
            //var x3 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);

            IPlaceBetService placebet = new PlaceBetService();

            var cockodd = placebet.ValidateOdd(oddPlaceBet);

            //if (Assert.Equals(cockodd.TransactionStatus, TransactionStatus.AcceptAmountAndWaitingReBet)) ;

        }

        [Test]
        public void GetCurrentMatch()
        {
            double fuckdouble = 0.95f;
            float fuckfloat = 0.95f;
            var match = new MatchRepository();
            string status = "";
            var xxx = match.GetCurrentMatch(out status);

            var matchNo = xxx.match_no;

            var matchId = xxx.fslno;


            //bool matchEnd;
            //var xyz = match.IsMatchStart(matchId, out matchEnd);

            PlaceBet oddPlaceBet=new PlaceBet();
            oddPlaceBet.MatchId = matchId;
            oddPlaceBet.MemberId = 4908;
            oddPlaceBet.Stake = 30;
            oddPlaceBet.BetType=BetType.Meron;
            oddPlaceBet.OddsRate = 0.95f;
            oddPlaceBet.PlaceTime = DateTime.Now;
            oddPlaceBet.ip = "192.168.1.1";
            

            PlaceBet oddPlaceBetSecon = new PlaceBet();
            oddPlaceBetSecon.MatchId = matchId;
            oddPlaceBetSecon.MemberId = 4908;
            oddPlaceBetSecon.Stake = 500;
            oddPlaceBetSecon.BetType = BetType.Meron;
            oddPlaceBetSecon.OddsRate = 0.95f;

            PlaceBet oddPlaceBetThird = new PlaceBet();
            oddPlaceBetThird.MatchId = matchId;
            oddPlaceBetThird.MemberId = 4908;
            oddPlaceBetThird.Stake = 3000;
            oddPlaceBetThird.BetType = BetType.Meron;
            oddPlaceBetThird.OddsRate = 0.95f;
           
            var xxxdfx = Math.Round((float) oddPlaceBetThird.OddsRate, 4, MidpointRounding.AwayFromZero);
            RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBet);

            var x1 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);
            RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBetSecon);
            var x2 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);
            RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBetThird);
            var x3 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);

            //IPlaceBetService placebet=new PlaceBetService();

            //placebet.PlaceBets(oddPlaceBet);

        }

        [Test]
        public void PlaceOdd()
        {
            double fuckdouble = 0.95f;
            float fuckfloat = 0.95f;
            var match = new MatchRepository();
            string status = "";
            var xxx = match.GetCurrentMatch(out status);

            var matchNo = xxx.match_no;

            var matchId = xxx.fslno;


            //bool matchEnd;
            //var xyz = match.IsMatchStart(matchId, out matchEnd);

            PlaceBet oddPlaceBet = new PlaceBet();
            oddPlaceBet.MatchId = matchId;
            oddPlaceBet.MemberId = 4908;
            oddPlaceBet.Stake = 30;
            oddPlaceBet.BetType = BetType.Meron;
            oddPlaceBet.OddsRate = 0.95f;
            oddPlaceBet.PlaceTime = DateTime.Now;
            oddPlaceBet.ip = "192.168.1.1";


            PlaceBet oddPlaceBetSecon = new PlaceBet();
            oddPlaceBetSecon.MatchId = matchId;
            oddPlaceBetSecon.MemberId = 4908;
            oddPlaceBetSecon.Stake = 500;
            oddPlaceBetSecon.BetType = BetType.Meron;
            oddPlaceBetSecon.OddsRate = 0.95f;

            PlaceBet oddPlaceBetThird = new PlaceBet();
            oddPlaceBetThird.MatchId = matchId;
            oddPlaceBetThird.MemberId = 4908;
            oddPlaceBetThird.Stake = 3000;
            oddPlaceBetThird.BetType = BetType.Meron;
            oddPlaceBetThird.OddsRate = 0.95f;

            //var xxxdfx = Math.Round((float)oddPlaceBetThird.OddsRate, 4, MidpointRounding.AwayFromZero);
            //RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBet);

            //var x1 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);
            //RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBetSecon);
            //var x2 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);
            //RiskManagementHandler.Instance.ReceiveMoney(oddPlaceBetThird);
            //var x3 = RiskManagementHandler.Instance.GetCurrentOdd(matchId);

            IPlaceBetService placebet=new PlaceBetService();

            placebet.PlaceBets(oddPlaceBet);

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
