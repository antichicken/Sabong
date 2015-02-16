using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabong.Business
{
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
    }
}
