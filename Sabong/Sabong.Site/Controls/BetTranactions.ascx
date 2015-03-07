<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BetTranactions.ascx.cs" Inherits="Controls_BetTranactions" %>
<div class="betsaccepted">
    <div class="betsaccepted-title">BETS ACCEPTED <span class="betsaccepted-close" onclick="window.open('TransactionReports.aspx','_newtab','width=920, height=800,scrollbars=1');">View Transaction History</span></div>
    <div class="betsaccepted-content">
        <table cellspacing="0" cellpadding="0" id="accepted_bet">
            <tbody>
                <tr class="betsaccepted-th">
                    <th>Match</th>
                    <th>Bet Selection</th>
                    <th></th>
                    <th>Odds</th>
                </tr>
                <asp:Repeater ID="rptBetList" runat="server">
                    <ItemTemplate>
                        <tr class="betsaccepted-td <%#Eval("cocktype")%>" id="a_<%#Eval("id") %>">
                            <td><%#Eval("matchno") %></td>
                            <td><%#Eval("cocktype") %></td>
                            <td><%#Eval("acceptedamount") %></td>
                            <td><%#Eval("odds") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</div>
