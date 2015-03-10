using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Business;
using Sabong.Repository.Repo;
using Sabong.Util;

public partial class Admin_OpeningOdd : System.Web.UI.Page
{
    readonly MatchRepository _matchRepos=new MatchRepository();
    private OddRepository _oddRepos = new OddRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInfo();
        }
    }

    void LoadInfo()
    {
        string anything;
        var match = _matchRepos.GetCurrentMatch(out anything);
        if (match!=null)
        {
            Cooktype.Value = match.cock_type.ToLower();
            LbMatch.Text = match.fslno.ToString();
            txtMeronRate.Text = match.C1odds.ToString();
            txtWalaRate.Text = match.C2odds.ToString();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            var odd = _oddRepos.GetOddsdiffCalcByMatchId(Convert.ToInt32(LbMatch.Text));
            if (odd!=null)
            {
                odd.C1odds = float.Parse(txtMeronRate.Text);
                odd.C2odds = float.Parse(txtWalaRate.Text);
                 _oddRepos.UpdateOddDiffCalc(odd);
            }

            Done.Visible = true;
            Faild.Visible = false;
        }
        catch (Exception ex)
        {
            ex.LogError();
            Done.Visible = false;
            Faild.Visible = true;
        }
    }
}