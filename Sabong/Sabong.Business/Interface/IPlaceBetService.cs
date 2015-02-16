namespace Sabong.Business
{
    public interface IPlaceBetService
    {
        TransactionHandler PlaceBets(PlaceBet memberTransaction);
        CockOddsBase ValidateOdd(PlaceBet memberTransaction);
    }
}