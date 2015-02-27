using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

public partial class Controls_MatchInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public view_matchdetail Match
    {
        get; set;
    }

    public string Status
    {
        get; set;
    }
}