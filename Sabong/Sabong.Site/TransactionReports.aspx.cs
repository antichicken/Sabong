using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Business;
using Sabong.Repository.Repo;

public partial class TransactionReports : PageBase
{
    private ReportRepository _repository = new ReportRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadReport();
        }
    }

    void LoadReport()
    {
        var fromDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null);
        var toDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", null);
        var report = _repository.GetTransactionReports(SessionInfo.UserId, fromDate, toDate);
        rptReport.DataSource = report;
        rptReport.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
}