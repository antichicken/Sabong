<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="OpeningOdd.aspx.cs" Inherits="Admin_OpeningOdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="dashboard-header">
        Setting for Opening Odds
    </div>
    <asp:Label runat="server" ID="Done" Visible="False" ForeColor="green">Done</asp:Label>
    <asp:Label runat="server" ID="Faild" Visible="False" ForeColor="red">Fail, Please try againt</asp:Label>
    <asp:HiddenField runat="server" ID="Cooktype"/>
    <br/>
    <table>
        <tr>
            <td>Current Match: </td>
            <td><asp:Label runat="server" ID="LbMatch"></asp:Label></td>
        </tr>
        <tr>
            <td><label for="<%=txtMeronRate.ClientID %>">Meron: </label></td>
            <td><asp:TextBox ID="txtMeronRate" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><label for="<%=txtWalaRate.ClientID %>">Wala: </label></td>
            <td><asp:TextBox ID="txtWalaRate" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Button ID="btnUpdate" runat="server" Text="Submit" OnClick="btnUpdate_Click" /></td>
            <td></td>
        </tr>
    </table>
</asp:Content>

