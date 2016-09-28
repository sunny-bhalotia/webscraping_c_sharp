<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </div>
        <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
        <asp:TextBox ID="TextBox1" runat="server" Height="445px" Width="953px"></asp:TextBox>
    </form>
</body>
</html>
