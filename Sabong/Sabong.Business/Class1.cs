using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabong.Business
{
    public class Class1
    {
    }

    public class OddServiceHandler
    {

        //Diff odd for Diff Country
        OddsDetail DiffOddForDiffCountry(OddsDetail odds, SpreadGroup spreadGroup)
        {
            if (spreadGroup == SpreadGroup.GroupC)
            {
                odds.MeronOdd += -0.02;
                odds.WalaOdd += -0.02;
               
            }
            if (spreadGroup == SpreadGroup.GroupB)
            {
                odds.MeronOdd += -0.01;
                odds.WalaOdd += -0.01;
               
            }
            return odds;
        }

        public OddsDetail GetCurrentOdd(int MatchId)
        {
            //Get Opening Odd from Operator In Database
            var odd = new OddsDetail();
            return odd;
        }
    }

    public enum SpreadGroup
    {
        GroupA,
        GroupB,
        GroupC
    }

    public class OddsDetail
    {
        public int Id { get; set; }

        public int MatchId { get; set; }


        public double MeronOdd { get; set; }

        public double WalaOdd { get; set; }

        public double CurrentDiff { get; set; }

        public bool IsPublish { get; set; }

        public DateTime LastUpdate { get; set; }

        public byte Status { get; set; }
    }
}
