$(function() {
    $('.video-quality').click(function() {
        url = 'rtmp://stream-edge-vn2.s128.net/live/';
        vid = "myStream";
        var screen = "uniform";
        if ($(this).data('type') == 1) {
            vid = "myStream";
        } else if ($(this).data('type') == 2) {
            vid = "myStream_360p";
            screen = "none";
        } else if ($(this).data('type') == 3) {
            vid = "myStream_160p";
            screen = "none";
        } else if ($(this).data('type') == 4) {
            vid = "myStream_90p";
            screen = "none";
        }

        jwplayer('playerlOrdvdydtRFi').setup({
            file: url + vid + '?wowzasessionid=v01010101',
            width: '100%',
            aspectratio: '16:10',
            autostart: true, // Added auto start
            stretching: screen,
            primary: 'flash',
            rtmp: {
                bufferlength: 2
            },
        });
    });
});

$(document).ready(function () {
    NotificationPoll();
    $('.betslip-close').click(function() {
        $(this).closest('.betslip').hide();
    });
    $('.disabled').click(function (e) {
        e.stopPropagation();
    });
    $('#choose-meron, #choose-wala, #choose-draw,#choose-ftd').click(function (e) {
        if ($(this).closest('.disabled').length>0) {
            return false;
        }
        $('.betslip').show();
        $('#input-stake').val('');
        $('#input-stake').focus();
        var des = "";
        var betInfo = new BettingInfo();
        betInfo.MatchId = $('#match-id').val();
        var rate=$(this).text();
        betInfo.OddRate = rate;
        if (e.target.id == "choose-meron") {
            des += $('#meron-name').text() + " MERON @" + rate;
            betInfo.Bettype = 0;
            betInfo.CockId = $(this).attr('data-cock');
            if (playerLimit) {
                $('#input-stake').attr('placeholder',playerLimit.minbet_meron);
            }
        }
        if (e.target.id == "choose-wala") {
            des += $('#wala-name').text() + " WALA @" + rate;
            betInfo.Bettype = 1;
            betInfo.CockId = $(this).attr('data-cock');
            if (playerLimit) {
                $('#input-stake').attr('placeholder', playerLimit.minbet_wala);
            }
        }
        if (e.target.id == "choose-draw") {
            des += "DRAW @" + rate;
            betInfo.Bettype = 2;
            if (playerLimit) {
                $('#input-stake').attr('placeholder', playerLimit.minbet_draw);
            }
        }
        if (e.target.id == "choose-ftd") {
            des += "FTD @" + rate;
            betInfo.Bettype = 3;
            if (playerLimit) {
                $('#input-stake').attr('placeholder', playerLimit.minbet_draw);
            }
        }
        betInfo.Stake = $('#input-stake').val();
        $('#betInfo').val(JSON.stringify(betInfo));
        $('#bet-description').text(des);
    });
    $('#input-stake').change(function(e) {
        var betInfo = new BettingInfo();
        if ($('#betInfo').val().length>0) {
            betInfo = $.parseJSON($('#betInfo').val());
        }
        betInfo.Stake = $(this).val();
        $('#betInfo').val(JSON.stringify(betInfo));
    });
    $('#change-pass').click(function () {
        $('#newpass, #newpasscf, #oldpass').removeClass('input-err').val('');
        $('.error').addClass('hidden');
        $('#change-pass-form').dialog({
            modal: true,
            width: 400,
            open: function () {
                $(this).parent().find('.ui-dialog-buttonset button:eq(0)').focus();
            },
            buttons: {
                Update: function () {
                    changePass($('#oldpass').val(), $('#newpass').val(), $('#newpasscf').val());
                },
                Reset: function () {
                    $('#newpass, #newpasscf, #oldpass').removeClass('input-err').val('');
                    $('.error').addClass('hidden');
                }
            }
        });
        return false;
    });

    function changePass(p1, p2, p3) {
        if (p1.length == 0) {
            $('#oldpass').addClass('input-err').next().removeClass('hidden').html('Enter the old password.');
            return false;
        } else {
            $('#oldpass').removeClass('input-err').next().addClass('hidden').html('');
        }
        if (p2.length == 0) {
            $('#newpass').addClass('input-err').next().removeClass('hidden').html('Enter your new password.');
            return false;
        } else {
            $('#newpass').removeClass('input-err').next().addClass('hidden').html('');
        }
        if (p3.length == 0) {
            $('#newpasscf').addClass('input-err').next().removeClass('hidden').html('Confirm your new password.');
            return false;
        } else {
            $('#newpasscf').removeClass('input-err').next().addClass('hidden').html('');
        }
        if (p2!=p3) {
            $('#newpass, #newpasscf').addClass('input-err').next().removeClass('hidden').html('New password should be equal to confirm password.');
            return false;
        } else {
            $('#newpass, #newpasscf').removeClass('input-err').next().addClass('hidden').html('');
        }
        if (/[^a-zA-Z0-9]/.test(p2)) {
            $('#newpass, #newpasscf').addClass('input-err').next().removeClass('hidden').html('Enter your password without space special character.');
            return false;
        } else {
            $('#newpass, #newpasscf').removeClass('input-err').next().addClass('hidden').html('');
        }

        if (!/[A-Z]/.test(p2)) {
            $('#newpass, #newpasscf').addClass('input-err').next().removeClass('hidden').html('Password must have at least one capital letter.');
            return false;
        } else {
            $('#newpass, #newpasscf').removeClass('input-err').next().addClass('hidden').html('');
        }
        if (!/[0-9]/.test(p2)) {
            $('#newpass, #newpasscf').addClass('input-err').next().removeClass('hidden').html('Password must have at least one numeric caracter.');
            return false;
        } else {
            $('#newpass, #newpasscf').removeClass('input-err').next().addClass('hidden').html('');
        }

        if (p2.length < 8) {
            $('#newpass, #newpasscf').addClass('input-err').next().removeClass('hidden').html('Password length should be 8-15.');
            return false;
        } else {
            $('#newpass, #newpasscf').removeClass('input-err').next().addClass('hidden').html('');
        }
        $('#newpass, #newpasscf,#oldpass').removeClass('input-err').next().addClass('hidden').html('');
        $('#change-pass-form').dialog('close');
        $('#loading-form').dialog({ modal: true });
        $.ajax({
            url: '/Services/UserServices.ashx',
            data: { current: $('#oldpass').val(), newpass: $('#newpass').val(), confirm: $('#newpasscf').val() },
            type: 'POST',
            success: function(data) {
                data = $.parseJSON(data);
                $('#loading-form').dialog('close');
                if (data.status == 1) {
                    $('#response-message > p').text(data.message);
                    $('#response-message').dialog({modal:true});
                }else if (data.status==-1) {
                    $('#response-message > p').text(data.message);
                    $('#response-message').dialog({
                        modal: true,
                        buttons: {
                            OK: function () {
                                $(this).dialog("destroy");
                                $('#change-pass-form').dialog('open');
                            }
                        }
                    });
                }else if (data.status==0) {
                    $('#response-message > p').text(data.message);
                    $('#response-message').dialog({
                        modal: true,
                        buttons: {
                            OK: function () {
                                $(this).dialog("destroy");
                                window.location = '/Login.aspx';
                            }
                        }
                    });

                    setTimeout(function() {
                        window.location = '/Login.aspx';
                    }, 5000);
                }
            }
        });
    }
    
});

