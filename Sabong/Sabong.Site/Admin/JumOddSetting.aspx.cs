using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Business;

public partial class Admin_JumOddSetting : System.Web.UI.Page
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
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var info = new RiskManagement();
            info.RiskLevel1 = float.Parse(txtR1.Text.Trim());
            info.RiskLevel2 = float.Parse(txtR2.Text.Trim());
            info.RiskLevel3 = float.Parse(txtR3.Text.Trim());
            info.RiskLevel4 = float.Parse(txtR4.Text.Trim());
            info.RiskLevel5 = float.Parse(txtR5.Text.Trim());

            info.RiskBoxSizeLeve1 = float.Parse(txtRB1.Text.Trim());
            info.RiskBoxSizeLeve2 = float.Parse(txtRB2.Text.Trim());
            info.RiskBoxSizeLeve3 = float.Parse(txtRB3.Text.Trim());
            info.RiskBoxSizeLeve4 = float.Parse(txtRB4.Text.Trim());
            info.RiskBoxSizeLeve5 = float.Parse(txtRB5.Text.Trim());

            RiskManagementHandler.Instance.UpdateRiskManagement(info);
            Done.Visible = true;
            Faild.Visible = false;
        }
        catch (Exception ex)
        {
            Done.Visible = false;
            Faild.Visible = true;
        }
    }
}