<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bucket_trashcan.aspx.cs" Inherits="_BookKeeping.bucket_trashcan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>願望垃圾桶</title>
</head>

<body>
    <form id="form1" runat="server">
        <div class="BucDiv">
        <div class="BMemo MemoStyle2">
            <div class="TraWord">
                <div class="TraCause">
                    <asp:Label ID="CantBuy" runat="server" Text="不能買"></asp:Label><br />
                    <asp:Label ID="Cause" runat="server" Text="因為"></asp:Label>
                </div>
                <asp:Label ID="NoCantBuy" class="NoCantBuy" runat="server" Text=""></asp:Label><br />
            </div>
            <asp:Button  class="TraButton ButtonStyle ButtonSize2" ID="Submit1" Text="知道了" runat="server" OnClick="Submit1_Click"/>
            <asp:Label ID="wish" runat="server"></asp:Label>
        </div>
        <div class="LastNext">
            <div><asp:Button class="ButtonStyle LastNextSize" ID="Last" runat="server" Text="<" Onclick="Last_Click"/></div><%--上一個願望--%>
            <div><asp:Button class="ButtonStyle LastNextSize" ID="Next" runat="server" Text=">" Onclick="Next_Click" /></div><%--下一個願望--%>
        </div>
        </div>
        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/bucket_list.aspx" />
    </form>
</body>
</html>