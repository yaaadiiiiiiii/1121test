<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="game_menu.aspx.cs" Inherits="BookKeeping.src.game_menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>關卡一主選單</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="GameBackground">
        <div class="GameBody">
            <asp:Label class="GameTitle" ID="Label1" runat="server" Text="Label">小遊戲</asp:Label>
            <div class="GameMain">
                <asp:Button class="ButtonStyle3 IntButton" ID="GameIntro" runat="server" Text="遊戲介紹" PostBackUrl="~/src/game_introduction.aspx"/>
                <asp:ImageButton ID="GameIcon1" runat="server" class="GameMenuButton" AlternateText="認識錢幣"  ImageUrl="images/game/game_icon1.png" PostBackUrl="~/src/game_first.aspx"/>
                <asp:Label class="" ID="GameTit1" runat="server" Text="Label">認識錢幣</asp:Label>
                <asp:ImageButton ID="GameIcon2" runat="server"  class="GameMenuButton" AlternateText="算算多少錢" ImageUrl="images/game/game_icon2.png" PostBackUrl="~/src/game_second.aspx" />
                <asp:Label class="" ID="GameTit2" runat="server" Text="Label">算算多少錢</asp:Label>
                <asp:ImageButton ID="GameIcon3" runat="server"  class="GameMenuButton" AlternateText="買文具" ImageUrl="images/game/game_icon3.png" PostBackUrl="~/src/game_third.aspx" />
                <asp:Label class="" ID="GameTit3" runat="server" Text="Label">買文具</asp:Label>
            </div>
        </div>
        </div>
        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/main.aspx" />
    </form>
</body>
</html>
