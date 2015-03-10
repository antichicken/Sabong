using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Business;

public partial class Admin_DashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var r = RiskManagementHandler.Instance.GetCurrentRiskManagementSetting();
            txtR1.Text = r.RiskLevel1.ToString();
            txtR2.Text = r.RiskLevel2.ToString();
            txtR3.Text = r.RiskLevel3.ToString();
            txtR4.Text = r.RiskLevel4.ToString();
            txtR5.Text = r.RiskLevel5.ToString();

            txtRB1.Text = r.RiskBoxSizeLeve1.ToString();
            txtRB2.Text = r.RiskBoxSizeLeve2.ToString();
            txtRB3.Text = r.RiskBoxSizeLeve3.ToString();
            txtRB4.Text = r.RiskBoxSizeLeve4.ToString();
            txtRB5.Text = r.RiskBoxSizeLeve5.ToString();

            txtOpenningOdd.Text = SystemConfig.OpeningOddRate.ToString();
            txtMaxBet.Text = RiskManagementHandler.Instance.MaxBetSetting.ToString();
        }
    }
}