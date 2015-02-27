<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            background-color: #000;
        }

        @font-face {
            font-family: "SourceSansPro-Bold";
            font-style: normal;
            font-weight: normal;
            src: url("font/SourceSansPro-Bold.eot?#iefix") format("embedded-opentype"), url("/fonts/SourceSansPro-Bold.woff") format("woff"), url("/fonts/SourceSansPro-Bold.ttf") format("truetype"), url("/fonts/SourceSansPro-Bold.svg") format("svg");
        }

        @font-face {
            font-family: "SourceSansPro-Regular";
            font-style: normal;
            font-weight: normal;
            src: url("/fonts/SourceSansPro-Regular.eot?#iefix") format("embedded-opentype"), url("/fonts/SourceSansPro-Regular.woff") format("woff"), url("/fonts/SourceSansPro-Regular.ttf") format("truetype"), url("/fonts/SourceSansPro-Regular.svg") format("svg");
        }

        .login-form-wrap {
            background: url("/images/cockbg1.png") no-repeat scroll center center;
            height: 345px;
            margin: 10% auto auto;
            width: 300px;
        }

        .welcome {
            color: #bfbfbf;
            float: left;
            font-family: "SourceSansPro-Bold";
            font-size: 18px;
            height: 26px;
            margin-top: 0;
            padding-top: 10px;
            text-align: center;
            text-shadow: 1px 1px 1px #000;
            width: 296px;
        }

        .uname {
            float: left;
            height: auto;
            margin-left: 60px;
            margin-top: 38px;
            width: 240px;
        }

        input {
            background: none repeat scroll 0 0 rgba(0, 0, 0, 0);
            border: medium none;
            color: #5e5e5e;
            font-family: "SourceSansPro-Regular";
            font-size: 14px;
            height: 28px;
            padding-left: 10px;
            width: 82%;
        }

        .pwd {
            float: left;
            height: auto;
            margin-left: 60px;
            margin-top: 8px;
            width: 240px;
        }

        .signin {
            float: left;
            height: 42px;
            margin-left: 32px;
            margin-top: 26px;
            width: 280px;
        }

        .verification {
            float: left;
            height: auto;
            margin-left: 30px;
            margin-top: 20px;
            width: 252px;
        }

        .vcode {
            float: left;
            height: auto;
            width: 110px;
        }

        .captcha {
            float: left;
            height: 35px;
            margin-left: 40px;
            width: 100px;
        }

        .schedule {
            cursor: pointer;
            margin: 30px auto auto;
            text-align: center;
            width: 300px;
        }

        .error-panel {
            background: none repeat scroll 0 0 #1f1f1f;
            position: absolute;
            top: 0;
            width: 300px;
            left: 50%;
            margin-left: -150px;
        }

        .error {
            float: left;
            color: #F80000;
        }

        .error-panel {
            position: absolute;
            width: 300px;
            background: #1F1F1F;
            top: 0;
        }

            .error-panel label {
                margin: 0 0 0 44px;
            }

                .error-panel label:first-child {
                    margin-top: 10px;
                }
    </style>
    <link href="/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/jquery.validate.min.js"></script>
    <script type="text/javascript">
        $(function(){
            $('#loginpanel_form').validate({
                onfocusout: true,
                onkeyup: true,
                onclick: true,
                focusInvalid: true,
                debug:true,
                // Specify the validation rules
                rules: {
                    username: "required",
                    password: {
                        required: true,
                        minlength: 6
                    },
                    vercode: {
                        required: true,
                        minlength: 4
                    }
                },
                // Specify the validation error messages
                messages: {
                    username: "Please enter your username",
                    password: {
                        required: "Please enter your password",
                        minlength: "Password Minimum 6 characters"
                    },
                    vercode: {
                        required: "Please enter the verification code",
                        minlength: "Verifycode check at least 4 characters"
                    }
                },
                errorPlacement: function(error, element) {
                    //  if($.trim( $(".error-panel").html()) && $('.error-panel').hasClass('alerted')){
                    if($(".error-panel").outerHeight() > 50)
                        $(".error-panel").html('');

                    error.appendTo("div.error-panel");
                    // }else{
                    //      error.appendTo("div.error-panel");
                    //      $(".error-panel").addClass('alerted');
                    //   }
                },
                submitHandler: function(form) {
                    form.submit();
                    /**   var formData = new FormData($('#loginpanel_form')[0]);
                     // alert('OK');
                     $.ajax({
                url:$('#loginpanel_form').attr('action'),
                type: 'POST',
                data: formData,
                async: false,
                success: function (data) {
                    var obj = jQuery.parseJSON(data);

                    if(!obj.status){
                        alert(obj.msg);
                    }else{
                        window.location = "https://119.9.104.57/"+obj.url;
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            }); */

                }

            });
        });

    </script>
</head>
<body>
    <div class="error-panel">
        <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>  
    </div>
    <form id="loginpanel_form" runat="server">
        <div class="login-form-wrap">
            <div class="welcome">Welcome</div>
            <div class="uname">
                <asp:TextBox CssClass="login_usr" Style="" MaxLength="30" autocomplete="off" placeholder="Username" ID="username" runat="server"></asp:TextBox>
            </div>
            <div class="pwd">
                <asp:TextBox ID="password" CssClass="login_pwd" runat="server" Style="" MaxLength="30" autocomplete="off" placeholder="Password" TextMode="Password"></asp:TextBox>
            </div>
            <div class="verification">
                <div class="vcode">
                    <asp:TextBox ID="vercode" runat="server" Style="width: 122px;" MaxLength="30" autocomplete="off" value="" placeholder="Verify Code"></asp:TextBox>
                </div>
                <div class="captcha">
                    <div style="float: left; height: 35px;" id="imgdiv">
                        <img style="height: 35px; width: 68px;" src="/Controls/myCaptcha.aspx?t=123" id="captcha">
                    </div>
                    <img style="width: 23px; float: left; margin-left: 3px; margin-top: 5px;" src="/images/reload.png" onclick="RefreshCaptcha();">
                    <script type="text/javascript">
                    function RefreshCaptcha() {
                        var img = document.getElementById('captcha');
                        var url = img.src.substring(0, img.src.indexOf("?"));
                        img.src = url + "?t=" + Math.random();
                    }
                    </script>
                </div>
            </div>
            <div class="signin">
                <a style="text-decoration: none; display: none;" href="adduser.php">New User Signup</a>
                <asp:Button ID="btnLogin" CssClass="button1 btn_login" runat="server" Text="Sign In" OnClick="btnLogin_Click" Style="margin-left: 0px; height: 30px; color: #fff; background: none; border: none; cursor: pointer; font-size: 15px; font-family: Arial, Helvetica, sans-serif; text-shadow: 0px 1px 1px #000; padding-top: 10px;" />
            </div>
        </div>
        <div style="position: relative; margin-top: 10px;" class="schedule en">
            <img onclick="window.open('calenderview.php','_newtab','width=920, height=800,scrollbars=1');" style="width: 65%;" src="/images/schedule1.png">
            <div onclick="window.open('calenderview.php','_newtab','width=920, height=800,scrollbars=1');" style="width: 150px; height: 22px; float: left; padding-top: 8px; position: absolute; z-index: 99; top: 5px; left: 60px; font-family: arial; font-size: 12px; text-shadow: 1px 1px 1px #fff; color: #000;">
                Click here for schedule
            </div>
        </div>
    </form>
</body>
</html>
