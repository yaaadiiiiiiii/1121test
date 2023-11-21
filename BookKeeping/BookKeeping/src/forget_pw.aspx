<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forget_pw.aspx.cs" Inherits="BookKeeping.src.forget_pw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>忘記密碼</title>
</head>
<body>
    <div class="LogBody">
    <form class="LogForm" id="form1" runat="server">
        <h1 class="ForgetPwdTitle">重設密碼</h1>
        <div class="ForgetPwdText">
            <div class="RegTextBlock">
            <p style="margin-left:30px;">帳號
            <asp:TextBox class="TextBoxStyle" ID="enteraccount" runat="server" style="padding-left:10px;" Width="250px" Height="20px" placeholder="輸入帳號"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEnterAccount" runat="server" class="ForgetPwError" ControlToValidate="enteraccount" InitialValue="" ErrorMessage="請填寫帳號" ForeColor="Red" />
            </p>

            <p>安全問題
            <asp:DropDownList class = "DropDownList TextBoxStyle" ID="secretQuestion" runat="server" CssClass="DropDownStyle" Width="265px" Height="30px" >
                <asp:ListItem Text="請選擇問題1" Value="" />
                <asp:ListItem Text="請問您的出生地在哪?" Value="1" />
                <asp:ListItem Text="請問你住在哪裡" Value="2" />
            </asp:DropDownList>
            </p>

            <p>回答問題
            <asp:TextBox class="TextBoxStyle" ID="secretAnswer" runat="server" style="padding-left:10px; margin-top:10px;" Width="250px" Height="20px" placeholder="請輸入答案"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSecretAnswer" runat="server" class="ForgetPwError" ControlToValidate="secretAnswer" InitialValue="" ErrorMessage="請填寫答案" ForeColor="Red" />
            </p>
                
            <p>重設密碼
            <asp:TextBox class="TextBoxStyle" ID="newanswer" runat="server" style="padding-left:10px;" Width="250px" Height="20px" placeholder="請輸入新密碼"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNewAnswer" runat="server" class="ForgetPwError" ControlToValidate="newanswer" InitialValue="" ErrorMessage="請填寫新密碼" ForeColor="Red" />
            </p>

            <p>確認密碼
            <asp:TextBox class="TextBoxStyle" ID="TextBox1" runat="server" style="padding-left:10px;" Width="250px" Height="20px" placeholder="請輸入新密碼"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" class="ForgetPwError" ControlToValidate="newanswer" InitialValue="" ErrorMessage="請再次填寫新密碼" ForeColor="Red" />
            </p>

    
            <asp:Label style="font-size:20px;" ID="ErrorMessage" runat="server" Text="" />

            <br />
            <div class="ForgetButton">
                <asp:Button class="ButtonStyle3 ButtonSize1" ID="ReturnButton" runat="server" Text="返回" OnClick="ReturnButton_Click" CausesValidation="false" />
                <asp:Button class="ButtonStyle3 ButtonSize1" ID="Button2" runat="server" OnClick="Comfirm_Click" Text="確認" />
            </div>
            </div>
        </div>
        <div id="overlay"></div>
    </form>
    </div>
</body>
</html>
