using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabong.Business;

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

    public static SessionInfo GetSessionInfo()
    {
        var session = SessionContainer.Get(GetUserKey());
        if (session != null)
        {
            session.LastUpdate = DateTime.Now;
            SetCookie("sec", session.SessionId, DateTime.Now.AddDays(7));
            return session;
        }
        return null;
    }

    public static string GetUserKey()
    {
        try
        {
            return HttpContext.Current.Request.Cookies["sec"].Value;
        }
        catch (Exception)
        {
            return "";
        }

    }

    public static void SetCookie(string name, string value, DateTime exprire)
    {
        var cookies = HttpContext.Current.Response.Cookies;
        var c = new HttpCookie(name, value) { Expires = exprire };

        if (!cookies.AllKeys.Contains(name)) HttpContext.Current.Response.Cookies.Add(c);
        else HttpContext.Current.Response.Cookies.Set(c);
    }

    public static string GetCurrentLang()
    {
        var cookies = HttpContext.Current.Request.Cookies;
        if (cookies["user-lang"]!=null)
        {
            return cookies["user-lang"].Value;
        }
        else
        {
            return string.Empty;
        }
    }

    public static void ExprireCookie(string name)
    {
        SetCookie(name, "", DateTime.Now);
    }
}