<%@ WebHandler Language="C#" Class="UserServices" %>

using System;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Sabong.Business;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

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