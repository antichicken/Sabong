<%@ Page Title="Annoucement" Language="C#" MasterPageFile="DarkMasterPage.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    Annoucement
    </asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div id="content2_box"  class="content" style="height:380px; width:845px; padding: 10px; margin-left: 40px; background: #333; ">
						   <div   style="width:100%; height: auto; float: left; margin-top: 0px;">
						       <div style='width:auto;  font-family:Helvetica;  text-align:center; background:#000; color:#fff; padding:4px; padding-left:8px; padding-right:8px; float:left; font-size:14px; font-weight:bold; margin-left:2.2%; margin-bottom:5px; margin-top:20px;'>24 / 02 / 2015</div><br/>
                               
                                  <div style="width:85%; float:left; font-family:Helvetica; padding-bottom: 12px; padding-top: 12px;  font-size:14px; border-bottom:1px solid #1c1c1c; color:#fff; margin-left:5%;">
										     <div style="width:15%;float:left; font-family:Helvetica;  height: auto; color: #fa9600;">Time xxx</div>
										     <div style="width:76%;float: left; height: auto;">
												   <div style="width:17%; height: auto; float: left; text-align: right;">   Attn: manchester</div>
												   <div style="width:78%; height: auto; float: left; margin-left: 2%;">
                                                       <%--<%#Eval("subject") %>--%>
												   </div>
										     </div>
								    </div>

                           </div>
       
        </div>
    
     <div style="clear:both;"></div>
				  <div style="width:250px; float: left; margin-left: 355px; margin-top:10px;">
				  				<a href="/Default.aspx" style="text-decoration:none;"><input type="button" name="agree" id="agree" value="Proceed" class="button3" style="text-decoration:none; cursor:pointer; "/></a>
				  </div>
</asp:Content>