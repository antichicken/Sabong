<%@ WebHandler Language="C#" Class="BettingHandler" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Sabong.Business;

public class BettingHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context)
    {
        PlaceBet(context);
    }

    private void PlaceBet(HttpContext context)
    {
        var sessionInfo = WebUtil.GetSessionInfo();
        if (sessionInfo!=null)
        {
            IPlaceBetService service = new PlaceBetService();
            var betInfo = new PlaceBet();
            betInfo.MemberId = sessionInfo.User.slno;
            betInfo.MatchId = int.Parse(context.Request.Params["match"]);
            betInfo.Stake = float.Parse(context.Request.Params["stake"]);
            betInfo.BetType = (BetType)int.Parse(context.Request.Params["type"]);
            betInfo.OddsRate = double.Parse(context.Request.Params["oddrate"]);
            //betInfo.OddsId = long.Parse(context.Request.Params["odd"]);
            betInfo.PlaceTime = DateTime.UtcNow;
<<<<<<< HEAD
            betInfo.Stake = float.Parse(context.Request.Params["stake"]);
            betInfo.BetType = (BetType) int.Parse(context.Request.Params["type"]);
=======
            betInfo.ip = "192.168.1.1";
            
>>>>>>> 36d5b1e4a24b2b48b57db564dc8b5dee9cd06430
            var res = service.PlaceBets(betInfo);
            //market expride
            //context.Response.Write(JsonConvert.SerializeObject(new
            //{
            //    Status = TransactionStatus.MarketExpire.ToString()
            //}));

            //odd value change
            //context.Response.Write(JsonConvert.SerializeObject(new
            //{
            //    Status = TransactionStatus.OddValueChange.ToString(),
            //    RateChange=0.90
            //}));

            context.Response.Write(JsonConvert.SerializeObject(new
            {
                Status = TransactionStatus.AcceptAmountAndWaitingReBet.ToString(),
                MoneyAccept = 2000,
                RemainMoney=3000,
                RateChange = 0.7
            }));
        }
        else
        {
            throw new ApplicationException();
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}