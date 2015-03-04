<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePass.aspx.cs" Inherits="ChangePass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>..::Cockfight::..</title>
    <style type="text/css">
        .container {
            width: 100%;
            overflow: hidden;
            height: auto;
        }

        .rightcolcontainer {
            width: 100%;
            height: auto;
            background-color: #fff;
            margin-left: 15px;
            margin-top: 15px;
            margin-right: 35px;
        }
        .rightcolcontainer td {
            color: #000;
            font-weight: bold;
        }
        .rightcolcontainer td {
            height: 22px;
            border: 1px solid #aeafae;
            padding-left: 3px;
            padding-right: 3px;
            font-size: 12px;
        }
        .clearfix:before,
        .clearfix:after {
            content: " "; /* 1 */
            display: table; /* 2 */
        }

        .clearfix:after {
            clear: both;
        }

        .clearfix {
            *zoom: 1;
        }
    </style>
    <link href="/css/layout.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="rightcolcontainer clearfix" style="width: 514px;">
                <div class="red2 strong head6">Change Password</div>
                <div class="graycontainer"></div>
                <div class="sub_content" style="border: none; background: none; padding: 0; margin-left: 0;">
                    <div>
                        <div>Password must have 8 to 15 characters without white-space and contain the following:</div>
                        • Uppercase letter [A-Z]<br>
                        • Numeric [0-9]
                        <div>For example: Aui123456, Adheufkvn0</div>
                        <div>Please note that your password is cAsE sEnSiTiVe and must not contain any special characters (!,@,#,etc..)</div>
                    </div>
                    <table height="200" style="width: 100%; color: #000; background: #f8f0e5;">
                        <tr style="height: 25px">
                            <input type="hidden" name="hd_userval" id="hd_userval" value="1484">
                            <td colspan="2" class="transfertableheader" style="font-size: 15px; color: #fff;">Change Password</td>
                        </tr>
                        <tr>
                            <td>Old Password</td>
                            <td>
                                <input type="password" name="password" id="oldpassword" class="form" onblur="return getpassword();" maxlength="15">
                            </td>
                        </tr>
                        <tr>
                            <td>New Password</td>
                            <td>
                                <input type="password" id="npass" name="npassword" class="form" maxlength="15">
                            </td>
                        </tr>
                        <tr>
                            <td>Confirm Password</td>
                            <td>
                                <input type="password" id="cpass" name="cpassword" class="form" maxlength="15">
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <input type="submit" name="submit" value="Update" class="submitbtn2" style="float: left;">
                                <input type="reset" name="" value="Reset" class="submitbtn2" style="float: left; margin-left: 20px;"></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
