using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sabong.Repository.EntityModel;
using Sabong.Repository.Repo;

namespace Sabong.Business
{
    public class FetchAnnouncement
    {
        private string _currentRunningAnnouncement = string.Empty;
        private matchending_announcement _currentMatchendingAnnouncement = null;
        private List<string> _currentChartData = null;
        private view_matchdetail _currentMatch = null;

        public Dictionary<MatchStatus, string> _dicStatus;
        public void DictMatchStatusAddMessage()
        {
            _dicStatus.Add(MatchStatus.Cancel, "Match Cancel");
            _dicStatus.Add(MatchStatus.ClosingSoon, "Bet closing soon");
            _dicStatus.Add(MatchStatus.Confirmed, "Meron/Wala Confirmed");
            _dicStatus.Add(MatchStatus.MatchEnd, "Match End");
            _dicStatus.Add(MatchStatus.MatchNotFound, "Match not Found");
            _dicStatus.Add(MatchStatus.MatchStarted, "Match Start- Market Expire");
            _dicStatus.Add(MatchStatus.StopBet, "Market Expire");
            _dicStatus.Add(MatchStatus.UnConfirmed, "Meron/ Wala Unconfirmed");


        }

        private static FetchAnnouncement _instance;

        public static FetchAnnouncement Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FetchAnnouncement();
                }
                return _instance;
            }
        }

        #region Repos

        private readonly AnnoucementRunningRepository _annoucementRunningRepos = new AnnoucementRunningRepository();
        private readonly MatchRepository _matchRepository = new MatchRepository();

        #endregion

        private bool Started = false;

        public void Start()
        {
            if (!Started)
            {
                Started = true;
                var thread = new Thread(InternalLoop);
                thread.Start();
            }
        }

        private void InternalLoop()
        {
            //match
            //match confirm
            //Running
            //chart
            GetRunningAnnoucement();
            GetMatchEndingAnnouncemen();
            GetChartInfo();
            GetMatchInfo();
            Thread.Sleep(2000);
            InternalLoop();
        }

        private void GetRunningAnnoucement()
        {
            try
            {
                var x = _annoucementRunningRepos.GetLatest();
                if (_currentRunningAnnouncement != null && _currentRunningAnnouncement != x)
                {
                    try
                    {
                        NodeHelper.SendToNode(new
                        {
                            type = "running",
                            message = new {en = x, vn = x}
                        });
                        _currentRunningAnnouncement = x;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void GetMatchEndingAnnouncemen()
        {
            try
            {
                var x = _matchRepository.GetMatchendingAnnouncement();
                if (x != null)
                {
                    if (x!=_currentMatchendingAnnouncement)
                    {
                        try
                        {
                            NodeHelper.SendToNode(new
                            {
                                type = "matchend",
                                match = x.slno,
                                message = new
                                {
                                    en = x.announcement,
                                    vn = x.announcement
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                        }
                        _currentMatchendingAnnouncement = x;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GetChartInfo()
        {
            try
            {
                var x = _matchRepository.GetFightAssignsByDate();
                if (_currentChartData == null || x.Count != _currentChartData.Count)
                {
                    try
                    {
                        NodeHelper.SendToNode(new
                        {
                            type = "chart",
                            charData = x
                        });
                        _currentChartData = x;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void GetMatchInfo()
        {
            try
            {
                string oddStatus;
                var match = _matchRepository.GetCurrentMatch(out oddStatus);
                if (match != null)
                {
                    if (!match.AreEqual(_currentMatch))
                    {
                        try
                        {
                            var matchStatus = match.GetMatchStatus();
                            if (matchStatus==MatchStatus.Confirmed || matchStatus==MatchStatus.ClosingSoon)
                            {
                                oddStatus = "Meron / Wala confirmed";
                            }
                            else
                            {
                                oddStatus = "Meron / Wala unconfirm";
                            }
                            NodeHelper.SendToNode(new
                            {
                                type=(_currentMatch==null ||match.fslno !=_currentMatch.fslno)?"match-next":"match-change",
                                matchinfo = new
                                {
                                    match=match.fslno,
                                    matchnumber=match.match_no,
                                    meron_rate=match.C1odds,
                                    wala_rate=match.C2odds,
                                    draw_rate=match.drawwodds,
                                    ftd_rate=match.ftd,
                                    confirm = new
                                    {
                                        en = matchStatus == MatchStatus.Confirmed ? "Meron / Wala confirmed" : "Meron / Wala unconfirm",
                                        vn = oddStatus
                                    },
                                    top = new
                                    {
                                        en = matchStatus == MatchStatus.Confirmed?string.Format("Betting for fight {0} is closing soon",match.match_no):"",
                                        vn = matchStatus == MatchStatus.Confirmed ? string.Format("Betting for fight {0} is closing soon", match.match_no) : ""
                                    },
                                    match_status=matchStatus.ToString(),
                                    meron_image=match.cock_type.ToLower()=="wala"? match.agimage : match.cimage,
                                    wala_img = match.cock_type.ToLower() == "wala" ? match.cimage : match.agimage,
                                    meron_name = match.cock_type.ToLower() == "wala" ? match.agname : match.cname,
                                    wala_name = match.cock_type.ToLower() == "wala" ? match.cname : match.agname,
                                }
                            });
                            _currentMatch = match;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

            }
            catch (Exception)
            {

            }
        }
    }
}
