<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/styleReport.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="#fff2 strong head6">Daily Win/Loss Report</div>
        
        <div class="graycontainer">
                	<div class="grayinsidecontainer clearfix">
                        <div class="floatleft11 margintop2">Start Date:</div>
                        <div class="floatleft11 marginleft10" style="margin-left:4px;"><input type="text" class="datetxtbox date1 hasDatepicker" id="inputField" name="stdate" value="02/03/2015"></div>
                	    <div class="floatleft marginleft3"><img src="images/ico_calendar.jpg" width="20" height="18" alt="" id="calandericon1"/></div>
			 
                        <div class="floatleft11 marginleft25">End Date</div>
                	    <div class="floatleft11 marginleft10"><input type="text" class="datetxtbox date2 hasDatepicker" id="inputField1" name="endate" value="02/03/2015"/></div>
                	    <div class="floatleft marginleft3"><img src="images/ico_calendar.jpg" width="20" height="18" alt="" id="calandericon2"/></div>
                	    <div class="floatleft11 marginleft25"> <input type="submit" class="submitbtn2" value="Submit"/></div>
                    </div>
                </div>
        <table style=" margin-top:20px; width:100%;">
										     <tbody><tr>
                                             		<td colspan="17" class="table_head">Daily Win Loss Details -
                                             		
 	02/03/2015 --&gt; 02/03/2015</td>
                                             </tr>
											<tr style="line-height:1.0;">
											<td rowspan="2" class="transfertableheader">#</td>
											
											<td rowspan="2" class="transfertableheader"> Date</td>
											
											<td rowspan="2" class="transfertableheader">Turnover</td>
											
											<td rowspan="2" class="transfertableheader">Gross Comm.</td>
											
											<td colspan="3" class="transfertableheader">Member</td>
											<td rowspan="2" class="transfertableheader">Company</td>
</tr>
						
											<tr>
												<td class="winlosesubheader"> Win / Loss</td>
												<td class="winlosesubheader"> Comm.</td>
													<!--<th> Win Tax.</th>-->
												<td class="winlosesubheader"> Total </td>




											</tr>




								     
								     
								      <tr style="background: #fbf9ed; text-align: right;font-weight:bold;">

<td colspan="2" class="td_bg" align="center">Grand Total</td>
										     
										    
<td class="td_bg"><span> 0.00</span></td>
<td class="td_bg"><span> 0.00</span></td>

<td class="td_bg1"><span> 0.00</span></td>
<td class="td_bg1"><span> 0.00</span></td>
<td class="td_bg1"><span> 0.00</span></td>
<td class="td_bg"><span> 0.00</span></td>  
										  
										     
								     </tr>
	
										</tbody></table>
    </div>
    </form>
</body>
</html>
