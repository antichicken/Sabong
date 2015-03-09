<%@ WebHandler Language="C#" Class="UserServices" %>

using System;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Sabong.Business;
using Sabong.Business.BO;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

public class UserServices : IHttpHandler {
    
    public void ProcessRequest (HttpContext context)
    {
        var action = context.Request.Params["action"];
        if (action!=null)
        {
            if (action=="credit")
            {
                UserCredit(context);
            }
        }
        else
        {
            ChangePass(context);
        }
        
    }

    private void UserCredit(HttpContext context)
    {
        var sessionInfo = WebUtil.GetSessionInfo();
        if (sessionInfo != null)
        {
            var credit = sessionInfo.User.UserCredit();
            context.Response.Write(JsonConvert.SerializeObject(new
            {
                BetCredit = credit.BetCredit.NumberTostring(),
                Profit=credit.Profit.NumberTostring(),
                GivenCredit=credit.GivenCredit.NumberTostring()
            }));
        }
        else
        {
            context.Response.Write(JsonConvert.SerializeObject(new
            {
                type="error",
                code=0
            }));
        }
    }
    
    private void ChangePass(HttpContext context)
    {
        var sessionInfo = WebUtil.GetSessionInfo();
        if (sessionInfo != null) 
        {
            try
            {
                var currentPass = context.Request.Params["current"];
                var newPass = context.Request.Params["newpass"];
                var confirm = context.Request.Params["confirm"];
                sessionInfo.FetchNewUserInfo();
                var user = sessionInfo.User;
                if (user.password == currentPass.GetMd5Hash())
                {
                    if (confirm == newPass
                        && Regex.IsMatch(newPass, "^[a-zA-Z0-9]")
                        && Regex.IsMatch(newPass, "[A-Z]")
                        && Regex.IsMatch(newPass, "[0-9]")
                        && newPass.Length > 7)
                    {
                        user.password = newPass;
                        var repo = new UserRepository();
                        repo.UPdatePassWord(user);
                        context.Response.Write(JsonConvert.SerializeObject(new
                        {
                            status = 1,
                            message = "Change password successfully!"
                        }));
                        sessionInfo.FetchNewUserInfo();
                    }
                    else
                    {
                        context.Response.Write(JsonConvert.SerializeObject(new
                        {
                            status = -1,
                            message = "New password is not math with password rule!"
                        }));
                    }
                }
                else
                {
                    context.Response.Write(JsonConvert.SerializeObject(new
                    {
                        status = -1,
                        message = "Current password is not correct!"
                    }));
                }
            }
            catch (Exception ex)
            {
                sessionInfo.FetchNewUserInfo();
                context.Response.Write(JsonConvert.SerializeObject(new
                {
                    status=-1,
                    message="Change password failed, please try againt later!"
                }));
            }
            
        }
        else
        {
            context.Response.Write(JsonConvert.SerializeObject(new
            {
                status = 0,
                message = "User is not logged in!"
            }));
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}