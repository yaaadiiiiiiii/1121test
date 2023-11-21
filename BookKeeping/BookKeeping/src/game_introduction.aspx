<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="game_introduction.aspx.cs" Inherits="BookKeeping.src.game_introduction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>遊戲介紹</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="GameBackground">
        <div class="GameBody">
        <asp:Label class="GameItdTitle" ID="Label1" runat="server" Text="Label">遊戲介紹</asp:Label>
        <div class="GameItdButton">
            <asp:Button class="ButtonStyle3 IntButton" ID="Button1" runat="server" Text="認識錢幣" OnClick="Button1_Click" />
            <asp:Button class="ButtonStyle3 IntButton" ID="Button2" runat="server" Text="算算多少錢" OnClick="Button2_Click" />
            <asp:Button class="ButtonStyle3 IntButton" ID="Button3" runat="server" Text="買文具" OnClick="Button3_Click" />
        </div>
        <div>
            <asp:Image ID="itd_game1" runat="server" ImageUrl="images/game/itd_game1.png" Visible="false"/>
            <asp:Image ID="itd_game2" runat="server" ImageUrl="images/game/itd_game2.png" Visible="false"/>
            <asp:Image ID="itd_game31" runat="server" ImageUrl="images/game/itd_game31.png" Visible="false"/>
            <asp:Image ID="itd_game32" runat="server" ImageUrl="images/game/itd_game32.png" Visible="false"/>
            <asp:Button class="ButtonStyle LastNextSize GameLast" ID="GameLast" runat="server" Text="<" Visible="false" OnClick="GameLast_Click"/>
            <asp:Button class="ButtonStyle LastNextSize GameNext" ID="GameNext" runat="server" Text=">" Visible="false" OnClick="GameNext_Click"/>
        </div>
        </div>
        </div>
        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/game_menu.aspx" />
    </form>
</body>
</html>
