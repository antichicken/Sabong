<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MatchHistory.aspx.cs" Inherits="MatchHistory" %>

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
            $("#select-date").datepicker({
                dateFormat: "dd/mm/yy"
            });
            $("#calendar-ico").click(function () {
                $('#select-date').datepicker("show");
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
                            <input type="text" class="input-text" id="select-date" />
                            <img src="/images/ico_calendar.jpg" class="calendar-ico" id="calendar-ico" />
                        </td>
                        <td class="input-title">Select Arena:
                        </td>
                        <td>
                            <select class="input-select">
                                <option>1</option>
                                <option>3</option>
                                <option>3</option>
                            </select>
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
                        <td colspan="7"></td>
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
                    <tr class="data-row">
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
                    </tr>
                    <tr class="data-row">
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
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
