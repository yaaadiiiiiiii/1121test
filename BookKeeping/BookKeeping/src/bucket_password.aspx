<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bucket_password.aspx.cs" Inherits="BookKeeping.bucket_password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>審核密碼</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="BucDiv">
        <div class="BMemo MemoStyle2">
            <div class="DreWord">
                <label class="BucPwTitle">家長審核密碼</label>
                <asp:TextBox class="TextBoxStyle" ID="UserPwd" runat="server" Width="250" Height="35" placeholder="請輸入密碼" Font-Size="13"></asp:TextBox>
                <a class="RevForgetPwd" href="forget_bucket_pwd.aspx">忘記密碼</a>
                <asp:Label ID="ErrorMessageLabel" CssClass="ErrorMessageLabel" runat="server" Text="" />
                <br />
                <div class="Qua">
                <asp:Label ID="question" runat="server" Text="安全問題" Visible="false"></asp:Label>
                <asp:DropDownList ID="securityQuestion" runat="server" Visible="false" CssClass="DropDownStyle" Width="170" Height="40" style ="margin-bottom:10px;">
                    <asp:ListItem Text="請選擇問題" Value="" />
                    <asp:ListItem Text="請問您最喜歡的書籍" Value="1" />
                    <asp:ListItem Text="請問您最喜歡的電影" Value="2" />
                </asp:DropDownList>
                <asp:TextBox class="TextBoxStyle" ID="securityAnswer" runat="server" Width="90" Height="30" style ="margin-bottom:10px;" placeholder="請輸入答案"></asp:TextBox>
                </div>

                <p><asp:Button class="BucPwButton ButtonStyle ButtonSize2" ID="LoginButton" runat="server" OnClick="LoginButton_Click" Text="確認" /></p>
            </div>
        </div>
        </div>
        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/bucket_list.aspx" />
        <div id="overlay"></div>
    </form>

    <%--<script>
        function storeFormData() {
            var pwd = document.getElementById("UserPwd").value;

            var formData = {
                pwd: pwd,
            };

            localStorage.setItem("formData", JSON.stringify(formData));
        }

        function restoreFormData() {
            var formData = localStorage.getItem("formData");
            if (formData) {
                formData = JSON.parse(formData);

                document.getElementById("UserPwde").value = formData.pwd;
            }
        }
    </script>--%>
</body>
</html>