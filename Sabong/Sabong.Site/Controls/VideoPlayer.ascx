<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoPlayer.ascx.cs" Inherits="VideoPlayer" %>
<div class="video">
    <div class="content_matchbox" id="vedio_content" style="height: calc(100% -70px); height: -webkit-calc(100% -70px); height: calc(100% -70px); height: -moz-calc(100% - 70px); min-height: 400px; overflow: hidden;">
        <a href="#" class="example-defaults1" style="text-decoration: none; color: #a1a1a1; display: none;">
            <img src="/images/video.jpg" width="30" style="position: absolute; z-index: 999; float: left;" />
        </a>
        <script type='text/javascript' src='/Scripts/swfobject.js'></script>
        <script src="/Scripts/8wAVtGmiEeSbeA6sC0aurw.js"></script>
        <script type="text/javascript" src="/Scripts/jwplayer.js"></script>
        <script type="text/javascript">jwplayer.key="ILt4TjRHjZ6qLGStbFMjFjN6GQ+q0ImTQFPe2w==";</script>
        <div id='playerlOrdvdydtRFi'></div>
        <script type='text/javascript'>
    
            url = 'rtmp://stream-edge-vn2.s128.net/live/';

            jwplayer('playerlOrdvdydtRFi').setup({
                file: url+'myStream',
                width: '100%',
                aspectratio: '16:10',
                primary: 'flash',
                rtmp: {
                    securetoken: "ttttttttt",
                    bufferlength: 2
                },
            });
        </script>

    </div>
    <ul>
        <li>Select Video Quality:</li>
        <li><a href="#" class="video-quality" data-type="1">High (600kbqs)</a></li>
        <li><a href="#" class="video-quality" data-type="2">Medium (360kbqs)</a></li>
        <li><a href="#" class="video-quality" data-type="3">Low (200kbqs)</a></li>
        <li><a href="#" class="video-quality" data-type="4">Low (100kbqs)</a></li>
    </ul>
</div>
