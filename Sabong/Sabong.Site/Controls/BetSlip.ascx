<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BetSlip.ascx.cs" Inherits="Controls_BetSlip" %>
<div class="betslip">
    <div class="betslip-title">BET SLIP <span class="betslip-close">Close</span></div>
    <div class="betslip_content">
        <table>
            <tbody>
                <tr>
                    <td id="bet-description">COCK1 (MERON) @ -0.79</td>
                    <td>
                        <input name="" type="text" id="input-stake">
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <button id="place-bet" onclick="PlaceBet();return false;">Place Bet</button></td>
                </tr>
            </tbody>
        </table>
    </div>
    <input type="hidden" id="betInfo" value=""/>
</div>
