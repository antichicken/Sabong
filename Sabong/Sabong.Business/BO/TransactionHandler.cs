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
       public  int MatchId { get; set; }
       public float Spread { get; set; }
       // double BoxLimit { get; set; }
       public float RiskLevel1 { get; set; }

       public float RiskLevel2 { get; set; }
       public float RiskLevel3 { get; set; }
       public float RiskLevel4 { get; set; }
       public float RiskLevel5 { get; set; }

       public float RiskBoxSizeLeve1 { get; set; }
       public float RiskBoxSizeLeve2 { get; set; }
       public float RiskBoxSizeLeve3 { get; set; }
       public float RiskBoxSizeLeve4 { get; set; }
       public float RiskBoxSizeLeve5 { get; set; }
    }
    public  class RiskManagementHandler
    {
        private static volatile RiskManagementHandler _instance;
        private static readonly object SyncRoot = new Object();

        private RiskManagementHandler()
        {
           
           // CurrentBoxValue = 0;
            thrholdGroupA = 0.01f;
            InitRiskManagement();
            MaxBetSetting = 3000;
        }
        //moi lan Jump Odd thi log lai 1 list<matchid,box level,risk level,current odd,isMeronJump>
        public Dictionary<int,List<OddJumpStore>> DictOddJump=new Dictionary<int, List<OddJumpStore>>(); 
        public void UpdateRiskManagement(RiskManagement newRiskmanagement)
        {
            newRiskmanagement.MatchId = _defaultRiskManagement.MatchId;
            _defaultRiskManagement = newRiskmanagement;
        }

        public RiskManagement GetCurrentRiskManagementSetting()
        {
            return _defaultRiskManagement;
        }
        private RiskManagement _defaultRiskManagement;
        void InitRiskManagement()
        {
           //if(dicRiskManagements.TryGetValue()) 
            _defaultRiskManagement=new RiskManagement
                                   {
                                       RiskBoxSizeLeve1 = 500,
                                       RiskBoxSizeLeve2 = 1000,
                                       RiskBoxSizeLeve3 = 1500,
                                       RiskBoxSizeLeve4 = 2000,
                                       RiskBoxSizeLeve5 = 2700,

                                       RiskLevel1 = 0.01f,
                                       RiskLevel2 = 0.02f,
                                       RiskLevel3 = 0.03f,
                                       RiskLevel4 = 0.04f,
                                       RiskLevel5 = 0.05f,
                                   };
        }

        static bool IsJumpOdd(RiskManagement riskData, float currentSize, float stake, out float boxSize, out float riskLevel)
        {
            

            var cumulatively = currentSize + stake;
            cumulatively = Math.Abs(cumulatively);

            if (cumulatively >= riskData.RiskBoxSizeLeve5)
            {
                riskLevel = riskData.RiskLevel5;
                boxSize = riskData.RiskBoxSizeLeve5;
                return true;
            }
            if (cumulatively >= riskData.RiskBoxSizeLeve4)
            {
                riskLevel = riskData.RiskLevel4;
                boxSize = riskData.RiskBoxSizeLeve4;
                return true;
            }
            if (cumulatively >= riskData.RiskBoxSizeLeve3)
            {
                riskLevel = riskData.RiskLevel3;
                boxSize = riskData.RiskBoxSizeLeve3;
                return true;
            }
            if (cumulatively >= riskData.RiskBoxSizeLeve2)
            {
                riskLevel = riskData.RiskLevel2;
                boxSize = riskData.RiskBoxSizeLeve2;
                return true;
            }
            if (cumulatively >= riskData.RiskBoxSizeLeve1)
            {
                riskLevel = riskData.RiskLevel1;
                boxSize = riskData.RiskBoxSizeLeve1;
                return true;
            }
            riskLevel = riskData.RiskBoxSizeLeve1; ;
            boxSize = riskData.RiskBoxSizeLeve1;
            return false;
        }
        //MatchId,RiskManagement
        private Dictionary<int, RiskManagement> dictRiskManagements; 
        //Home Stake
        //Away Stake

        //If abs(Home stake - Away Stake )  >= 3000 ---> clear CurrentBoxReceived =0 + 
        //Current Status Of Box. 
        //Total Box or Big Box
        //List<OddWala, OddMeron , Diff,MatchId)---> current.

        // Setting A,B,C.
        public Dictionary<int, float> _currentBoxValue = new Dictionary<int, float>();

        public Dictionary<int, oddsdiff_calc> _currentOddValue =new Dictionary<int, oddsdiff_calc>();
        readonly OddRepository _oddRepo = new OddRepository();
        void InitOdd(int matchId)
        {
            oddsdiff_calc oddDiff;
            
            if (!_currentOddValue.TryGetValue(matchId, out oddDiff))
            {
                //Get Odd from database
                oddDiff = _oddRepo.GetOddsdiffCalcByMatchId(matchId);

                _currentOddValue.Add(matchId, oddDiff);
            }
        }
        public oddsdiff_calc GetCurrentOdd(int matchId)
        {
            //Get Current OddDiff or Opening Odd
            oddsdiff_calc oddDiff;

            if (_currentOddValue.TryGetValue(matchId, out oddDiff))
            {
                return oddDiff;
            }
            return _oddRepo.GetOddsdiffCalcByMatchId(matchId);
        }

       public float ProcessOddMoreThanOne(float odd)
        {
           // var sum = odd + threadHold;

            //-0.98 -0.03 =-1 
            if (odd>1)
            {
                var remainSum = odd - 1;
                //1.03 --> -0.97
                return remainSum - 1;
            }
            else if(odd<-1)
            {
                var remainSum = odd + 1;
                //1.03 --> -0.97
                return 1 + remainSum;
            }

            //-0.98 

           
                //-1.03 -> 0.97
                //0.95 ->0.9   -> 0.8 -> 
                //0.95 -> 1.0 -> -0.98 - 05 -->  -1.03
            return odd;

        }

       
        
        void JumpAndUpdateOddDiff(int matchId,float thrhold,float boxsize,bool isMeron)
        {
            //Get Current OddDiff or Opening Odd
            oddsdiff_calc oddDiff;
          
            if (_currentOddValue.TryGetValue(matchId, out oddDiff))
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

                oddDiff.C1odds = ProcessOddMoreThanOne(oddDiff.C1odds);
                oddDiff.C2odds = ProcessOddMoreThanOne(oddDiff.C2odds);
                    ;//tang wala
                //update odd diff. vao dictionary
                _currentOddValue[matchId] = oddDiff;

                //update vao db OddDiffCalc
                Repository.Repo.OddRepository oddrepo=new OddRepository();
                Repository.EntityModel.oddsdiff_calc oddsdiffCalc=new oddsdiff_calc();
                //oddsdiffCalc.C1odds = oddDiff.C1odds;
                //oddsdiffCalc.C2odds = oddDiff.C2odds;
                //oddsdiffCalc.match_slno = matchId;
             //   oddsdiffCalc.idval=oddf
                oddrepo.UpdateOddDiffCalc(oddDiff);

                //update vao Dictonary Log Odd Jump
                UpdateDictOddJump(matchId, thrhold, boxsize, isMeron);
            }
 
        }

        private void UpdateDictOddJump(int matchId, float thrhold, float boxsize, bool isMeron)
        {
            List<OddJumpStore> oddJumpStore;
            OddJumpStore oddjump = new OddJumpStore
                                   {
                                       MatchId = matchId,
                                       JumpTime = DateTime.Now,
                                       IsMeron = isMeron,
                                       RiskLevel = thrhold,
                                       BoxSize = boxsize
                                   };
            if (!DictOddJump.TryGetValue(matchId, out oddJumpStore))
            {
                oddJumpStore = new List<OddJumpStore> {oddjump};

                DictOddJump.Add(matchId, oddJumpStore);
            }
            else
            {
                oddJumpStore.Add(oddjump);
                DictOddJump[matchId] = oddJumpStore;
            }
        }

        public void ReceiveMoney(PlaceBet betTransaction)
        {
            //If bet on Meron is Plus and bet on Wala is Minus.
        //    CockOddsBase retVal=new CockOddsBase();
            float cumulatively;
            float remainStake;
             float riskLevel;
                float riskboxSize;
                float stake = 0;
            if (betTransaction.BetType == BetType.Meron)
            {
                //meron thi + wala thi -
                stake = betTransaction.Stake;
            }
            if (betTransaction.BetType == BetType.Wala)
            {
                stake = betTransaction.Stake*-1;
            }

            if (!_currentBoxValue.TryGetValue(betTransaction.MatchId, out cumulatively))
            {// chua co keo nao` Bet vao tran dau nay`
                InitOdd(betTransaction.MatchId);

                
               
                if (IsJumpOdd(_defaultRiskManagement, 0, stake, out riskboxSize, out riskLevel))
                {

                    //cumulattively=0
                    //cumulattively du bao nhieu
                    // nhan 1 phan tien de = box size va alert odd change 
                    //calculate remain stake to continue store in cumulatively
                    cumulatively = stake;//5000
                    //3000

                    //boxsize 2700 danh 3000 . Jump 300
                    remainStake = Math.Abs(cumulatively) - riskboxSize;
 
                    //3000
                    //du 2000
                    //500 nua

                    //Empty current Box
                   // retVal.RemainStake = remainStake;
                    

                    //Calculate odd to Odd jump because box clear;
                    bool isMeron = cumulatively > 0;
                    JumpAndUpdateOddDiff(betTransaction.MatchId,riskLevel,riskboxSize,isMeron);
                    cumulatively = remainStake;
                }
                else
                {
                    cumulatively = stake;
                }
               
                // Add gia tri cua Dictionary
                _currentBoxValue.Add(betTransaction.MatchId, cumulatively);
            }
            else
            {
                
                if (IsJumpOdd(_defaultRiskManagement,cumulatively,stake,out riskboxSize,out riskLevel))
                {
                    
                    // nhan 1 phan tien de = box size va alert odd change 
                    //calculate remain stake to continue bet
                    cumulatively += stake;
                    remainStake = riskboxSize - Math.Abs(cumulatively);
                    //Empty current Box
               //     retVal.RemainStake = remainStake;
                    
                    //Calculate odd to Odd jump because box clear;
                    bool isMeron = cumulatively > 0;
                    JumpAndUpdateOddDiff(betTransaction.MatchId, riskLevel,riskboxSize, isMeron);

                    cumulatively = remainStake;
                }
                else
                {
                    cumulatively += stake;
                }
                //Update gia tri cua Box
                _currentBoxValue[betTransaction.MatchId] = cumulatively;
            }
          //  return retVal;

        }

        public float thrholdGroupA { get; set; }


        public float MaxBetSetting { get; set; }
        public static RiskManagementHandler Instance
        {
           
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new RiskManagementHandler();
                    }
                }

                return _instance;
            }
        }
    }

    public class OddJumpStore
    {
        public DateTime JumpTime { get; set; }
        public int MatchId { get; set; } 
        public float BoxSize { get; set; }
        //,risk level,current odd,isMeronJump
        public float RiskLevel { get; set; }
        public bool IsMeron { get; set; }
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
