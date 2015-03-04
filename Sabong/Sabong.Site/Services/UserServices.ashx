<%@ WebHandler Language="C#" Class="UserServices" %>

using System;
using System.Web;

public class UserServices : IHttpHandler {
    
    public void ProcessRequest (HttpContext context)
    {
        PlaceBet(context);
    }

    private void PlaceBet(HttpContext context)
    {
        var sessionInfo = WebUtil.GetSessionInfo();
        if (sessionInfo != null)
        {
            var currentPass = context.Request.Params["current"];
            var newPass = context.Request.Params["newpass"];
            var confirm = context.Request.Params["confirm"];
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