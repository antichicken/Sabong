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
    private readonly UserRepository _userRepo = RepositoryContainer.Factory.Get<UserRepository>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        var user = _userRepo.Login(txtUser.Text, txtPass.Text);
        if (user!=null)
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
        }
    }
}