using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

public partial class _Default : PageBase
{
    readonly MatchRepository _matchRepo = new MatchRepository();
    UserRepository _userRepos = new UserRepository();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MatchInfo.Match = Match;
            ChartControls1.CharInfo = ChartInfo;
            BetTranactions1.Match = Match;
        }
    }

    private view_matchdetail _match;
    protected view_matchdetail Match
    {
        get
        {
            if (_match == null)
            {
                
                _match = _matchRepo.GetCurrentMatch(out _status);
            }
            return _match;

        }
    }

    private string _status;
    protected string Status
    {
        get { return _status; }
    }

    private string _chartInfo=string.Empty;
    protected string ChartInfo
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_chartInfo))
            {
                var info = _matchRepo.GetFightAssignsByDate();
                if (info!=null)
                {
                    var x = new
                    {
                        chartInfo = info,
                        banker = info.Count(i => i == "banker"),
                        player = info.Count(i => i == "player"),
                        draw = info.Count(i => i == "draw"),
                    };
                    _chartInfo = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                }
            }
            return _chartInfo;
        }
    }

    private string _annoucement = string.Empty;

    AnnoucementRunningRepository _annoucementRepos = new AnnoucementRunningRepository();

    protected string RunningAnnoucement
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_annoucement))
            {
                _annoucement = _annoucementRepos.GetLatest();
            }
            return _annoucement;
        }
    }

    private playerbet_limit _playerbetLimit;
    protected playerbet_limit PlayerLimit
    {
        get
        {
            if (_playerbetLimit==null)
            {
                _playerbetLimit = _userRepos.GetPlayerbetLimit(WebUtil.GetSessionInfo().UserId);
            }
            return _playerbetLimit;
        }
    }
}