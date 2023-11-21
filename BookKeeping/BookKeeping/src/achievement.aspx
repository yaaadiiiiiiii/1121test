<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="achievement.aspx.cs" Inherits="_BookKeeping.achievement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>成就</title>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="AchBody">
            <div class="AchHeader">
                <h1><asp:Label class="AchTopTitle" ID="Label1" runat="server" Text="成就"></asp:Label></h1>
                <div class="AchTopButton">
                    <asp:Button class="ButtonStylePick ButtonSize1 NoFin" ID="Button1" runat="server" Text="未完成" />
                    <asp:Button class="ButtonStyle ButtonSize1 Fin" ID="Button2" runat="server" Text="已完成"  PostBackUrl="~/src/finishAch.aspx"/>
                </div>
            </div>

            <div class="AchContent">
                <asp:Repeater ID="TaskRepeater" runat="server">
                    <ItemTemplate>
                        <div class="task-container">
                            <!-- 左側圖片 -->
                            <asp:Image CssClass="task-image" ID="Image1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' AlternateText="Task Image" />

                            <!-- 任務內容 -->
                            <div class="task-content">
                                <!-- 任務主題 -->
                                <h3 class="task-main"><%# Eval("TaskName") %></h3>
                                <!-- 任務內容 -->
                                <p><%# Eval("TaskDescription") %></p>
                                <!-- 進度條 -->
                                <div class="progress">
                                    <div class="progress-bar" style='<%# Eval("ProgressBarStyle") %>'></div>
                                </div>
                            </div>

                            <!-- 領取按鈕（僅在達成條件時顯示） -->
                            <asp:Button CssClass="ButtonStyle3 ButtonSize2 claim-button" ID="ClaimButton" runat="server" Text="領取" CommandName="ClaimTask" CommandArgument='<%# Eval("TaskID") %>' Visible='<%# Convert.ToBoolean(Eval("IsTaskCompleted")) %>' OnClick="ClaimButton_Click" style='<%# Convert.ToBoolean(Eval("IsTaskCompleted")) ? "" : "display:none;" %>' />

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/main.aspx" />
        <div id="overlay"></div>
    </form>
</body>
</html>
