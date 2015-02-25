using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// Summary description for PageBase
/// </summary>
public class PageBase : System.Web.UI.Page
{
    protected override void InitializeCulture()
    {
        if (Request.Cookies["user-lang"]!=null)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Request.Cookies["user-lang"].Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Request.Cookies["user-lang"].Value);
            }
            catch (Exception)
            {
                throw;
            }
        }
        base.InitializeCulture();
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        var session = WebUtil.GetSessionInfo();
        if (session!=null)
        {
            session.LastUpdate = DateTime.Now;
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}