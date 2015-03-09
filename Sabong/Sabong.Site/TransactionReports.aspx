<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionReports.aspx.cs" Inherits="TransactionReports" %>
<%@ Import Namespace="Sabong.Business" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Scripts/jquery-ui/jquery-ui.min.css" rel="stylesheet" />
    <link href="css/report-lite.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.9.1.min.js"></script>
    <script src="/Scripts/jquery-ui/jquery-ui.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#<%=txtDate.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy"
            });
            $("#calendar-ico1").click(function () {
                $('#<%=txtDate.ClientID%>').datepicker("show");
            });

            $("#<%=txtEndDate.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy"
            });
            $("#calendar-ico2").click(function () {
                $('#<%=txtEndDate.ClientID%>').datepicker("show");
            });
        })
    </script>
    <style type="text/css">
        td.col-name {
            height: 22px;
        }

        .table-ctn td {
            height: 22px;
            border: 1px solid #aeafae;
            font-size: 12px;
            text-align: center;
            padding: 10px 3px;
        }

        .bold {
            font-weight: bold;
        }

        .normal {
            font-weight: normal;
        }

        .text-right {
            text-align: right !important;
        }

        .text-left {
            text-align: left !important;
        }

        .text-top {
            vertical-align: top;
        }

        .rp-arena {
            color: #888;
        }

        .rp-matchno {
        }

        .lose {
            color: red;
        }

        .rp-ip {
            vertical-align: bottom;
            color: #2a37f1;
            font-weight: bold;
        }

        .bet-meron {
            color: #d41527;
            font-weight: bold;
        }

        .bet-wala {
            color: #023eff;
            font-weight: bold;
        }

        .bet-dbb, .bet-ftd {
            color: #c65900;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="rightcolcontainer">
            <div class="report-title strong">TRANSACTION REPORT</div>
            <div class="report-filter-bar">
                <table style="margin-top: 8px">
                    <tr>
                        <td class="input-title">Start Date:</td>
                        <td>
                            <asp:TextBox ReadOnly="False" ID="txtDate" runat="server" CssClass="input-text"></asp:TextBox>
                            <img src="/images/ico_calendar.jpg" class="calendar-ico" id="calendar-ico1" />
                        </td>
                        <td class="input-title">End Date:
                        </td>
                        <td>
                            <asp:TextBox ReadOnly="False" ID="txtEndDate" runat="server" CssClass="input-text"></asp:TextBox>
                            <img src="/images/ico_calendar.jpg" class="calendar-ico" id="calendar-ico2" />
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" CssClass="input-btn" runat="server" Text="Sumit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="table-ctn">
                <table style="width: 100%; padding: 0px; margin: 0px; margin-top: 20px;">
                    <tr class="tbl-header">
                        <td colspan="7" class="text-left">Player Bet List (<%=SessionInfo.User.username%>) - <%=txtDate.Text %> --> <%=txtEndDate.Text %></td>
                    </tr>
                    <tr>
                        <td class="col-name">#</td>
                        <td class="col-name">Trans Time</td>
                        <td class="col-name">Choice</td>
                        <td class="col-name">Odds</td>
                        <td class="col-name">Amount</td>
                        <td class="col-name">Result</td>
                        <td class="col-name">Status</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptReport">
                        <ItemTemplate>
                            <tr class="data-row <%# Container.ItemIndex%2 == 0 ?"event-row":string.Empty %>">
                                <td><%#Container.ItemIndex+1 %></td>
                                <td class="bold">Ref No:TXN120<%#Eval("id") %>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="normal"><%#Eval("date") %></div>
                                </td>
                                <td class="text-right text-top">
                                    <span style="color: gray">(Bet on)<span class="bet-<%#Eval("cocktype").ToString().ToLower() %>"><%#Eval("cocktype") %></span></span><br />
                                    <%#BuildMatchName(Eval("matchname").ToString()) %>
                                    <div class="rp-arena"><%#Eval("arena") %></div>
                                    <div>Match No:<span class="bold"><%#Eval("matchno") %></span></div>
                                </td>
                                <td class="bold text-top">
                                    <%#Eval("odds") %>
                                </td>
                                <td class="bold text-right text-top">
                                    <%#Eval("acceptedamount") %>
                                </td>
                                <td class="text-right text-top">
                                    <div class="bold <%#Eval("Status").ToString().ToLower() %>"><%#Eval("winloseamnt") %></div>
                                    <div class="bold"><%#Eval("betcomamt") %></div>
                                </td>
                                <td class="text-top">
                                    <div class="bold"><%#Eval("Status") %></div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="rp-ip"><%#Eval("ip") %></div>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr style="background: #e9e0ca;">
                                <td colspan="5" style="text-align: right; font-weight: bold;">Subtotal(win/lost):<br>
                                    Subtotal(Com.):<br>
                                    Total:</td>
                                <td style="text-align: right; font-weight: bold;"><span style="color: red;"><%#totalWin.NumberTostring(false) %></span><br>
                                    <%#totalCom.NumberTostring(false) %><br>
                                    <span style="color: red;"><%#(totalWin+totalCom).NumberTostring(false) %></span></td>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
