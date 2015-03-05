<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="Calendar" %>
<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8' />
<title>..::Cockfight::..</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<script src="/Scripts/jquery-1.9.1.min.js"></script>
<link href='/css/calendar/fullcalendar.css' rel='stylesheet' />
<link href='/css/calendar/fullcalendar.print.css' rel='stylesheet' media='print' />
<script src='/Scripts/moment.min.js'></script>
<script src='/Scripts/fullcalendar.min.js'></script>
<script>

    $(document).ready(function () {
        var d = new Date();
        $('#calendar').fullCalendar({
            defaultDate: d,
            editable: true,
            eventLimit: true, // allow "more" link when too many events

            events: <%=CalendarData%>
    });

    });

</script>
<style>

	

	#calendar {
		max-width: 900px;
		margin: 0 auto;
		font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
		font-size: 14px;
	}

</style>
</head>
<body>
<!--------------top bar -------->
 <div id="top_bar">
 		
 </div>
 <!--------------top bar end-------->
 
 <!--------------content bar-------->
 <div id="main_bar">
 		<div id="main_box">
				
				<div id="right_box" >
						
						<div id="content1">
								<div class="head2" style="display:none;">
										Derby Schedule
								</div>
								<div id="content2" style="min-height:350px;">
									   
									  
									 
										<div class="sub_content">
											<div id='calendar'></div>
										</div>
								</div>
						</div>
				</div>
		</div>
 </div>
 

</html>

