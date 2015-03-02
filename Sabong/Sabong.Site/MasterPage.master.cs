using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Business;

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

    private SessionInfo session;

    protected SessionInfo SessionInfo
    {
        get
        {
            if (session == null)
            {
                session = WebUtil.GetSessionInfo();
            }
            return session;
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
}
