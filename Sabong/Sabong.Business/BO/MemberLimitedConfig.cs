using System;

namespace Sabong.Business
{
    public class MemberLimitedConfig
    {
        float _MinBetInOrder;
        float _MaxBetInOrder;

        float _MinBetPerMatch;
        float _MaxBetPerMatch;

        float _MinBetDraw;
        float _MaxBetDraw;
        //    double _ExtraOddsPromotion;
        float _MaxWinningBet;
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

        public float MinBetInOrder
        {
            get { return _MinBetInOrder; }
            set { _MinBetInOrder = value; }
        }
        public float MaxBetInOrder
        {
            get { return _MaxBetInOrder; }
            set { _MaxBetInOrder = value; }
        }

        public float MinBetPerMatch
        {
            get { return _MinBetPerMatch; }
            set { _MinBetPerMatch = value; }
        }
        public float MaxBetPerMatch
        {
            get { return _MaxBetPerMatch; }
            set { _MaxBetPerMatch = value; }
        }

        public float MinBetDraw
        {
            get { return _MinBetDraw; }
            set { _MinBetDraw = value; }
        }
        public float MaxBetDraw
        {
            get { return _MinBetDraw; }
            set { _MinBetDraw = value; }
        }

        public float MaxWinningBet
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