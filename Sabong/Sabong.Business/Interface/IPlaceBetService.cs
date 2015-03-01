using System.Text.RegularExpressions;
using Sabong.Repository.EntityModel;

namespace Sabong.Business
{
    public interface IPlaceBetService
    {
        CockOddsBase PlaceBets(PlaceBet memberTransaction);
        CockOddsBase ValidateOdd(PlaceBet memberTransaction);
    }

    public class PlaceBetService : IPlaceBetService
    {
        public CockOddsBase PlaceBets(PlaceBet memberTransaction)
        {
           //Validate Odd truoc Dua vao validate odd de return Transaction Handler
            
            //Validate Max winning   //Not implement this version
            //Validate xem odd change and Accept How Much to Jump
           // TransactionHandler reTransactionHandler = new TransactionHandler();
            var oddValidate=ValidateOdd(memberTransaction);

           //work flow for odd jump
            //Kiem tra xem la nhan bao nhieu tien
            //so sanh max bet cua user va so tien stake de quyet dinh co nhan du hay nhan bao nhieu
            //dat ra 1 so Max co dinh cho muc cuoc cua he thong de nhan bao nhieu tien.

            // tinh toan risk cua he thong :cac tham so can thiet
            //1. possible win( stake * odd)
            //2. max bet cua user

           



            //Convert currency de jump odd
            if (oddValidate.TransactionStatus == TransactionStatus.AcceptBet)
            {
                TransationServices transServices = new TransationServices();
                transServices.Insert(memberTransaction);

                //Convert currency de Jump odd
                //Calculate PT
                RiskManagementHandler.Instance.ReceiveMoney(memberTransaction);
               // reTransactionHandler.IsSuccessBet = true;
               // reTransactionHandler.ReturnMessage = "Bet Success!";
            }

            return oddValidate;

            // throw new System.NotImplementedException();
        }

        public CockOddsBase ValidateOdd(PlaceBet memberTransaction)
        {
            CockOddsBase cockOddsBase = new CockOddsBase();
            //Validate match block, confirmed meron wala, match cancel

            MatchWorkFlow matchWorkFlow=new MatchWorkFlow();
            var matchStatus= matchWorkFlow.GetMatchStatus(memberTransaction.MatchId);
            if (matchStatus == MatchStatus.UnConfirmed)
            {
                cockOddsBase.AllowBet = false;
                cockOddsBase.OddExpire = false;
                cockOddsBase.MatchExpire = false;

                cockOddsBase.TransactionStatus = TransactionStatus.MeronWalaUnConfirmed;
                return cockOddsBase;
            }
           
            if (matchStatus == MatchStatus.StopBet )
            {
                cockOddsBase.OddExpire = true;
                cockOddsBase.AllowBet = false;
                cockOddsBase.MatchExpire = false;

                cockOddsBase.TransactionStatus = TransactionStatus.MarketExpire;
                return cockOddsBase;
            }

            if (matchStatus == MatchStatus.MatchEnd ||
                matchStatus == MatchStatus.MatchStarted)
            {
                cockOddsBase.MatchExpire = true;
                cockOddsBase.OddExpire = true;
                cockOddsBase.AllowBet = false;

                cockOddsBase.TransactionStatus = TransactionStatus.MarketExpire;
                return cockOddsBase;
            }

            if (matchStatus == MatchStatus.ClosingSoon || matchStatus == MatchStatus.Confirmed)
            {
                //allow bet
                cockOddsBase.AllowBet = true;
                cockOddsBase.OddExpire = false;
                cockOddsBase.MatchExpire = false;
            }

            //Validate max bet, min bet
            UserServices userServices = new UserServices();
            var userLimit=userServices.GetUserLimit(memberTransaction.MemberId);

            switch (memberTransaction.BetType)
            {
                case BetType.Meron:
                    if (memberTransaction.Stake < userLimit.minbet_meron || memberTransaction.Stake > userLimit.maxbet_meron)
                    {
                        //not allow bet
                        cockOddsBase.TransactionStatus = TransactionStatus.MaxBetExceed;

                        return cockOddsBase;
                    }
                    break;
                case BetType.Wala:
                    if (memberTransaction.Stake < userLimit.minbet_wala || memberTransaction.Stake > userLimit.maxbet_wala)
                    {
                        //not allow bet
                        cockOddsBase.TransactionStatus = TransactionStatus.MaxBetExceed;
                        return cockOddsBase;
                    }
                    break;
                default:
                    if (memberTransaction.Stake < userLimit.minbet_draw || memberTransaction.Stake > userLimit.maxbet_draw)
                    {
                        //not allow bet
                        cockOddsBase.TransactionStatus = TransactionStatus.MaxBetExceed;
                        return cockOddsBase;
                    }
                    break;
            }
            //Validate Credit Balance and profit.
            var betCredit=userServices.GetCashBalance(memberTransaction.MemberId);
            if ((betCredit - memberTransaction.Stake) < 0)
            {
                cockOddsBase.TransactionStatus = TransactionStatus.WalletNotEnough;
                return cockOddsBase;
            }

            // Check odd change

            var oddDiff = RiskManagementHandler.Instance.GetCurrentOdd(memberTransaction.MatchId);
            if (memberTransaction.BetType == BetType.Meron)
            {
                if ((oddDiff.C1odds - memberTransaction.OddsRate) != 0)
                {
                    cockOddsBase.TransactionStatus = TransactionStatus.OddValueChange;
                    cockOddsBase.OddRateChange = true;
                    cockOddsBase.RateChange = oddDiff.C1odds;
                    return cockOddsBase;
                }
            }

            if (memberTransaction.BetType == BetType.Wala)
            {
                if ((oddDiff.C2odds - memberTransaction.OddsRate) != 0)
                {
                    cockOddsBase.TransactionStatus = TransactionStatus.OddValueChange;
                    cockOddsBase.OddRateChange = true;
                    cockOddsBase.RateChange = oddDiff.C2odds;
                    return cockOddsBase;
                }
            }


            cockOddsBase.TransactionStatus = TransactionStatus.AcceptBet;

            return cockOddsBase;
        }

        
    }

}