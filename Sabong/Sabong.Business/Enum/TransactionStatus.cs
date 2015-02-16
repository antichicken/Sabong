namespace Sabong.Business
{
    public enum TransactionStatus
    {
        MarketExpire = 0,   
        OddValueChange,   //Giá kèo thay đổi
        WaitingBet,
        AcceptBet,
        AcceptAmountAndWaitingReBet, // Stataus này nghĩa là chỉ nhận 1 phần tiền và tiếp tục chờ đánh nốt số tiền còn lại. Ex: đánh 1000  ăn 0.98 chỉ nhận 300 ăn 0.98 và hỏi 700 có đánh với giá mới 0.95 hay ko 
        WalletNotEnough,  // Ví ko đủ tiền
        MaxBetExceed,    // Vượt quá số tiền đặt cược 
        MaxPerMatchExceed,  // Vượt quá số tiền đánh của 1 trận
        MaxWinningExceed  // 1 ngày chỉ được phép thằng tối đa
    }
}