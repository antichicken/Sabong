using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Repository.Repo;
using Sabong.Util;

public partial class Annoucement : PageBase
{
    private readonly AnnoucementRepository _annoucementRepos = new AnnoucementRepository();
    readonly ArenaRepository _arenaRepository=new ArenaRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadAnnoucement();
    }

    void LoadAnnoucement()
    {
        try
        {
            var list = _annoucementRepos.GetAll();
            rptAnnouce.DataSource = list;
            rptAnnouce.DataBind();
        }
        catch (Exception ex)
        {
            LogHelper.Logger.Error(ex);
        }
        
    }

    protected string AreaName(string date)
    {
        return _arenaRepository.GetArenaNameByDate(date);
    }
}