<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyWinLoss.aspx.cs" Inherits="DailyWinLoss" %>

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
            $("#start-date").datepicker({
                dateFormat: "dd/mm/yy"
            });
            $("#calendar-start").click(function () {
                $('#start-date').datepicker("show");
            });
            $("#end-date").datepicker({
                dateFormat: "dd/mm/yy"
            });
            $("#calendar-end").click(function () {
                $('#end-date').datepicker("show");
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="rightcolcontainer">
            <div class="report-title">WIN/LOSS BY BET TYPE</div>
            <div class="report-filter-bar">
                <table style="margin-top: 8px">
                    <tr>
                        <td class="input-title">Start Date:</td>
                        <td>
                            <input type="text" class="input-text" id="start-date" />
                            <img src="/images/ico_calendar.jpg" class="calendar-ico" id="calendar-start" />
                        </td>
                        <td class="input-title">End Date:
                        </td>
                        <td>
                            <input type="text" class="input-text" id="end-date" />
                            <img src="/images/ico_calendar.jpg" class="calendar-ico" id="calendar-end" />
                        </td>
                        <td>
                            <input type="button" class="input-btn" value="Sumit" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="table-ctn">
                <table style="width: 100%; padding: 0px; margin: 0px; margin-top: 20px;">
                    <tr class="tbl-header">
                        <td colspan="19">Win Loss By Bet Type - 05/03/2015 --> 05/03/2015
                        </td>
                    </tr>
                    <tr style="line-height: 1.0;">
                        <td rowspan="2" class="transfertableheader">#</td>
                        <td rowspan="2" class="transfertableheader">Type</td>

                        <td rowspan="2" class="transfertableheader">Turnover</td>

                        <td rowspan="2" class="transfertableheader">Gross Comm.</td>

                        <td colspan="3" class="transfertableheader">Member   </td>

                        <td rowspan="2" class="transfertableheader">Company</td>

                    </tr>
                    <tr>
                        <td class="winlosesubheader">Win / Loss</td>
                        <td class="winlosesubheader">Comm.</td>

                        <td class="winlosesubheader">Total </td>


                    </tr>
                    <tr style="background: #c4bd95; text-align: right; font-weight: bold;">

                        <td colspan="2" class="td_bg" align="center">Grand Total</td>


                        <td class="td_bg"><span>0.00</span></td>
                        <td class="td_bg"><span>0.00</span></td>

                        <td class="td_bg1"><span>0.00</span></td>
                        <td class="td_bg1"><span>0.00</span></td>
                        <td class="td_bg1"><span>0.00</span></td>
                        <td class="td_bg"><span>0.00</span></td>


                    </tr>
                    <tr class="data-row">
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="data-row event-row">
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
