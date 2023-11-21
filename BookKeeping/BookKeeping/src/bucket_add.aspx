<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bucket_add.aspx.cs" Inherits="_BookKeeping.bucket_add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>願望新增</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="BucDiv">
        <div class="YMemo MemoStyle">
            <div class="BucWord">
                <asp:Label ID="WishUser" runat="server" Text=""></asp:Label>
                <div class="BucAddButton">
                    <asp:Button class="ButtonStyle ButtonSize2" ID="BucAdd" runat="server" Text="確認" Onclick="Submit_Click"/>
                    <asp:Button class="ButtonStyle ButtonSize2" ID="BucUnadd" runat="server" Text="取消" PostBackUrl="~/src/bucket_list.aspx" />
                </div>
                <div id="customSelect">
                  <asp:TextBox class="TextBoxStyle" ID="WishTextbox" PlaceHolder="請輸入或選擇願望" runat="server" ></asp:TextBox>
                  <div id="arrowIcon">▼</div>
                  <div id="customOptions"></div>
                </div>
            </div>
            <asp:Label ID="ErrorMessage1" runat="server" CssClass="ErrorMessage" Visible="false" style ="color:red;font-size:20px"></asp:Label>
        </div>
        </div>
        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/bucket_list.aspx" />
        <div id="overlay"></div>
    </form>

    <script>
        // 模擬選項
        const options = ['絨毛娃娃', '玩具車', '鉛筆盒', '遊戲機', '吃大餐'];

        // 取得元素
        const WishTextbox = document.getElementById('WishTextbox');
        const arrowIcon = document.getElementById('arrowIcon');
        const customOptions = document.getElementById('customOptions');

        // 建立下拉式選單
        options.forEach(optionText => {
            const optionElement = document.createElement('div');
            optionElement.classList.add('option');
            optionElement.textContent = optionText;
            optionElement.addEventListener('click', () => {
                WishTextbox.value = optionText;
                customOptions.style.display = 'none';
            });
            customOptions.appendChild(optionElement);
        });

        // 監聽點擊箭頭事件
        arrowIcon.addEventListener('click', () => {
            customOptions.style.display = (customOptions.style.display === 'none') ? 'block' : 'none';
        });

        // 監聽點擊頁面其他區域的事件，以隱藏選項
        document.addEventListener('click', event => {
            if (!event.target.closest('#customSelect')) {
                customOptions.style.display = 'none';
            }
        });

        // 阻止點擊選項時冒泡至 document 造成 document click 事件觸發
        customOptions.addEventListener('click', event => {
            event.stopPropagation();
        });
    </script>
</body>
</html>