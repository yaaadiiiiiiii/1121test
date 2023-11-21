<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="finishAch.aspx.cs" Inherits="_BookKeeping.finishAch" %>

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
                    <asp:Button class="ButtonStyle ButtonSize1 NoFin" ID="Button1" runat="server" Text="未完成" PostBackUrl="~/src/achievement.aspx"/>
                    <asp:Button class="ButtonStylePick ButtonSize1 Fin" ID="Button2" runat="server" Text="已完成" />
                </div>
            </div>

             <div class="AchContent">
                <asp:Repeater ID="FinishRepeater" runat="server">
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
                            </div>
                            <asp:Image ID="MyImage" CssClass="check" runat="server" ImageUrl='<%# Eval("ImageURLField") %>' AlternateText="Image" />
                

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/main.aspx" />
    </form>
</body>
</html>