<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="_BookKeeping.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>記帳趣 登入</title>
</head>
        
<body>
    <div class="LogBody">
    <form class="LogForm" id="form1" runat="server">
        <h1 class="LogTitle" align="center">記帳趣</h1>
        <div class="LogText">
        <p>
            <label style="font-size:25px;">帳號</label>
            <asp:TextBox class="TextBoxStyle" ID="UserAcc" runat="server" Width="250" Height="35" placeholder="請輸入帳號" Font-Size="13"></asp:TextBox>
        </p>
        <br />
        <p>
            <label style="font-size:25px;">密碼</label>
            <asp:TextBox class="TextBoxStyle" ID="UserPwd" runat="server" Width="250" Height="35" TextMode="Password" placeholder="請輸入密碼" Font-Size="13"></asp:TextBox>
        </p>
        <a class="LogForgetPwd" href="forget_pw.aspx">忘記密碼</a>
        </div>
        <asp:Label ID="state" runat="server" style="color:red"></asp:Label>
        <div class="LogButton">
            <asp:Button class="ButtonStyle3 LogButton1" ID="LoginButton" runat="server" Text="登入" Width="300" Height="35" Font-Size="13" OnClick="Login_click"/>
            <asp:Button class="ButtonStyle3 LogButton2" ID="RegisterButton" runat="server" Text="我要註冊！" Width="300" Height="35" Font-Size="13" PostBackUrl="register.aspx" />
        </div>
    </form>
    </div>
</body>
</html>
