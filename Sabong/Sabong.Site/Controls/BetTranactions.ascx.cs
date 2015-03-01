using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Business;
using Sabong.Repository.EntityModel;

public partial class Controls_BetTranactions : UserControl
{

    public view_matchdetail Match
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBet();
        }
    }

    private void LoadBet()
    {
        var session = WebUtil.GetSessionInfo();
        if (Match!=null && session!=null)
        {
            var wf = new MatchWorkFlow();
            var trans = wf.GetAllAcceptedTransaction(session.User.slno, Match.fslno);
            rptBetList.DataSource = trans;
        }
        
    }
}