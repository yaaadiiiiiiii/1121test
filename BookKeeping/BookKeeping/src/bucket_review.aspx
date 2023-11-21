﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bucket_review.aspx.cs" Inherits="BookKeeping.src.bucket_review" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>願望審核區</title>
</head>
<body>
    <form class="BucForm" id="form1" runat="server">
        <div class="BucDiv">
        <div class="BMemo MemoStyle2">
            <div class="TraWord">
                <div class="RevText">
                    <asp:Label ID="label1" runat="server" Text=""></asp:Label>
                    <asp:Label ID="label2" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="label3" runat="server" Text=""></asp:Label>
                </div>
                <asp:Label ID="label7" class="NoRev" runat="server" Text=""></asp:Label>

                <div class="YesNo">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                        <asp:ListItem ID="Yess" runat="server" Text="可以" Value="y" Selected="True" />
                        <asp:listitem ID="Noo" runat="server" Text="不可以" Value="n" />
                    </asp:RadioButtonList>
                </div>
                <div class="RevTextbox">
                    <asp:Panel ID="Panel2" runat="server">
                    <asp:Label ID="label4" runat="server" Text="設定目標為"></asp:Label>
                    <asp:TextBox class="TextBoxStyle" type="text" ID="MoneyTextbox" placeholder="請輸入金額" runat="server" height="30px" width="100px" ></asp:TextBox>
                    <asp:Label ID="label5" runat="server" Text="元"></asp:Label>
                        <br />
                        <asp:Label ID="ErrorMessagel" runat="server" CssClass="ErrorMessage" style="color:red;font-size:20px" Visible="false"></asp:Label>
                    </asp:Panel>
                </div>
                <div class="RevTextbox2">
                    <asp:Panel ID="Panel3" runat="server" Visible="false">
                    <asp:Label ID="label6" runat="server" Text="拒絕原因："></asp:Label><br />
                    <asp:TextBox class="TextBoxStyle" type="text" ID="CauseTextbox" placeholder="請輸入原因" runat="server" height="30px" width="250px"></asp:TextBox>
                        <br />
                    <asp:Label ID="ErrorMessage2" runat="server" CssClass="ErrorMessage" Visible="false" style ="color:red;font-size:20px"></asp:Label>
                     </asp:Panel>
                </div>
                <asp:Label ID="IndexCount" runat="server" Text="" Visible="false"></asp:Label> 
            </div>
        </div>
        <asp:Button class="BucButton ButtonStyle ButtonSize2" ID="Button3" runat="server" Text="確認" OnClick="Submit_Click" />

        <div class="LastNext">
            <div><asp:Button class="ButtonStyle LastNextSize" ID="Last" runat="server" Text="<" CommandArgument="minus" OnClick="ChangeWish_Click"/></div><%--上一個願望--%>
            <div><asp:Button class="ButtonStyle LastNextSize" ID="Next" runat="server" Text=">" CommandArgument= "plus" OnClick="ChangeWish_Click"/></div><%--下一個願望--%>
        </div>
        </div>
        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/bucket_list.aspx" />
        <div id="overlay"></div>
    </form>

</body>
</html>