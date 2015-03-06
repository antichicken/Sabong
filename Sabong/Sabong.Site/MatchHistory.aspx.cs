using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

public partial class MatchHistory : System.Web.UI.Page
{
    private ReportRepository reportRepos = new ReportRepository();
    private MatchRepository matchRepository=new MatchRepository();
    private List<arena> arenas; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            arenas = matchRepository.GetAllArenas();
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            int arena = 0;
            if (!int.TryParse(Request.QueryString["arena"],out arena))
            {
                var firstOrDefault = arenas.FirstOrDefault();
                if (firstOrDefault != null) arena = firstOrDefault.id;
            }
            LoadReport(arena, txtDate.Text);
            LoadArena(arena);
        }
    }

    void LoadArena(int? selected)
    {
        ddlArena.DataSource = arenas;
        ddlArena.DataTextField = "arena_name";
        ddlArena.DataValueField = "id";
        ddlArena.DataBind();
        if (selected!=null)
        {
            foreach (ListItem item in ddlArena.Items)
            {
                if (item.Value == selected.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }
    }

    private void LoadReport(int arena, string date)
    {
        var rp = reportRepos.GetMatchResultByDate(arena, date.Replace("/","-"));
        rptReport.DataSource = rp;
        rptReport.DataBind();
    }

    public static string UnixTimeStampToDateTime(string input)
    {
        // Unix timestamp is seconds past epoch
        if (string.IsNullOrWhiteSpace(input))
            return "_";
        
        var unixTimeStamp = Convert.ToDouble(input);
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime.ToString("hh:mm:ss");
    }

    public string GetDuration(string input1, string input2)
    {
        if (string.IsNullOrWhiteSpace(input1) || string.IsNullOrWhiteSpace(input2))
            return "_";
        
        var t1 = Convert.ToDouble(input1);
        var t2 = Convert.ToDouble(input2);
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(t2 - t1).ToLocalTime();
        return dtDateTime.ToString("hh:mm:ss");
    }

    public string WinnerName(view_matchResult mat)
    {
        if (mat.winner_cockid == mat.acid)
        {
            return mat.agname;
        }
        if (mat.winner_cockid == mat.cid)
        {
            return mat.cname;
        }
        return "_";
    }

    protected string MatchResult(view_matchResult match)
    {
        if (match.winner_cockid == match.cid)
        {
            if (match.cock_type == "meron")
            {
                return "Meron";
            }
            return "Wala";
        }
        if (match.winner_cockid == match.acid)
        {
            if (match.cock_type == "meron")
            {
                return "Meron";
            }
            return "Wala";
        }
        if (match.winner_cockid == -1)
        {
            return "Draw";
        }

        if (match.winner_cockid == 0 && match.cancelmatch == 1)
        {
            return "Cancel";

        }
        return "--";
    }

    protected string MatchCancel(view_matchResult match)
    {
        if (match.winner_cockid == 0 && match.cancelmatch == 1)
        {
            return "cancel";
        }
        return string.Empty;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadReport(Convert.ToInt32(ddlArena.SelectedValue), txtDate.Text);
    }
}