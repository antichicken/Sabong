<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Controls/VideoPlayer.ascx" TagName="VideoPlayer" TagPrefix="uc1" %>

<%@ Register Src="Controls/ChartControls.ascx" TagName="ChartControls" TagPrefix="uc2" %>

<%@ Register src="Controls/MatchInfo.ascx" tagname="MatchInfo" tagprefix="uc3" %>
<%@ Register src="Controls/BetSlip.ascx" tagname="BetSlip" tagprefix="uc4" %>

<%@ Register src="Controls/BetTranactions.ascx" tagname="BetTranactions" tagprefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeftColumn" runat="Server">
    <div class="lefttop">YOU ARE WATCHING PSG 3-BULLSTAG</div>
    <uc1:VideoPlayer ID="VideoPlayer1" runat="server" />
    <uc2:ChartControls ID="ChartControls1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRightColumn" runat="Server">
    <div class="righttop">
        <span class="number">16</span>
        <span class="text">Betting for fight 16 is closing soon</span>
    </div>
    <div class="matchscore">
        <uc3:MatchInfo ID="MatchInfo1" runat="server" />
        <uc4:BetSlip ID="BetSlip1" runat="server" />
        <uc5:BetTranactions ID="BetTranactions1" runat="server" />
    </div>
</asp:Content>

