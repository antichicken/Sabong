<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MatchInfo.ascx.cs" Inherits="Controls_MatchInfo" %>
<div class="twocol">
    <div class="twocol-1">
        <span>Meron</span>
        <img id="meron-image" src="/images/Chicken_01.jpg">
    </div>
    <div class="twocol-2">
        <span>wala</span>
        <img id="wala-image" src="/images/Chicken_02.jpg">
    </div>
</div>
<div class="threecol">
    <span class="threecol-1" id="meron-name">cock1</span>
    <span class="threecol-2">vs</span>
    <span class="threecol-3" id="wala-name">roster1</span>
</div>
<div class="threecolblock">
    <div class="threecol2">
        <span class="threecol2-1">Meron</span>
        <span class="threecol2-2">Draw</span>
        <span class="threecol2-3">Wala</span>
    </div>
    <div class="threecol3">
        <div class="threecol3-1"><span id="choose-meron">+0.95</span></div>
        <div class="threecol3-2"><span id="choose-draw">1:6</span></div>
        <div class="threecol3-3"><span id="choose-wala">+0.95</span></div>
    </div>
    <div class="mix" id="match-confirm">Meron/Wala confirmed</div>
</div>
<input type="hidden" value="100" id="match-id"/>
