namespace Sabong.Business
{
    public interface IPlaceBetService
    {
        TransactionHandler PlaceBets(PlaceBet memberTransaction);
        CockOddsBase ValidateOdd(PlaceBet memberTransaction);
    }

    class PlaceBetService : IPlaceBetService
    {
        public TransactionHandler PlaceBets(PlaceBet memberTransaction)
        {
           //Validate Odd truoc Dua vao validate odd de return Transaction Handler

            TransationServices transServices=new TransationServices();
            transServices.Insert(memberTransaction);

            throw new System.NotImplementedException();
        }

        public CockOddsBase ValidateOdd(PlaceBet memberTransaction)
        {

            //Validate xem odd change?
            //Validate max bet, min bet
            //Validate Max winning
            //Validate Credit Balance and profit.
            //Validate match block, confirmed meron wala, match cancel
            throw new System.NotImplementedException();
        }
    }
}