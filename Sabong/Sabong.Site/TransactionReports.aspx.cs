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
    public double totalWin = 0;
    public double totalCom = 0;
    public double totalWinTax = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtEndDate.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            LoadReport();
        }
    }

    void LoadReport()
    {
        var fromDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null);
        var toDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", null);
        var report = _repository.GetTransactionReports(SessionInfo.UserId, fromDate, toDate);
        totalWin = report.Sum(i => i.winloseamnt);
        totalCom = report.Sum(i => i.betcomamt);
        totalWinTax = report.Sum(i => i.winloseamnt);
        rptReport.DataSource = report;
        rptReport.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected string BuildMatchName(string input)
    {
        var tmp = input.Split(new[] {"VS"}, StringSplitOptions.RemoveEmptyEntries);
        if (tmp.Length > 1)
            return string.Format("<span style='color:red'>{0}</span><br>vs<br><span style='color:blue'>{1}</span>",
                tmp[0], tmp[1]);
        
        return input;
    }
}