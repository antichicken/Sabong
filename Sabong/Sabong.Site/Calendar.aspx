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

            events: [
				{ title: "10AM", start: "2015-01-11" }, { title: "9AM", start: "2015-01-12" }, { title: "10AM", start: "2015-01-13" }, { title: "Derby", start: "2015-01-16" }, { title: "9AM", start: "2015-01-14" }, { title: "TBC", start: "2015-01-15" }, { title: "10am", start: "2015-01-17" }, { title: "9AM", start: "2015-01-18" }, { title: "9AM", start: "2015-01-19" }, { title: "9AM", start: "2015-01-20" }, { title: "9AM", start: "2015-01-21" }, { title: "10AM", start: "2015-01-22" }, { title: "9AM", start: "2015-01-23" }, { title: "10AM", start: "2015-01-24" }, { title: "9AM", start: "2015-01-25" }, { title: "9AM", start: "2015-01-26" }, { title: "9AM", start: "2015-01-27" }, { title: "9AM", start: "2015-01-28" }, { title: "10AM", start: "2015-01-29" }, { title: "9AM", start: "2015-01-30" }, { title: "10AM", start: "2015-01-31" }, { title: "8-9AM_1_COCK_FK", start: "2015-02-02" }, { title: "NO_MATCHES!!", start: "2015-02-09" }, { title: "NO_MATCHES!!", start: "2015-02-16" }, { title: "NO_MATCHES!!", start: "2015-02-23" }, { title: "10AM_1COCK_FK", start: "2015-02-01" }, { title: "9-10AM_1COCK_FK", start: "2015-02-03" }, { title: "8-9AM_1_COCK_FK", start: "2015-02-04" }, { title: "1PM_COCK_TIMBANGAN", start: "2015-02-05" }, { title: "9-10AM_DERBY", start: "2015-02-06" }, { title: "10-11AM_1COCK_FK", start: "2015-02-07" }, { title: "8-9AM_1COCK_FK", start: "2015-02-08" }, { title: "8-9AM_1COCK_FK", start: "2015-02-15" }, { title: "8-9AM_1COCK_FK", start: "2015-02-22" }, { title: "9-10AM_1COCK_FK", start: "2015-02-10" }, { title: "9-10AM_1COCK_FK", start: "2015-02-17" }, { title: "9-10AM_1COCK_FK", start: "2015-02-24" }, { title: "8-9AM_1_COCK_FK", start: "2015-02-11" }, { title: "8-9AM_1_COCK_FK", start: "2015-02-18" }, { title: "8-9AM_1_COCK_FK", start: "2015-02-25" }, { title: "10-11AM_1COCK_FK", start: "2015-02-12" }, { title: "10-11AM_1COCK_FK", start: "2015-02-19" }, { title: "10-11AM_1COCK_FK", start: "2015-02-26" }, { title: "9-10AM_DERBY", start: "2015-02-13" }, { title: "9-10AM_DERBY", start: "2015-02-20" }, { title: "9-10AM_DERBY", start: "2015-02-27" }, { title: "10-11AM_1COCK_FK", start: "2015-02-14" }, { title: "3-4PM_4C_DERBY", start: "2015-02-21" }, { title: "10-11AM_1COCK_FK", start: "2015-02-28" },
            ]
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
 

<script type="text/javascript">window.NREUM || (NREUM = {}); NREUM.info = { "beacon": "bam.nr-data.net", "licenseKey": "dd560db9c7", "applicationID": "5355924", "transactionName": "NFcHYkZTXhdSUkwIXQ0dMERdHUUXVkMXAlMPVwtSUUBGDVZGFhFaEw==", "queueTime": 0, "applicationTime": 7, "atts": "GBAEFA5JTRk=", "errorBeacon": "bam.nr-data.net", "agent": "js-agent.newrelic.com\/nr-536.min.js" }</script></body>
</html>

