using System;
using System.Web.UI;
using Sabong.Business;
using Sabong.Repository.EntityModel;

public partial class Login : Page
{
    readonly LoginServices _loginServices=new LoginServices();
    protected void Page_Load(object sender, EventArgs e)
    {
        var sessionInfo = WebUtil.GetSessionInfo();
        if (sessionInfo!=null)
        {
            Response.Redirect("Default.aspx");
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (WebUtil.IsValidCapChar(vercode.Text.Trim()))
        {
            LoginResult loginResult;
            user userinfo;
            string sessionId;//do not use now
            _loginServices.DoLogin(username.Text,password.Text,out loginResult,out sessionId,out userinfo);
            if (userinfo != null && loginResult==LoginResult.Successful)
            {
                switch (loginResult)
                {
                    case LoginResult.Successful:
                    {
                        var sessionInfo = new SessionInfo()
                        {
                            LastUpdate = DateTime.Now,
                            SessionId = Guid.NewGuid().ToString(),
                            User = userinfo,
                            UserId = userinfo.slno
                        };
                        SessionContainer.Add(sessionInfo);

                        WebUtil.SetCookie("sec", sessionInfo.SessionId, DateTime.Now.AddDays(7));
                        Response.Redirect("~/Term.aspx");
                        break;
                    }
                    case LoginResult.Closed:
                    {
                        vercode.Text = string.Empty;
                        ltrMessage.Text = "<label for=\"username\" generated=\"false\" class=\"error\">Your account are closed</label>";
                        break;
                    }
                    case LoginResult.Suspended:
                    {
                        vercode.Text = string.Empty;
                        ltrMessage.Text = "<label for=\"username\" generated=\"false\" class=\"error\">Your account are suspended</label>";
                        break;
                    }
                    case LoginResult.Expired:
                    {
                        vercode.Text = string.Empty;
                        ltrMessage.Text = "<label for=\"username\" generated=\"false\" class=\"error\">Your account are expired</label>";
                        break;
                    }
                    case LoginResult.NotAllowAccessPlayerSite:
                    {
                        vercode.Text = string.Empty;
                        ltrMessage.Text = "<label for=\"username\" generated=\"false\" class=\"error\">Your account are not allow to access player site</label>";
                        break;
                    }
                    default:
                    {
                        vercode.Text = string.Empty;
                        ltrMessage.Text = "<label for=\"username\" generated=\"false\" class=\"error\">Username or Password is not correct</label>";
                        break;
                    }
                }
                
            }
            else
            {
                vercode.Text = string.Empty;
                ltrMessage.Text = "<label for=\"username\" generated=\"false\" class=\"error\">Username or Password is not correct</label>";
            }
        }
        else
        {
            vercode.Text = string.Empty;
            ltrMessage.Text = "<label for=\"vercode\" generated=\"false\" class=\"error\">Verification code is not correct</label>";
        }

    }
}