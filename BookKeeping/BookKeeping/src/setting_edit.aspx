<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setting_edit.aspx.cs" Inherits="BookKeeping.src.setting_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link rel="stylesheet" type="text/css" href="styles.css" />
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>設定</title>
</head>
<body>
    <form class="SetForm" id="form2" runat="server">
        <div class="SetBody">
            <h1 class="SetTitle">個人資料</h1>
            <div class="SetContent">
                <p>
                    <label>暱稱 </label>
                    <asp:TextBox class="SetTextBoxStyle" ID="SetNickname" runat="server"></asp:TextBox>
                </p>
                <p>
                    <label>性別 </label>            
                    <asp:Label ID="gender" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    <label>帳號 </label>
                    <asp:Label ID="account" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    <label>生日 </label>
                    <asp:Label ID="Birthday" runat="server"></asp:Label>
                </p>
            </div>
            <asp:Image class="Ava" ID="Avatar" runat="server" ImageUrl="images/avatar/ava_girl.png"/>
            <asp:Button class="ButtonStyle ButtonSize5" ID="Xx" runat="server" Text="x" PostBackUrl="~/src/main.aspx"/>
            <asp:Button class="ButtonStyle ButtonSize2" ID="GoBack" runat="server" Text="返回" PostBackUrl="~/src/setting.aspx"/>
            <asp:Button class="ButtonStyle ButtonSize4" ID="EditOk" runat="server" OnClick="EditOk_Click" Text="確認修改" />
        </div>
        <div id="overlay" style="border-radius:25px;"></div>
    </form>

    <script type="text/javascript">
    function validateNumberInput(event) {
        var charCode = (event.which) ? event.which : event.keyCode;

        // 允许退格和删除键
        if (charCode == 8 || charCode == 46) {
            return true;
        }

        // 确保输入的是数字
        if (charCode < 48 || charCode > 57) {
            event.preventDefault();
            return false;
        }

        return true;
    }
    </script>


</body>
</html>