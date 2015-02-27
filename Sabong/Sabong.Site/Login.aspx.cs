using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Business;
using Sabong.Repository;
using Sabong.Repository.Repo;

public partial class Login : System.Web.UI.Page
{
    private readonly UserRepository _userRepo = new UserRepository();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (WebUtil.IsValidCapChar(vercode.Text.Trim()))
        {
            var user = _userRepo.Login(username.Text, password.Text);
            if (user != null)
            {
                var sessionInfo = new SessionInfo()
                {
                    LastUpdate = DateTime.Now,
                    SessionId = Guid.NewGuid().ToString(),
                    User = user,
                    UserId = user.slno
                };
                SessionContainer.Add(sessionInfo);

                WebUtil.SetCookie("sec", sessionInfo.SessionId, DateTime.Now.AddDays(7));
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                ltrMessage.Text = "<label for=\"username\" generated=\"false\" class=\"error\">Username or Password is not correct</label>";
            }
        }
        else
        {
            ltrMessage.Text = "<label for=\"vercode\" generated=\"false\" class=\"error\">Verification code is not correct</label>";
        }

    }
}