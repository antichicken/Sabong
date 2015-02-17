<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>UserName: </td>
            <td><asp:TextBox ID="txtUser" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>PassWord: </td>
            <td><asp:TextBox ID="txtPass" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnLogin" runat="server" Text="Button" OnClick="btnLogin_Click" /></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
