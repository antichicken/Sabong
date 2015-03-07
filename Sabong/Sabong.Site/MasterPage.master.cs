using System;
using Sabong.Business;
using Sabong.Business.BO;
using Sabong.Repository.EntityModel;

public partial class MasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SelectLang();
        }
    }

    void SelectLang()
    {
        try
        {
            var lang = WebUtil.GetCurrentLang();
            if (!string.IsNullOrWhiteSpace(lang))
            {
                ddlLang.SelectedValue = lang;
            }
        }
        catch (Exception)
        {
        }
    }

    private SessionInfo _session;

    protected SessionInfo SessionInfo
    {
        get
        {
            if (_session == null)
            {
                _session = WebUtil.GetSessionInfo();
            }
            return _session;
        }
    }

    private UserCredit _credit;

    protected UserCredit Credit
    {
        get
        {
            if (_credit==null)
            {
                _credit = User.UserCredit();
            }
            return _credit;
        }
    }

    protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
    {
        WebUtil.SetCookie("user-lang",ddlLang.SelectedValue,DateTime.Now.AddYears(1000));
        Response.Redirect(Request.RawUrl);
    }
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        WebUtil.LogOut();
        Response.Redirect("~/Login.aspx");
    }

    protected user User
    {
        get
        {
            return SessionInfo.User;
        }
    }
    
}
