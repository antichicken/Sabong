using System;

namespace Sabong.Business
{
    public class CockOddsBase
    {
        public bool MatchExpire { get; set; }
        public BetType BetType { get; set; }

        public bool OddExpire { get; set; }

        public bool AllowBet { get; set; }

      //  public bool MinMaxExeed { get; set; }

        

        public bool OddRateChange { get; set; }//Gia keo thay doi

        public float RateChange { get; set; }//Gia keo moi nhat trong DB


        public DateTime OddLastUpdateTime { get; set; }

        public bool IsWaitingBet { get; set; }

        public float RemainStake { get; set; }

        public TransactionStatus TransactionStatus { get; set; }
    }
}