var NotificationPoll = function() {
    $.ajax({
        url: 'http://119.9.104.232:8888/?action=hear&match=' + $('#match-id').val() + '&id=' + $.cookie('sec'),
        timeout:60000,
        success: function (data) {
            if (data.length > 0) {
                try {
                    data = $.parseJSON(data);
                    MatchNotificationHandler(data);
                    BetNotificationHandler(data);
                    GlobalNotificationHandler(data);
                    UserNotificationHandler(data);
                } catch(e) {
                    if (console.log) {
                        console.log(e.message);
                    }
                } 
                
            }
        }
    }).always(function () {
        NotificationPoll();
    });
};

function MatchNotificationHandler(data) {
    if (data.type == 'match-announcement') {
        if (data.pos=='top') {
            $('#match-number').text(data.matchnumber);
            $('#match-des').text(GetMessageByCurrentLang(data.message));
        } else {
            $('#match-confirm').text(GetMessageByCurrentLang(data.message));
        }
    } else if (data.type == "match-next") {
        $('#accepted_bet .betsaccepted-td').remove();
        fillMatchInfo(data);
        resetBetSlip();
    } else if (data.type == "match-change") {
        fillMatchInfo(data);
        resetBetSlip(data);
    }
    
    function resetBetSlip(matchdata) {
        if (matchdata == null) {
            $('#betInfo').val('');
            $('.betslip').hide();
        } else {
            var x = $('#betInfo');
            if (x.length > 0) {
                var betInfo = $.parseJSON(x);
                var des = "";
                if (betInfo.Bettype == 1) {
                    betInfo.OddRate = matchdata.matchinfo.wala_rate;
                    betInfo.CockId = matchdata.matchinfo.acid;
                    des += $('#wala-name').text() + " WALA @" + matchdata.matchinfo.wala_rate;

                } else if (betInfo.Bettype == 0) {
                    betInfo.OddRate = matchdata.matchinfo.meron_rate;
                    betInfo.CockId = matchdata.matchinfo.cid;
                    des += $('#meron-name').text() + " MERON @" + matchdata.matchinfo.meron_rate;

                } else if (betInfo.Bettype == 2) {
                    betInfo.OddRate = matchdata.matchinfo.draw_rate;
                    des += "BDD @" + matchdata.matchinfo.draw_rate;

                } else if (betInfo.Bettype == 3) {
                    betInfo.OddRate = matchdata.matchinfo.ftd_rate;
                    des += "FTD @" + matchdata.matchinfo.ftd_rate;
                }
                $('#betInfo').val(JSON.stringify(betInfo));
                $('#bet-description').text(des);
            }
        }

    }

    function fillMatchInfo(matchdata) {
        $('#match-info-wrap').removeClass('hidden');
        $('#match-id').val(matchdata.matchinfo.match);
        $('#match-number').text(matchdata.matchinfo.matchnumber);
        $('#match-des').text(GetMessageByCurrentLang(matchdata.matchinfo.top));
        $('#wala-image').attr('src', matchdata.matchinfo.wala_img);
        $('#meron-image').attr('src', matchdata.matchinfo.meron_img);
        $('#wala-name').text(matchdata.matchinfo.wala_name);
        $('#meron-name').text(matchdata.matchinfo.meron_name);
        $('#choose-meron').text(matchdata.matchinfo.meron_rate).attr('data-cock', matchdata.matchinfo.cid);
        $('#choose-wala').text(matchdata.matchinfo.wala_rate).attr('data-cock', matchdata.matchinfo.acid);
        $('#choose-draw').text(matchdata.matchinfo.draw_rate);
        $('#choose-ftd').text(matchdata.matchinfo.ftd_rate);
        $('#match-confirm').text(GetMessageByCurrentLang(matchdata.matchinfo.confirm));
        var status = matchdata.matchinfo.match_status;
        if (status == "Confirmed" || status == "ClosingSoon") {
            $('.clickable').removeClass('disabled');
            $('#match-confirm').removeClass('unconfirm');
        } else {
            $('#match-confirm').addClass('unconfirm');
            $('.clickable').addClass('disabled');
            if (status == "Cancel") {
                $('#page-dialog').dialog("destroy");
                $('#page-dialog > p').text("Match no " + matchdata.matchinfo.matchnumber + ' cancelled');
                $('#page-dialog').dialog({ modal: true });
            }
            if (status=="StopBet") {
                $('#accepted_bet .betsaccepted-td').remove();
                clearBetSlip();
            }
        }
    }

    
}
function clearBetSlip() {
    $('.betslip').hide();
    $('#input-stake').val('');
    $('#betInfo').val('');
}
function BetNotificationHandler(data) {
    if (data.type == "selected-bet") {
        if (data.betinfo.length > 0) {
            $('#selected-bet').closest('.betsaccepted').show();
            for (var i = 0; i < data.betinfo.length; i++) {
                var bet = data.betinfo[i];
                var id = '#s_' + bet.betId;
                var tmp = '<tr class="betsaccepted-td" id="s_' + bet.betId + '"><td>' + bet.matchId + '</td><td>' + bet.bet_select + '</td><td>' + bet.stake + '</td><td>' + bet.odds + '</td></tr>';
                if ($(id).length > 0) {
                    $(id).replaceWith(tmp);
                } else {
                    $('#selected-bet').append(tmp);
                }
            }
        }
    }else if (data.type=="accepted-bet") {
        for (var i = 0; i < data.betinfo.length; i++) {
            var bet = data.betinfo[i];
            var sid = '#s_' + bet.betId;
            var aid = '#a_' + bet.betId;
            var tmp = '<tr class="betsaccepted-td" id="a_' + bet.betId + '"><td>' + bet.matchId + '</td><td>' + bet.bet_select + '</td><td>' + bet.stake + '</td><td>' + bet.odds + '</td></tr>';
            $(sid).remove();
            if ($('#selected-bet .betsaccepted-td').length<1) {
                $('#selected-bet').closest('.betsaccepted').hide();
            }
            if ($(aid).length > 0) {
                $(aid).replaceWith(tmp);
            } else {
                $('#accepted_bet').append(tmp);
            }
        }
    }
}

