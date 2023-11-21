<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="BookKeeping.src.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>主畫面</title>
</head>
<body>
    <div class="MainBody">
    <form id="Form1" runat="server">
        <asp:Label ID="userid" runat="server"></asp:Label>
        <div class="Setting"><%--使用者設定--%>
            <asp:ImageButton ID="Setting" runat="server" ImageUrl="images/main/user_set.png" PostBackUrl="~/src/setting.aspx"/>
            <asp:Image ID="AvatarHead" runat="server" ImageUrl="images/avatar/ava_girl.png" /><%--人物--%>
            <asp:Label ID="NickName" runat="server"></asp:Label>
            <asp:Label ID="UId" runat="server"></asp:Label>
        </div>
        <div class="MainPerson"> <%--*0.8--%>
            <asp:Image ID="MainHead" runat="server" ClientIDMode="Static"/>
            <asp:Image ID="MainBody" runat="server" ClientIDMode="Static"/>
            <asp:Image ID="MainPet" runat="server" ClientIDMode="Static"/>
        </div>
        <asp:Button ID="Closet" runat="server" PostBackUrl="~/src/dressing_room.aspx"/><%--更衣室--%>
        <asp:Button ID="Wishes" runat="server" PostBackUrl="~/src/bucket_list.aspx"/><%--願望清單--%>
        <asp:Button ID="Ach" runat="server" PostBackUrl="~/src/achievement.aspx"/><%--成就--%>
        <asp:Button ID="Game" runat="server" PostBackUrl="~/src/game_menu.aspx"/><%--小遊戲--%>
        <asp:Button ID="Bookkeeping" runat="server" PostBackUrl="~/src/bookkeeping_add.aspx"/><%--記帳--%>

        <asp:Image ID="Pig" runat="server"   />
        <asp:Label ID="PigProgress" class="PigText" runat="server" Text=" " ></asp:Label>
    </form>
    </div>
</body>
</html>