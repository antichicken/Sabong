namespace Sabong.Business
{
    public interface IPlaceBetService
    {
        TransactionHandler PlaceBets(PlaceBet memberTransaction);
        CockOddsBase ValidateOdd(PlaceBet memberTransaction);
    }

    public class PlaceBetService : IPlaceBetService
    {
        public TransactionHandler PlaceBets(PlaceBet memberTransaction)
        {
            throw new System.NotImplementedException();
        }

        public CockOddsBase ValidateOdd(PlaceBet memberTransaction)
        {
            throw new System.NotImplementedException();
        }
    }
}