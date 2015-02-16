using System;

namespace Sabong.Business
{
    public class MemberLimitedConfig
    {
        double _MinBetInOrder;
        double _MaxBetInOrder;

        double _MinBetPerMatch;
        double _MaxBetPerMatch;

        double _MinBetDraw;
        double _MaxBetDraw;
        //    double _ExtraOddsPromotion;
        double _MaxWinningBet;
        DateTime _LastUpdate;
        //public byte Position
        //{
        //    get { return _Position; }
        //    set { _Position = value; }
        //}

        //public byte Grade
        //{
        //    get { return _Grade; }
        //    set { _Grade = value; }
        //}

        public double MinBetInOrder
        {
            get { return _MinBetInOrder; }
            set { _MinBetInOrder = value; }
        }
        public double MaxBetInOrder
        {
            get { return _MaxBetInOrder; }
            set { _MaxBetInOrder = value; }
        }

        public double MinBetPerMatch
        {
            get { return _MinBetPerMatch; }
            set { _MinBetPerMatch = value; }
        }
        public double MaxBetPerMatch
        {
            get { return _MaxBetPerMatch; }
            set { _MaxBetPerMatch = value; }
        }

        public double MinBetDraw
        {
            get { return _MinBetDraw; }
            set { _MinBetDraw = value; }
        }
        public double MaxBetDraw
        {
            get { return _MinBetDraw; }
            set { _MinBetDraw = value; }
        }

        public double MaxWinningBet
        {
            get { return _MaxWinningBet; }
            set { _MaxWinningBet = value; }
        }
       
        public DateTime LastUpdate
        {
            get { return _LastUpdate; }
            set { _LastUpdate = value; }
        }
    }
}