<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="LogTestWeb.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />日志内容：<br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />写日志：<br />
        <asp:Button ID="Button1" runat="server" Text="写日志" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
