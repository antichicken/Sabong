using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Business;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

public partial class Controls_MatchInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private view_matchdetail _match;
    public view_matchdetail Match
    {
        get
        {
            return _match;
        }
        set
        {
            _match = value;
            if (_match!=null)
            {
                Status = _match.GetMatchStatus();
            }
        }
    }
    public MatchStatus Status
    {
        get; set;
    }

    public string DisabledCss()
    {
        if (Status==MatchStatus.Confirmed || Status==MatchStatus.ClosingSoon)
        {
            return string.Empty;
        }
        return "disabled";
    }
}