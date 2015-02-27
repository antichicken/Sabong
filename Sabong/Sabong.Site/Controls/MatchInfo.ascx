<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MatchInfo.ascx.cs" Inherits="Controls_MatchInfo" %>
<% if (Match != null)
   {%>
<div class="twocol">
    <div class="twocol-1">
        <span>Meron</span>
        <img id="meron-image" width="122px" src="https://s1288.net/admin/<%= Match.cock_type.ToLower()=="wala"? Match.agimage : Match.cimage %>">
    </div>
    <div class="twocol-2">
        <span>wala</span>
        <img id="wala-image" width="122px" src="https://s1288.net/admin/<%=Match.cock_type.ToLower()=="wala"? Match.cimage : Match.agimage %>">
    </div>
</div>
<div class="threecol">
    <span class="threecol-1" id="meron-name"><%=Match.cock_type.ToLower()=="wala"? Match.agname : Match.cname %></span>
    <span class="threecol-2">vs</span>
    <span class="threecol-3" id="wala-name"><%=Match.cock_type.ToLower()=="wala"? Match.cname : Match.agname %></span>
</div>
<div class="threecolblock">
    <div class="threecol2">
        <span class="threecol2-1">Meron</span>
        <span class="threecol2-2">Draw</span>
        <span class="threecol2-3">Wala</span>
    </div>
    <div class="threecol3">
        <div class="threecol3-1"><span id="choose-meron"><%=Match.C1odds %></span></div>
        <div class="threecol3-2"><span id="choose-draw"><%=Match.drawwodds %></span></div>
        <div class="threecol3-3"><span id="choose-wala"><%=Match.C2odds %></span></div>
    </div>
    <div class="mix" id="match-confirm"><%=Status%></div>
</div>
<input type="hidden" value="<%=Match.fslno%>" id="match-id" />
<%} %>

