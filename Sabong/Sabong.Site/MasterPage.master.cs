using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Business;
using Sabong.Repository.EntityModel;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected double GivenCredit;
    protected double Profit;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SelectLang();
            Profit = User.GetCashBalance();
            GivenCredit = User.GetCreditBalance();
            GivenCredit = Math.Round(GivenCredit, 2, MidpointRounding.AwayFromZero);
            Profit = Math.Round(Profit, 2, MidpointRounding.AwayFromZero);
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

    protected user User
    {
        get
        {
            return SessionInfo.User;
        }
    }

}
