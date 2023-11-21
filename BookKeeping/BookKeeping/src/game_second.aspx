<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="game_second.aspx.cs" Inherits="BookKeeping.src.game_second" %>

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
    <title>遊戲二-算算多少錢</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="GameBackground">
        <div class="GameBody">
        <asp:Panel ID="SecondGamePanel" runat="server" Visible="false"><%--遊戲二--%>
            <div class="GameLeft2"><%--左半邊題目--%>
                <asp:Label ID="question2" class="question2" runat="server" Text="Label">請問下圖中總共有多少錢？</asp:Label>
                <div class="Thousand"><%--$1000--%>
                    <asp:Panel ID="Panel1000" runat="server"></asp:Panel>
                </div>
            
                <div class="FiveHundred"><%--$500--%>
                    <asp:Panel ID="Panel500" runat="server"></asp:Panel>
                </div>
            
                <div class="OneHundred"><%--$100--%>
                    <asp:Panel ID="Panel100" runat="server"></asp:Panel>
                </div>
            
                <div class="Fifty"><%--$50--%>
                    <asp:Panel ID="Panel50" runat="server"></asp:Panel>
                </div>
           
                <div class="Ten"><%--$10--%>
                    <asp:Panel ID="Panel10" runat="server"></asp:Panel>
                </div>
            
                <div class="Five"><%--$5--%>
                    <asp:Panel ID="Panel5" runat="server"></asp:Panel>
                </div>

                <div class="One"><%--$1--%>
                    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                </div> 
            </div>

            <div class="GameRight2"><%--右半邊答案--%>
                <asp:Button class="CoinAnsButton" ID="Ans4" runat="server" OnClick="CheckAnswer" CommandArgument="4" />
                <asp:Button class="CoinAnsButton" ID="Ans5" runat="server" OnClick="CheckAnswer" CommandArgument="5" />
                <asp:Button class="CoinAnsButton" ID="Ans6" runat="server" OnClick="CheckAnswer" CommandArgument="6" />
            </div>
            </asp:Panel>
        <!-- 進度條 -->
        <div class="GameProgress">
            <div class="GameProgress-bar" style='<%# Eval("ProgressBarStyle") %>' runat="server" id="GameProgressBar"></div>
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
