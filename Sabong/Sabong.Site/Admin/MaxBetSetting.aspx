<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="MaxBetSetting.aspx.cs" Inherits="Admin_MaxBetSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="dashboard-header">
        Setting for MaxBet
    </div>
    <asp:Label runat="server" ID="Done" Visible="False" ForeColor="green">Done</asp:Label>
    <asp:Label runat="server" ID="Faild" Visible="False" ForeColor="red">Fail, Please try againt</asp:Label>
    <br/>
    <label for="<%=txtMaxBet.ClientID %>">MaxBet: </label>
    <asp:TextBox ID="txtMaxBet" runat="server"></asp:TextBox>
    <asp:Button ID="btnUpdate" runat="server" Text="Submit" OnClick="btnUpdate_Click" />
</asp:Content>

