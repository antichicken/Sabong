<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="Admin_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dashboard-header">
        Admin control panel for odd adjustment
    </div>
    <style>
        .bold {
            font-weight: bold
        }
        .auto-style1 {
            width: 180px;
        }
        .auto-style2 {
            font-weight: bold;
            width: 180px;
        }
        .auto-style3 {
            font-weight: bold;
            width: 154px;
        }
        .auto-style4 {
            width: 152px;
        }

        .odd-jump:hover {
            background-color: yellow;
            cursor: pointer
        }
    </style>
    <table>
        <tr>
            <td class="auto-style4">Current Match: </td>
            <td class="auto-style3"></td>
            <td>Openning Odds: </td>
            <td class="auto-style1"><asp:Literal ID="txtOpenningOdd" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="auto-style4">MaxBet Setting: </td>
            <td class="bold" colspan="3"><asp:Literal ID="txtMaxBet" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="bold" colspan="4" style="height: 40px;vertical-align: bottom;">Current Setting For Jumb Odd</td>
        </tr>
        <tr class="odd-jump">
            <td class="auto-style4">Risk Box Size Leve1: </td>
            <td class="auto-style3">
                <asp:Literal ID="txtRB1" runat="server"></asp:Literal></td>
            <td>Risk Level1: </td>
            <td class="auto-style2"><asp:Literal ID="txtR1" runat="server"></asp:Literal></td>
        </tr>
        <tr class="odd-jump">
            <td class="auto-style4">Risk Box Size Leve2: </td>
            <td class="auto-style3">
                <asp:Literal ID="txtRB2" runat="server"></asp:Literal></td>
            <td>Risk Level2: </td>
            <td class="auto-style2"><asp:Literal ID="txtR2" runat="server"></asp:Literal></td>
        </tr>
        <tr class="odd-jump">
            <td class="auto-style4">Risk Box Size Leve3: </td>
            <td class="auto-style3">
                <asp:Literal ID="txtRB3" runat="server"></asp:Literal></td>
            <td>Risk Level3: </td>
            <td class="auto-style2"><asp:Literal ID="txtR3" runat="server"></asp:Literal></td>
        </tr>
        <tr class="odd-jump">
            <td class="auto-style4">Risk Box Size Leve4: </td>
            <td class="auto-style3">
                <asp:Literal ID="txtRB4" runat="server"></asp:Literal></td>
            <td>Risk Level4: </td>
            <td class="auto-style2"><asp:Literal ID="txtR4" runat="server"></asp:Literal></td>
        </tr>
        <tr class="odd-jump">
            <td class="auto-style4">Risk Box Size Leve5: </td>
            <td class="auto-style3">
                <asp:Literal ID="txtRB5" runat="server"></asp:Literal></td>
            <td>Risk Level5: </td>
            <td class="auto-style2"><asp:Literal ID="txtR5" runat="server"></asp:Literal></td>
        </tr>
    </table>
</asp:Content>

