<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Import Namespace="Sabong.Business" %>

<%@ Register Src="~/Controls/VideoPlayer.ascx" TagName="VideoPlayer" TagPrefix="uc1" %>

<%@ Register Src="Controls/ChartControls.ascx" TagName="ChartControls" TagPrefix="uc2" %>

<%@ Register src="Controls/MatchInfo.ascx" tagname="MatchInfo" tagprefix="uc3" %>
<%@ Register src="Controls/BetSlip.ascx" tagname="BetSlip" tagprefix="uc4" %>

<%@ Register src="Controls/BetTranactions.ascx" tagname="BetTranactions" tagprefix="uc5" %>

<%@ Register src="Controls/SelectedBet.ascx" tagname="SelectedBet" tagprefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeftColumn" runat="Server">
    <div class="lefttop upper" id="">
        <marquee behaviour="scroll" id="site-anouncement" style="width:100%;">
            <%=RunningAnnoucement %>
        </marquee>
    </div>
    <uc1:VideoPlayer ID="VideoPlayer1" runat="server" />
    <uc2:ChartControls ID="ChartControls1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRightColumn" runat="Server">
    <div class="righttop">
        <% if (Match!=null)
           {%>
        <span class="number" id="match-number"><%=Match!=null ? Match.match_order.ToString():""%></span>
        <span class="text" id="match-des"><%=Match.GetMatchStatus()==MatchStatus.Confirmed?string.Format("Betting for fight {0} is closing soon",Match.match_no):"" %></span>
           <%}
           else
           {%>
            <span class="number" id="match-number">Match</span>
            <span class="text" id="match-des"></span>
           <%} %>   
    </div>
    <div class="matchscore">
        <uc3:MatchInfo ID="MatchInfo" runat="server" />
        <uc4:BetSlip ID="BetSlip1" runat="server" />
        <uc6:SelectedBet ID="SelectedBet1" runat="server" />
        <uc5:BetTranactions ID="BetTranactions1" runat="server" />
    </div>
</asp:Content>

