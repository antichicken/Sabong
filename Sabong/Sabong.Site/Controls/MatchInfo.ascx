<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MatchInfo.ascx.cs" Inherits="Controls_MatchInfo" %>
<%@ Import Namespace="Sabong.Business" %>
<% if (Match != null)
   {%>
<div id="match-info-wrap">
    <div class="twocol">
    <div class="twocol-1">
        <span>Meron</span>
        <img id="meron-image" width="122px" src="<%= Match.cock_type.ToLower()=="wala"? Match.agimage : Match.cimage %>">
    </div>
    <div class="twocol-2">
        <span>wala</span>
        <img id="wala-image" width="122px" src="<%=Match.cock_type.ToLower()=="wala"? Match.cimage : Match.agimage %>">
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
        <span class="threecol2-2">BDD</span>
        <span class="threecol2-4">FTD</span>
        <span class="threecol2-3">Wala</span>
    </div>
    <div class="threecol3">
        <div class="button_m clickable <%=DisabledCss() %>"><span id="choose-meron" data-cock="<%=Match.cock_type.ToLower()=="wala"?Match.acid:Match.cid %>"><%=Match.C1odds %></span></div>
        <div class="button_d clickable <%=DisabledCss() %>"><span id="choose-draw"><%=Match.drawwodds %></span></div>
        <div class="button_f clickable <%=DisabledCss() %>"><span id="choose-ftd"><%=Match.ftd %></span></div>
        <div class="button2_wa clickable <%=DisabledCss() %>"><span id="choose-wala" data-cock="<%=Match.cock_type.ToLower()=="wala"?Match.cid:Match.acid %>"><%=Match.C2odds %></span></div>
    </div>
    <% if (Status == MatchStatus.Confirmed || Status == MatchStatus.ClosingSoon)
       { %>
    <div class="mix" id="match-confirm">Meron / Wala confirmed</div>
    <% }
       else
       {%>
    <div class="mix unconfirm" id="match-confirm">Meron / Wala unconfirm</div>
    <%}%>
</div>
<input type="hidden" value="<%=Match.fslno%>" id="match-id" />
</div>

<%}
   else
   {%>
<div id="match-info-wrap"  class="hidden">
    <div class="twocol">
        <div class="twocol-1">
            <span>Meron</span>
            <img id="meron-image" width="122px" src="">
        </div>
        <div class="twocol-2">
            <span>wala</span>
            <img id="wala-image" width="122px" src="">
        </div>
    </div>
    <div class="threecol">
        <span class="threecol-1" id="meron-name"></span>
        <span class="threecol-2">vs</span>
        <span class="threecol-3" id="wala-name"></span>
    </div>
    <div class="threecolblock">
        <div class="threecol2">
            <span class="threecol2-1">Meron</span>
            <span class="threecol2-2">BDD</span>
            <span class="threecol2-4">FTD</span>
            <span class="threecol2-3">Wala</span>
        </div>
        <div class="threecol3">
            <div class="button_m clickable"><span id="choose-meron"></span></div>
            <div class="button_d clickable"><span id="choose-draw"></span></div>
            <div class="button_f clickable"><span id="choose-ftd"></span></div>
            <div class="button2_wa clickable"><span id="choose-wala"></span></div>
        </div>
        <% if (Status == MatchStatus.Confirmed || Status == MatchStatus.ClosingSoon)
           { %>
        <div class="mix" id="match-confirm">Meron / Wala confirmed</div>
        <% }
           else
           {%>
        <div class="mix unconfirm" id="match-confirm">Meron / Wala unconfirm</div>
        <%}%>
    </div>
    <input type="hidden" value="" id="match-id" />
</div>

<%} %>

