using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Repository.Repo;

public partial class Annoucement : PageBase
{
    private AnnoucementRepository annoucementRepos = new AnnoucementRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadAnnoucement();
    }

    void LoadAnnoucement()
    {
        var list = annoucementRepos.GetAll();
        rptAnnouce.DataSource = list;
        rptAnnouce.DataBind();
    }

    protected string AreaName(string date)
    {
        return annoucementRepos.GetArenaNameByDate(date);
    }
}