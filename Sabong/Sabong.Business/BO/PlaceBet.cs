using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabong.Business
{
    public class PlaceBet
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public int MemberId { get; set; }

        public long OddsId { get; set; }

        public BetType BetType { get; set; }  // chọn Meron hay Wala hay 1 trong 3 kiểu draw

        public double OddsRate { get; set; }

        public double Stake { get; set; }  // số tiền cược

        public double MaxPayout { get; set; } // số tiền ăn tối đa

        public DateTime PlaceTime { get; set; }

        public string PlaceRemark { get; set; }  // Description

    
    }
}