function GlobalNotificationHandler(data) {
    if (data.type=="running") {
        $('#site-anouncement').text(GetMessageByCurrentLang(data.message));
    }
    else if (data.type == "chart") {
        UpdateCredit();
        ResetTable();
        DrawChart(data.chartInfo);
        DrawNormalChart(data.chartInfo);
        $('#banker-win').text(data.banker);
        $('#player-win').text(data.player);
        $('#win-win').text(data.draw);
        $('.bk-content-wrap').scrollLeft(4000);
    }
}

function UserNotificationHandler(data) {
    if (data.type=="loginlogout") {
        $.cookie('sec', null);
        $('#page-dialog > p').html('Account was logged in by another');
        $('#page-dialog').dialog({
            modal: true,
            open: function () {
                $(this).parent().find('.ui-dialog-buttonset button:eq(0)').focus();
            },
            buttons: {
                OK: function () {
                    window.location = "/Login.aspx";
                }
            }
        });
        setTimeout(function () {
            window.location = '/Login.aspx';
        }, 5000);
    }
}

function PlaceBet() {
    if ($('#place-bet').hasClass('disabled')) {
        return false;
    }
    $('#place-bet').attr('disabled', 'disabled');
    
    var betInfo = $.parseJSON($('#betInfo').val());
    betInfo.Stake = $('#input-stake').val();
    if (betInfo.Stake.length < 1) {
        $('#loading-form').dialog('close');
        $('#page-dialog > p').text('Please enter odd stake');
        $('#page-dialog').dialog({ modal: true });
        $('#place-bet').removeAttr('disabled');
        $('#input-stake').focus();
        return false;
    }
    if (!isValidBet()) {
        $('#loading-form').dialog('close');
        $('#page-dialog > p').text("MaxBetExceed");
        $('#page-dialog').dialog({ modal: true });
        $('#place-bet').removeAttr('disabled');
        $('#input-stake').focus();
        return false;
    }

    internalBet(betInfo);

    function internalBet(betinfo) {
        $('#loading-form').dialog({ modal: true });
        $.ajax({
            url: '/Services/BettingHandler.ashx',
            type: 'POST',
            data: { match: betInfo.MatchId, stake: betInfo.Stake, type: betInfo.Bettype, oddrate: betInfo.OddRate,cockid:betinfo.CockId },
            success: function (res) {
                bettingResultHandler($.parseJSON(res));
            }
        }).always(function () {
            $('#loading-form').dialog('close');
            $('#place-bet').removeAttr('disabled');
        });;
    }
    

    function isValidBet() {
        if (playerLimit != null) {
            var vstake = parseFloat(betInfo.Stake);
            if (betInfo.Bettype == 0) {
                if (vstake < playerLimit.minbet_meron || vstake > playerLimit.maxbet_meron) {
                    return false;
                }
                return true;
            } else if (betInfo.Bettype == 1) {
                if (vstake < playerLimit.minbet_wala || vstake > playerLimit.maxbet_wala) {
                    return false;
                }
                return true;
            } else {
                if (vstake < playerLimit.minbet_draw || vstake > playerLimit.maxbet_draw) {
                    return false;
                }
                return true;
            }
        }
        return true;
    }

    function updateTransInfo(data) {
        if (data.Status == 'AcceptBet' || data.Status == 'AcceptAmountAndWaitingReBet') {
            if (data.BetList !=null) {
                for (var i = 0; i < data.BetList.length; i++) {
                    var bet = data.BetList[i];
                    //var sid = '#s_' + bet.id;
                    var aid = '#a_' + bet.id;
                    var tmp = '<tr class="betsaccepted-td ' + bet.cocktype + '" id="a_' + bet.id + '"><td>' + bet.matchno + '</td><td>' + bet.cocktype + '</td><td>' + bet.acceptedamount + '</td><td>' + bet.odds + '</td></tr>';
                    if ($('#selected-bet .betsaccepted-td').length < 1) {
                        $('#selected-bet').closest('.betsaccepted').hide();
                    }
                    if ($(aid).length > 0) {
                        $(aid).replaceWith(tmp);
                    } else {
                        $('#accepted_bet .betsaccepted-th').after(tmp);
                    }
                }
            }
        }
    }



    function bettingResultHandler(result) {
        HandleSystemError(result);
        updateTransInfo(result);
        if (result.Status == "MarketExpire") {
            clearBetSlip();
            $('#page-dialog > p').text("MarketExpire");
            $('#page-dialog').dialog({ modal: true });
        }
        else if (result.Status=="OddValueChange") {
            $('#page-dialog > p').html('Odd value changed.<br/>Do you want to play with new Odd value: '+ result.RateChange);
            $('#page-dialog').dialog({
                modal: true,
                open: function () {
                    $(this).parent().find('.ui-dialog-buttonset button:eq(0)').focus();
                },
                buttons: {
                    "Yes": function () {
                        betInfo.OddRate = result.RateChange;
                        $('#betInfo').val(JSON.stringify(betInfo));
                        
                        internalBet(betInfo);
                        $(this).dialog("destroy");
                    },
                    No: function () {
                        $(this).dialog("destroy");
                    }
                }
            });
        }
        else if (result.Status == "AcceptAmountAndWaitingReBet") {
            UpdateCredit();
            $('#page-dialog > p').html('chap nhan mot phan, phan con lai danh voi odd rate khac <br/> So tien con lai danh voi oddrate moi: ' + result.RemainMoney + '<br/>Rate moi: ' + result.RateChange);
            $('#page-dialog').dialog({
                modal: true,
                open: function() {
                    $(this).parent().find('.ui-dialog-buttonset button:eq(0)').focus();
                },
                buttons: {
                    "Yes": function () {
                        betInfo.OddRate = result.RateChange;
                        betInfo.Stake = result.RemainMoney;
                        $('#betInfo').val(JSON.stringify(betInfo));
                        $('#input-stake').val(result.RemainMoney);
                        
                        internalBet(betInfo);
                        $(this).dialog("destroy");
                    },
                    No: function() {
                        $(this).dialog("destroy");
                        clearBetSlip();
                    }
                }
            });
        }
        else if (result.Status=="WalletNotEnough") {
            $('#page-dialog > p').text("WalletNotEnough");
            $('#page-dialog').dialog({ modal: true });
        }
        else if (result.Status=="MaxBetExceed") {
            $('#page-dialog > p').text("MaxBetExceed");
            $('#page-dialog').dialog({ modal: true });
        }
        else if (result.Status=="MaxPerMatchExceed") {
            $('#page-dialog > p').text("MaxPerMatchExceed");
            $('#page-dialog').dialog({ modal: true });
        }
        else if (result.Status=="MaxWinningExceed") {
            $('#page-dialog > p').text("MaxWinningExceed");
            $('#page-dialog').dialog({ modal: true });
        } else if (result.Status == "AcceptBet") {
            UpdateCredit();
            clearBetSlip();
            $('#page-dialog > p').text("Bet Accepted");
            $('#page-dialog').dialog({ modal: true });
        } else if (result.Status == "MeronWalaUnConfirmed") {
            $('#page-dialog > p').text("MeronWalaUnConfirmed");
            $('#page-dialog').dialog({ modal: true });
        }
    }
}

