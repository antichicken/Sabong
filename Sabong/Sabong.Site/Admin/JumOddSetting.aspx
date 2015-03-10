<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="JumOddSetting.aspx.cs" Inherits="Admin_JumOddSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="dashboard-header">
        Setting for Jump Odds
    </div>
    <asp:Label runat="server" ID="Done" Visible="False" ForeColor="green">Done</asp:Label>
    <asp:Label runat="server" ID="Faild" Visible="False" ForeColor="red">Fail, Please try againt</asp:Label>
    <br/>
    <table>
        <tr>
            <td>Risk Box Size Leve1: </td>
            <td>
                <asp:TextBox ID="txtRB1" runat="server"></asp:TextBox></td>
            <td>Risk Level1: </td>
            <td><asp:TextBox ID="txtR1" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Risk Box Size Leve2: </td>
            <td>
                <asp:TextBox ID="txtRB2" runat="server"></asp:TextBox></td>
            <td>Risk Level2: </td>
            <td><asp:TextBox ID="txtR2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Risk Box Size Leve3: </td>
            <td>
                <asp:TextBox ID="txtRB3" runat="server"></asp:TextBox></td>
            <td>Risk Level3: </td>
            <td><asp:TextBox ID="txtR3" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Risk Box Size Leve4: </td>
            <td>
                <asp:TextBox ID="txtRB4" runat="server"></asp:TextBox></td>
            <td>Risk Level4: </td>
            <td><asp:TextBox ID="txtR4" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Risk Box Size Leve5: </td>
            <td>
                <asp:TextBox ID="txtRB5" runat="server"></asp:TextBox></td>
            <td>Risk Level5: </td>
            <td><asp:TextBox ID="txtR5" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" Width="95px" /></td>
            <td></td>
        </tr>
    </table>
</asp:Content>

