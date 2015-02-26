using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

namespace Sabong.Business
{
    public class RiskManagement
    {
       // int MatchId { get; set; }
        float Spread { get; set; }
        double BoxLimit { get; set; }
        float RiskLevel1 { get; set; }
        
        float RiskLevel2 { get; set; }
        float RiskLevel3 { get; set; }
        float RiskLevel4 { get; set; }
        float RiskLevel5 { get; set; }

        float RiskBoxSizeLevel { get; set; }
        float RiskBoxSizeLeve2 { get; set; }
        float RiskBoxSizeLeve3 { get; set; }
        float RiskBoxSizeLeve4 { get; set; }
        float RiskBoxSizeLeve5 { get; set; }
    }
    public sealed class BoxReceivedMoney
    {
        private static volatile BoxReceivedMoney _instance;
        private static readonly object SyncRoot = new Object();

        private BoxReceivedMoney()
        {
            boxSize = 3000;
           // CurrentBoxValue = 0;
            thrholdGroupA = 0.01f;
        }

        //Home Stake
        //Away Stake

        //If abs(Home stake - Away Stake )  >= 3000 ---> clear CurrentBoxReceived =0 + 
        //Current Status Of Box. 
        //Total Box or Big Box
        //List<OddWala, OddMeron , Diff,MatchId)---> current.

        // Setting A,B,C.
        public Dictionary<int,double> _currentBoxValue =new Dictionary<int, double>();

        public Dictionary<int, oddsdiff_calc> _currentOddValue =new Dictionary<int, oddsdiff_calc>();

        void InitOdd(int MatchId)
        {
            oddsdiff_calc oddDiff;
            OddRepository oddRepo = new OddRepository();
            if (!_currentOddValue.TryGetValue(MatchId, out oddDiff))
            {
                //Get Odd from database
                oddDiff = oddRepo.GetOddsdiffCalcByMatchId(MatchId);

                _currentOddValue.Add(MatchId, oddDiff);
            }
        }


        void JumpOddDiff(int MatchId,float thrhold,bool isMeron)
        {
            //Get Current OddDiff or Opening Odd
            oddsdiff_calc oddDiff;
          
            if (_currentOddValue.TryGetValue(MatchId, out oddDiff))
            {
                if (isMeron)
                {
                    oddDiff.C1odds -= thrhold;//giam meron
                    oddDiff.C2odds += thrhold;//tang wala
                }
                else
                {
                    oddDiff.C1odds += thrhold;//tang meron
                    
                    oddDiff.C2odds -= thrhold;//giam wala
                }
                _currentOddValue[MatchId] = oddDiff;
            }
                
            
            
           
           
        }
        public CockOddsBase ReceiveMoney(PlaceBet betTransaction)
        {
            //If bet on Meron is Plus and bet on Wala is Minus.
            CockOddsBase retVal=new CockOddsBase();
            double currentstake;
            double remainStake;
            if (!_currentBoxValue.TryGetValue(betTransaction.MatchId, out currentstake))
            {// chua co keo nao` Bet vao tran dau nay`
                InitOdd(betTransaction.MatchId);
                if (betTransaction.BetType == BetType.Meron)
                {
                    //CurrentBoxValue.Keys = betTransaction.MatchId;
                    currentstake = betTransaction.Stake;
                }
                if (betTransaction.BetType == BetType.Wala)
                {
                    currentstake = -betTransaction.Stake;
                }
                if (Math.Abs(currentstake) >= boxSize)
                {
                    // nhan 1 phan tien de = box size va alert odd change 
                    //calculate remain stake to continue bet
                    remainStake = boxSize - (Math.Abs((currentstake)) + betTransaction.Stake);
                    //Empty current Box
                    retVal.RemainStake = remainStake;
                    currentstake = 0;

                    //Calculate odd to Odd jump because box clear;
                    bool isMeron = currentstake > 0;
                    JumpOddDiff(betTransaction.MatchId,thrholdGroupA,isMeron);
                }
               
                // Add gia tri cua Dictionary
                _currentBoxValue.Add(betTransaction.MatchId, currentstake);
            }
            else
            {
                if (betTransaction.BetType == BetType.Meron)
                {
                    //CurrentBoxValue.Keys = betTransaction.MatchId;
                    currentstake += betTransaction.Stake;
                }
                if (betTransaction.BetType == BetType.Wala)
                {
                    currentstake -= betTransaction.Stake;
                }
                if (Math.Abs(currentstake) >= boxSize)
                {
                    // nhan 1 phan tien de = box size va alert odd change 
                    //calculate remain stake to continue bet
                    remainStake = boxSize - (Math.Abs((currentstake)) + betTransaction.Stake);
                    //Empty current Box
                    retVal.RemainStake = remainStake;
                    currentstake = 0;
                    //Calculate odd to Odd jump because box clear;
                    bool isMeron = currentstake > 0;
                    JumpOddDiff(betTransaction.MatchId, thrholdGroupA, isMeron);
                }
                //Update gia tri cua Box
                _currentBoxValue[betTransaction.MatchId] = currentstake;
            }
            return retVal;

        }

        public float thrholdGroupA { get; set; }

        public double boxSize { get; set; }

        public static BoxReceivedMoney Instance
        {
           
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new BoxReceivedMoney();
                    }
                }

                return _instance;
            }
        }
    }
    public class TransactionHandler
    {
        public bool MarketExpire { get; set; }
        public long TransactionId { get; set; }

        public long OddId { get; set; }
        public BetType BetType { get; set; }
        public long MatchId { get; set; }
      
        public TransactionStatus Status { get; set; }
        

        public string ReturnMessage { get; set; }

        public bool IsSuccessBet { get; set; }

        public bool IsWaitingBet { get; set; }

        public bool MatchExpire { get; set; }

        public bool OddExpire { get; set; }

        public double MoneyAccept { get; set; }  // tiền cược được chấp nhận

        public double RemainMoney { get; set; } // số tiền còn lại để đánh với cược mới

        public bool OddRateChange { get; set; }//Gia keo thay doi

        public double RateChange { get; set; }//Gia keo moi nhat trong DB

        public bool OddMeronWalaJump { get; set; }//Ti le ăn của Meron hoặc Wala nhảy



        //public bool OddWalaJump { get; set; }//Ti le ăn của Wala nhảy

        public double ValueChange { get; set; }//ti le chap moi nhat trong DB

        public bool WalletEnought { get; set; }

        public DateTime OddLastUpdateTime { get; set; }
        public bool MeronWalaUnConfirmed { get; set; }
    }
}
