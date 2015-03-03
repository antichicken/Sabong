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
        var des = "";
        var betInfo = new BettingInfo();
        betInfo.MatchId = $('#match-id').val();
        var rate=$(this).text();
        betInfo.OddRate = rate;
        if (e.target.id == "choose-meron") {
            des += $('#meron-name').text() + " MERON @" + rate;
            betInfo.Bettype = 0;
        }
        if (e.target.id == "choose-wala") {
            des += $('#wala-name').text() + " WALA @" + rate;
            betInfo.Bettype = 1;
        }
        if (e.target.id == "choose-draw") {
            des += "DRAW @" + rate;
            betInfo.Bettype = 2;
        }
        if (e.target.id == "choose-ftd") {
            des += "FTD @" + rate;
            betInfo.Bettype = 3;
        }
        betInfo.Stake = $('#input-stake').val();
        $('#betInfo').val(JSON.stringify(betInfo));
        $('#bet-description').text(des);
    });
    $('#input-stake').keyup(function(e) {
        var betInfo = new BettingInfo();
        if ($('#betInfo').val().length>0) {
            betInfo = $.parseJSON($('#betInfo').val());
        }
        betInfo.Stake = $(this).val();
        $('#betInfo').val(JSON.stringify(betInfo));
    });
});

var NotificationPoll = function() {
    $.ajax({
        url: 'http://localhost:8888/?action=hear&match=' + $('#match-id').val() + '&id='+$.cookie('sec'),
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
    }else if (data.type=="match-next") {
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
                    des += $('#wala-name').text() + " WALA @" + matchdata.matchinfo.wala_rate;

                } else if (betInfo.Bettype == 0) {
                    betInfo.OddRate = matchdata.matchinfo.meron_rate;
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
        $('#choose-meron').text(matchdata.matchinfo.meron_rate);
        $('#choose-wala').text(matchdata.matchinfo.wala_rate);
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

    function clearBetSlip() {
        $('.betslip').hide();
        $('#input-stake').val('');
        $('#betInfo').val('');
    }
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
    }
}

function PlaceBet(stake) {
    if ($('#place-bet').hasClass('disabled')) {
        return;
    }
    $('#place-bet').attr('disabled', 'disabled');
    
    var betInfo = $.parseJSON($('#betInfo').val());
    if (betInfo.Stake.length<1) {
        $('#page-dialog').dialog("destroy");
        $('#page-dialog > p').text('Please enter odd stake');
        $('#page-dialog').dialog({ modal: true });
    }
    var s = stake;
    if (stake==undefined) {
        s = betInfo.Stake;
    }
    $.ajax({
        url: '/Services/BettingHandler.ashx',
        type: 'POST',
        data: { match: betInfo.MatchId,stake:s,type:betInfo.Bettype,oddrate:betInfo.OddRate},
        success: function (res) {
            bettingResultHandler($.parseJSON(res));
        }
    }).always(function () {
        $('#place-bet').removeAttr('disabled');
    });;

    function updateTransInfo(data) {
        if (data.Status == 'AcceptBet' || data.Status == 'AcceptAmountAndWaitingReBet') {
            if (data.BetList !=null) {
                for (var i = 0; i < data.BetList.length; i++) {
                    var bet = data.BetList[i];
                    //var sid = '#s_' + bet.id;
                    var aid = '#a_' + bet.id;
                    var tmp = '<tr class="betsaccepted-td" id="a_' + bet.id + '"><td>' + bet.matchno + '</td><td>' + bet.cocktype + '</td><td>' + bet.acceptedamount + '</td><td>' + bet.odds + '</td></tr>';
                    //$(sid).remove();
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
        updateTransInfo(result);
        
        if (result.Status=="MarketExpire") {
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
                        PlaceBet();
                        $(this).dialog("destroy");
                    },
                    No: function () {
                        $(this).dialog("destroy");
                    }
                }
            });
        }
        else if (result.Status=="AcceptAmountAndWaitingReBet") {
            $('#page-dialog > p').html('chap nhan mot phan, phan con lai danh voi odd rate khac <br/> Da chap nhan: ' + result.MoneyAccept + '. So tien con lai danh voi oddrate moi: ' + result.RemainMoney + '<br/>Rate moi: ' + result.RateChange);
            $('#page-dialog').dialog({
                modal: true,
                open: function() {
                    $(this).parent().find('.ui-dialog-buttonset button:eq(0)').focus();
                },
                buttons: {
                    "Yes": function() {
                        PlaceBet(result.RemainMoney);
                        $(this).dialog("destroy");
                    },
                    No: function() {
                        $(this).dialog( "destroy" );
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
            $('#page-dialog > p').text("Bet Accepted");
            $('#page-dialog').dialog({ modal: true });
        } else if (result.Status == "MeronWalaUnConfirmed") {
            $('#page-dialog > p').text("MeronWalaUnConfirmed");
            $('#page-dialog').dialog({ modal: true });
        }
    }
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
}