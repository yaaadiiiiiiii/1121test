<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forget_bucket_pwd.aspx.cs" Inherits="BookKeeping.forget_bucket_pwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>審核密碼設定</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="BucDiv">
        <div class="BMemo MemoStyle">
            <div class="TraWord">
                <label class="BucPwTitle">重設審核密碼</label>
                <div class="ForgetRevPwText">
                    <p>安全問題
                    <asp:DropDownList class="DropDownStyle" ID="securityQuestion" runat="server" Width="195" Height="30" style ="margin-bottom:10px; padding-left:10px;">
                        <asp:ListItem Text="請選擇問題" Value="" />
                        <asp:ListItem Text="請問您最喜歡的書籍" Value="1" />
                        <asp:ListItem Text="請問您最喜歡的電影" Value="2" />
                    </asp:DropDownList>
                    </p>
                    <p>回答問題
                    <asp:TextBox class="TextBoxStyle" ID="securityAnswer" runat="server" Width="182" Height="20" style ="margin-bottom:10px; padding-left:10px;" placeholder="請輸入答案" ></asp:TextBox>
                    </p>
                    <p>重設審核密碼
                    <asp:TextBox class="TextBoxStyle" ID="UserPwd" runat="server" Width="145" Height="20" style ="margin-bottom:10px; padding-left:10px;" placeholder="請輸入審核密碼" ></asp:TextBox>
                    </p>
                    <p>確認審核密碼
                    <asp:TextBox class="TextBoxStyle" ID="TextBox1" runat="server" Width="145" Height="20" style ="margin-bottom:10px; padding-left:10px;" TextMode="Password" placeholder="請再次輸入審核密碼" ></asp:TextBox>
                    </p>
                </div>
                <div class="ForgetRevPwButtn">
                    <asp:Button class="ButtonStyle3 ButtonSize2" ID="ReturnButton" runat="server" Text="返回" PostBackUrl="~/src/bucket_password.aspx" />
                    <asp:Button class="ButtonStyle3 ButtonSize2" ID="LoginButton" runat="server" OnClick="LoginButton_Click" Text="確認" />
                </div>
            </div>
        </div>
        </div>
        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/bucket_list.aspx" />
        <div id="overlay"></div>
    </form>
</body>
</html>