function HandleSystemError(data) {
    if (data) {
        if (data.type=="error") {
            if (data.code==0) {
                $('#response-message > p').text("Your session is expired or logged in by another, You have to loggin again.");
                $('#response-message').dialog({
                    modal: true,
                    buttons: {
                        OK: function () {
                            $(this).dialog("destroy");
                            window.location = '/Login.aspx';
                        }
                    }
                });

                setTimeout(function () {
                    window.location = '/Login.aspx';
                }, 5000);
            }
            return false;
        }
    }
    return true;
}

var creditTimer;
function UpdateCredit() {
    $.ajax({
        url: '/Services/UserServices.ashx?action=credit',
        success: function(data) {
            data = $.parseJSON(data);
            HandleSystemError(data);
            if (data) {
                $('#given-credit').text(data.GivenCredit);
                $('#profit').text(data.Profit);
                $('#bet-credit').text(data.BetCredit);
            }
        }
    });
    clearTimeout(creditTimer);
    creditTimer = setTimeout(function() { UpdateCredit(); }, 120000);
}

function GetMessageByCurrentLang(message) {
    var lang = "en-us";
    if ($.cookie('user-lang')) {
        lang = $.cookie('user-lang');
    }
    if (lang=="vi-vn") {
        return message.vn;
    }
    return message.en;
}

function BettingInfo() {
    this.MatchId = null;
    this.OddId = null;
    this.OddRate = null;
    this.Bettype = null;
    this.Stake = null;
    this.CockId = '';
}