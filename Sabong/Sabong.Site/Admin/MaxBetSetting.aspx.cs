using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Business;

public partial class Admin_MaxBetSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        float newvalue;
        if (float.TryParse(txtMaxBet.Text,out newvalue))
        {
            RiskManagementHandler.Instance.MaxBetSetting = newvalue;
            Done.Visible = true;
            Faild.Visible = false;
        }
        else
        {
            Done.Visible = false;
            Faild.Visible = true;
        }
    }
}