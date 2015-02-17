using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WebUtil
/// </summary>
public class WebUtil
{
	public WebUtil()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void SetCookie(string name, string value, DateTime exprire)
    {
        var cookies = HttpContext.Current.Response.Cookies;
        var c = new HttpCookie(name, value) { Expires = exprire };

        if (!cookies.AllKeys.Contains(name)) HttpContext.Current.Response.Cookies.Add(c);
        else HttpContext.Current.Response.Cookies.Set(c);
    }

    public static void ExprireCookie(string name)
    {
        SetCookie(name, "", DateTime.Now);
    }
}