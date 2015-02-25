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
            betInfo.MatchId = int.Parse(context.Request.Params["match"]);
            betInfo.MaxPayout = 100;
            betInfo.MemberId = sessionInfo.User.slno;
            betInfo.OddsId = long.Parse(context.Request.Params["odd"]);
            betInfo.OddsRate = double.Parse(context.Request.Params["oddrate"]);
            betInfo.PlaceRemark = string.Empty;
            betInfo.PlaceTime = DateTime.UtcNow;
            betInfo.Stake = double.Parse(context.Request.Params["stake"]);
            betInfo.BetType = (BetType) int.Parse(context.Request.Params["type"]);
            var res = service.PlaceBets(betInfo);
            context.Response.Write(JsonConvert.SerializeObject(res));
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