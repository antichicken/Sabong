<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MatchHistory.aspx.cs" Inherits="MatchHistory" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="Sabong.Repository.EntityModel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Scripts/jquery-ui/jquery-ui.min.css" rel="stylesheet" />
    <link href="css/report-lite.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.9.1.min.js"></script>
    <script src="/Scripts/jquery-ui/jquery-ui.min.js"></script>
    <script>
        $(document).ready(function() {
            $("#<%=txtDate.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy"
            });
            $("#calendar-ico").click(function () {
                $('#<%=txtDate.ClientID%>').datepicker("show");
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="rightcolcontainer">
            <div class="report-title">Match History Report</div>
            <div class="report-filter-bar">
                <table style="margin-top: 8px">
                    <tr>
                        <td class="input-title">Select Date:</td>
                        <td>
                            <%--<input type="text" class="input-text" id="select-date" />--%>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="input-text"></asp:TextBox>
                            <img src="/images/ico_calendar.jpg" class="calendar-ico" id="calendar-ico" />
                        </td>
                        <td class="input-title">Select Arena:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlArena" CssClass="input-select" runat="server"></asp:DropDownList>
                            <%--<select class="input-select">
                                <option>1</option>
                                <option>3</option>
                                <option>3</option>
                            </select>--%>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" CssClass="input-btn" runat="server" Text="Sumit" OnClick="btnSearch_Click" />
                            <%--<input type="button" class="input-btn" value="Sumit" />--%>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="table-ctn">
                <table style="width: 100%; padding: 0px; margin: 0px; margin-top: 20px;">
                    <tr class="tbl-header">
                        <td colspan="7" style="text-align: left">
                            <% if (MatchCreatestart != null)
                               { %>
                                   <%= ddlArena.SelectedItem.Text +" - "+MatchCreatestart.description + (MatchCreatestart.fighttype == 0 ? "(OPEN FIGHT) -	" : "(TOURNAMENT FIGHT) -	") + MatchCreatestart.create_date.ToString("dd/MM/yyyy") + string.Format("({0}) --> ", MatchCreatestart.time) + (MatchCreatestart.enddate == DateTime.MaxValue || MatchCreatestart.enddate == DateTime.MinValue ? "..." : MatchCreatestart.enddate.ToString("dd/MM/yyyy")+ string.Format("({0})", MatchCreatestart.endtime))%>
                               <% }
                               else
                               {%>
                                   <%= ddlArena.SelectedItem.Text %>
                               <%}%>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="col-name">#</td>
                        <td class="col-name">Match</td>
                        <td class="col-name">Start Time</td>
                        <td class="col-name">End Time</td>
                        <td class="col-name">Fight Duration</td>
                        <td class="col-name">Winner</td>
                        <td class="col-name">Match Result</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptReport">
                        <ItemTemplate>
                            <tr class="data-row <%# Container.ItemIndex%2 == 0 ?"event-row":string.Empty %>">
                                <td><%#Eval("match_no") %></td>
                                <td class="<%#MatchCancel((view_matchResult)Container.DataItem) %>"><span class="meron"><%#Eval("cname") %></span>  vs <span class="wala"><%#Eval("agname") %></span></td>
                                <td><%#UnixTimeStampToDateTime(Eval("starttime").ToString()) %></td>
                                <td><%#UnixTimeStampToDateTime(Eval("stoptime").ToString()) %></td>
                                <td><%#GetDuration(Eval("starttime").ToString(),Eval("stoptime").ToString()) %></td>
                                <td><%#WinnerName((view_matchResult)Container.DataItem) %></td>
                                <td style="text-transform: uppercase"><%#MatchResult((view_matchResult)Container.DataItem) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                </table>
            </div>
        </div>
    </form>
</body>
</html>
