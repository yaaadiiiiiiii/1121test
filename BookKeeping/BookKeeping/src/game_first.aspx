<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="game_first.aspx.cs" Inherits="BookKeeping.src.game_first" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- 引用你的 CSS 文件 -->
    <link rel="stylesheet" type="text/css" href="styles.css" />

    <!-- 引用 Bootstrap 的 CSS 文件 -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />

    <!-- 引用 Bootstrap 的 JavaScript 文件（jQuery 需要在 Bootstrap 之前引入） -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>遊戲一-認識錢幣</title>
</head>
<body >
    <form id="form1" runat="server">
        <div class="GameBackground">
        <div class="GameBody">
        <asp:Panel ID="FirstGamePanel" runat="server" Visible="false"><%--遊戲一--%>
            <div class="GameLeft"><%--左半邊題目--%>
                <asp:Label ID="question1" class="question" runat="server" Text="">請問下圖中為多少金額？</asp:Label></br>
                <asp:Image ID="Image1" runat="server"/>
            </div>

            <div class="GameRight"><%--右半邊答案--%>
                <asp:Button class="CoinAnsButton" ID="Ans1" runat="server" OnClick="CheckAnswer" CommandArgument="0" />
                <asp:Button class="CoinAnsButton" ID="Ans2" runat="server" OnClick="CheckAnswer" CommandArgument="1" />
                <asp:Button class="CoinAnsButton" ID="Ans3" runat="server" OnClick="CheckAnswer" CommandArgument="2" />
            </div>
        </asp:Panel>

        <!-- 進度條 -->
        <div class="GameProgress">
            <div class="GameProgress-bar" id="GameProgressBar" runat="server" style='<%# Eval("ProgressBarStyle") %>'></div>
        </div>                        
        <asp:Label ID="GameProgress" class="GameProgressText" runat="server" Text='<%# Eval("ProgressText") %>' ></asp:Label>

		<asp:Button class="ButtonStyle3 GameEnd" ID="LeaveGame" runat="server" Text="結束遊戲" OnClick="LeaveGame_Click" />
        <asp:Label ID="randomNum" runat="server" Text="" Visible="false"></asp:Label>
        <div class="CorrectcntText">
            <asp:Label ID="Label2" runat="server" Text="已答對"></asp:Label>
            <asp:Label ID="correctcnt" runat="server" Text="0"></asp:Label><%--紀錄答對題數--%>
            <asp:Label ID="Label1" runat="server" Text="題"></asp:Label>
        </div>
        </div>
        </div>
        <div id="resultModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="resultModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-body">
                        <p id="resultMessage"></p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" text="再玩一次" class="ButtonStyle3 JumpButton" OnClick="RestartGame" />
                        <asp:Button runat="server" text="結束遊戲" class="ButtonStyle3 JumpButton" PostBackUrl="game_menu.aspx" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>