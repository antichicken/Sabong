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
        //if (!string.IsNullOrEmpty(Request["lang"]))
        //{

        //    Session["lang"] = Request["lang"];
        //}
        //string lang = Convert.ToString(Session["lang"]);
        //string culture = string.Empty;
        //if (lang.ToLower().CompareTo("en") == 0 || string.IsNullOrEmpty(culture))
        //{
        //    culture = "en-US";
        //}
        //if (lang.ToLower().CompareTo("vi") == 0)
        //{
        //    culture = "vi-VN";
        //}
        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
        //Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("vi-VN");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
        base.InitializeCulture();
    }
}