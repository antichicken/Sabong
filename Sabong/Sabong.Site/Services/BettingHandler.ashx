<%@ WebHandler Language="C#" Class="BettingHandler" %>

using System;
using System.Linq;
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
            betInfo.OddsId = int.Parse(context.Request.Params["match"]);
            betInfo.PlaceTime = DateTime.UtcNow;
            betInfo.ip = WebUtil.GetIPAddress();

            if (betInfo.BetType == BetType.Meron || betInfo.BetType == BetType.Wala)
            {
                betInfo.OddsRate = float.Parse(context.Request.Params["oddrate"]);
                betInfo.Cockid = int.Parse(context.Request.Params["cockid"]);
            }
            
            betInfo.OddsRateInString = context.Request.Params["oddrate"];
            
            var res = service.PlaceBets(betInfo);

            var wf = new MatchWorkFlow();
            
            context.Response.Write(JsonConvert.SerializeObject(new
            {
                Status = res.TransactionStatus.ToString(),
                RemainMoney=res.RemainStake,
                RateChange = res.RateChange,
                OddRateChanged=res.OddRateChange,
                BetList = wf.GetAllAcceptedTransaction(sessionInfo.User.slno, betInfo.MatchId).Select(i => new { i.id, i.matchno, i.cocktype, i.acceptedamount,i.odds})
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