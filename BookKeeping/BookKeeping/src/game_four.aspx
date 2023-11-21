<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="game_four.aspx.cs" Inherits="BookKeeping.src.game_four" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>題型一-認識錢幣</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="ThirdGamePanel1" runat="server"><%--遊戲三之一--%>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
         
            <div class="GameLeft3_1"><%--左半邊題目--%>
                <div class="SisQ" draggable="true" id='<%# Eval("SisRandomNumber") %>'>
                        <asp:Image ID="Image9" runat="server" Height="168px" Width="88px" ImageUrl="images/game/game3_sis.png" />
                        <asp:Label ID="Sis" ClientIDMode="Static" runat="server" Text='<%# Eval("SisRandomNumber") %>' Visible="false"></asp:Label>
                        <span><%# Eval("SisRandomNumber") %></span>
                    </div>

                    <div class="GlueQ" draggable="true" id='<%# Eval("GlueRandomNumber") %>'>
                        <asp:Image ID="Image10" runat="server" Height="168px" Width="88px" ImageUrl="images/game/game3_glue.png" />
                        <asp:Label ID="Glue" runat="server" Text='<%# Eval("GlueRandomNumber") %>' Visible="false"></asp:Label>
                        <span><%# Eval( "GlueRandomNumber") %></span>
                    </div>

                    <div class="CorQ" draggable="true" id='<%# Eval("CorRandomNumber") %>'>
                        <asp:Image ID="Image11" runat="server" Height="168px" Width="88px" ImageUrl="images/game/game3_cor.png" />
                        <asp:Label ID="Cor" runat="server" Text='<%# Eval("CorRandomNumber") %>' Visible="false"></asp:Label>
                        <span><%# Eval("CorRandomNumber") %></span>
                    </div>

                    <div class="RulerQ" draggable="true" id='<%# Eval("RulerRandomNumber") %>'>
                        <asp:Image ID="Image12" runat="server" Height="168px" Width="88px" ImageUrl="images/game/game3_ruler.png" />
                        <asp:Label ID="Ruler" runat="server" Text='<%# Eval("RulerRandomNumber") %>' Visible="false"></asp:Label>
                        <span><%# Eval("RulerRandomNumber") %></span>
                    </div>

                    <div class="RedQ" draggable="true" id='<%# Eval("RedRandomNumber") %>'> 
                        <asp:Image ID="Image13" runat="server" Height="168px" Width="88px" ImageUrl="images/game/game3_red.png" />
                        <asp:Label ID="Red" runat="server" Text='<%# Eval("RedRandomNumber") %>' Visible="false"></asp:Label>
                        <span><%# Eval("RedRandomNumber") %></span>
                    </div>

                    <div class="GreenQ" draggable="true" id='<%# Eval("GreenRandomNumber") %>'>
                        <asp:Image ID="Image14" runat="server" Height="168px" Width="88px" ImageUrl="images/game/game3_green.png" />
                        <asp:Label ID="Green" runat="server" Text='<%# Eval("GreenRandomNumber") %>' Visible="false"></asp:Label>
                        <span><%# Eval("GreenRandomNumber") %></span>
                    </div>

                    <div class="BlueQ" draggable="true" id='<%# Eval("BlueRandomNumber") %>'>
                        <asp:Image ID="Image15" runat="server" Height="168px" Width="88px" ImageUrl="images/game/game3_blue.png" />
                        <asp:Label ID="Blue" runat="server" Text='<%# Eval("BlueRandomNumber") %>' Visible="false"></asp:Label>
                        <span><%# Eval("BlueRandomNumber") %></span>
                    </div>

                    <div class="BlackQ" draggable="true" id='<%# Eval("BlackRandomNumber") %>'>
                        <asp:Image ID="Image16" runat="server" Height="168px" Width="88px" ImageUrl="images/game/game3_black.png" />
                        <asp:Label ID="Black" runat="server" Text='<%# Eval("BlackRandomNumber") %>' Visible="false"></asp:Label>
                        <span><%# Eval("BlackRandomNumber") %></span>
                    </div>
            </div>
              
         </ItemTemplate>
        </asp:Repeater>

            <div class="Stationery"><%--文具區--%>
                <asp:Image class="draggable" ID="Redd" runat="server" Height="240px" Width="155px" ImageUrl="images/game/game3_red2.png"/>
                <asp:Image class="draggable" ID="Greenn" runat="server" Height="240px" Width="155px" ImageUrl="images/game/game3_green2.png"/>
                <asp:Image class="draggable" ID="Bluee" runat="server" Height="240px" Width="155px" ImageUrl="images/game/game3_blue2.png"/>
                <asp:Image class="draggable" ID="Blackk" runat="server" Height="240px" Width="155px" ImageUrl="images/game/game3_black2.png"/>
                </br>
                <asp:Image class="draggable" ID="Siss" runat="server" Height="130px" Width="160px" ImageUrl="images/game/game3_sis2.png"/>
                <asp:Image class="draggable" ID="Gluee" runat="server" Height="130px" Width="160px" ImageUrl="images/game/game3_glue2.png"/>
                <asp:Image class="draggable" ID="Corr" runat="server" Height="130px" Width="160px" ImageUrl="images/game/game3_cor2.png"/>
                <asp:Image class="draggable" ID="Rulerr" runat="server" Height="130px" Width="160px" ImageUrl="images/game/game3_ruler2.png"/>
            </div>

            <div class="ShoppingCart" id="ShoppingCart">
                <asp:Label ID="ShoppingCartText" runat="server" Text="Label" Style="font-size: 100px;">購物車</asp:Label>
            </div><%--購物車--%>

            <asp:Image ID="Trolley" runat="server" ImageUrl="images/game/game_trolley.png"/>
            <asp:Button class="ButtonStyle3 ButtonSize3" ID="Correct1" runat="server" Text="V" OnClick="Check3_1_Click"/>

            <script type="text/javascript">
                var itemQuantities = {
                    "Redd": 0,
                    "Greenn": 0,
                    "Bluee": 0,
                    "Blackk": 0,
                    "Siss": 0,
                    "Gluee": 0,
                    "Corr": 0,
                    "Rulerr": 0,
                };

                window.onload = function () {
                    var draggableElements = document.querySelectorAll('.draggable');
                    var ShoppingCart = document.getElementById('ShoppingCart');
                    console.log('題目數量：', questionQuantities);

                    draggableElements.forEach(function (draggable) {
                        draggable.addEventListener('dragstart', function (event) {
                            var draggedElement = event.target.cloneNode(true);
                            draggedElement.classList.add('dragged-image');
                            document.body.appendChild(draggedElement);
                            event.dataTransfer.setData('text/plain', draggedElement.id);
                        });

                        draggable.addEventListener('dragend', function (event) {
                            var draggedElement = document.querySelector('.dragged-image');
                            if (draggedElement) {
                                draggedElement.parentNode.removeChild(draggedElement);
                            }
                        });
                    });

                    ShoppingCart.addEventListener('dragover', function (event) {
                        event.preventDefault();
                    });

                    ShoppingCart.addEventListener('drop', function (event) {
                        event.preventDefault();
                        var data = event.dataTransfer.getData('text/plain');
                        var draggedElement = document.getElementById(data);
                        console.log("拖曳的為" + draggedElement.id);

                        if (draggedElement && draggedElement.classList.contains('draggable')) {
                            if (isNaN(itemQuantities[draggedElement.id])) {
                                itemQuantities[draggedElement.id] = 0;
                            }

                            itemQuantities[draggedElement.id]++;
                            updateQuantityLabel('<%= RedLabel.ClientID %>', 'Redd');
                            updateQuantityLabel('<%= GreenLabel.ClientID %>', 'Greenn');
                            updateQuantityLabel('<%= BlueLabel.ClientID %>', 'Bluee');
                            updateQuantityLabel('<%= BlackLabel.ClientID %>', 'Blackk');
                            updateQuantityLabel('<%= SisLabel.ClientID %>', 'Siss');
                            updateQuantityLabel('<%= GlueLabel.ClientID %>', 'Gluee');
                            updateQuantityLabel('<%= CorLabel.ClientID %>', 'Corr');
                            updateQuantityLabel('<%= RulerLabel.ClientID %>', 'Rulerr');
                            document.getElementById('hiddenred').value = document.getElementById('RedLabel').innerText;
                            document.getElementById('hiddengreen').value = document.getElementById('GreenLabel').innerText;
                            document.getElementById('hiddenblue').value = document.getElementById('BlueLabel').innerText;
                            document.getElementById('hiddenblack').value = document.getElementById('BlackLabel').innerText;
                            document.getElementById('hiddensis').value = document.getElementById('SisLabel').innerText;
                            document.getElementById('hiddenglue').value = document.getElementById('GlueLabel').innerText;
                            document.getElementById('hiddencor').value = document.getElementById('CorLabel').innerText;
                            document.getElementById('hiddenruler').value = document.getElementById('RulerLabel').innerText;
                            console.log(itemQuantities[draggedElement.id]);

                            var clonedElement = draggedElement.cloneNode(true);
                            clonedElement.classList.add('draggable-item');
                            clonedElement.addEventListener('click', function () {
                                clonedElement.parentNode.removeChild(clonedElement);
                                if (!isNaN(itemQuantities[draggedElement.id])) {
                                    itemQuantities[draggedElement.id]--;
                                    updateQuantityLabel('<%= RedLabel.ClientID %>', 'Redd');
                                    updateQuantityLabel('<%= GreenLabel.ClientID %>', 'Greenn');
                                    updateQuantityLabel('<%= BlueLabel.ClientID %>', 'Bluee');
                                    updateQuantityLabel('<%= BlackLabel.ClientID %>', 'Blackk');
                                    updateQuantityLabel('<%= SisLabel.ClientID %>', 'Siss');
                                    updateQuantityLabel('<%= GlueLabel.ClientID %>', 'Gluee');
                                    updateQuantityLabel('<%= CorLabel.ClientID %>', 'Corr');
                                    updateQuantityLabel('<%= RulerLabel.ClientID %>', 'Rulerr');
                                    document.getElementById('hiddenred').value = document.getElementById('RedLabel').innerText;
                                    document.getElementById('hiddengreen').value = document.getElementById('GreenLabel').innerText;
                                    document.getElementById('hiddenblue').value = document.getElementById('BlueLabel').innerText;
                                    document.getElementById('hiddenblack').value = document.getElementById('BlackLabel').innerText;
                                    document.getElementById('hiddensis').value = document.getElementById('SisLabel').innerText;
                                    document.getElementById('hiddenglue').value = document.getElementById('GlueLabel').innerText;
                                    document.getElementById('hiddencor').value = document.getElementById('CorLabel').innerText;
                                    document.getElementById('hiddenruler').value = document.getElementById('RulerLabel').innerText;
                                }
                            });
                            ShoppingCart.appendChild(clonedElement);
                        }
                    });
                };
                function updateQuantityLabel(labelId, itemName) {
                    var label = document.getElementById(labelId);
                    if (label) {
                        label.textContent = itemQuantities[itemName] || 0;
                    }
                }

            </script>

        <asp:Label ID="RedLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="GreenLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="BlueLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="BlackLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="SisLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="GlueLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="CorLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="RulerLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
   </asp:Panel>

        <asp:Panel ID="ThirdGamePanel2" runat="server" Visible="true">
            <div class="GameLeft3_2">
                 <p id="TotalMoney">一共是<span id="TotalMoney2">&nbsp;&nbsp;&nbsp;&nbsp;</span>元</p> <%--結帳金額--%>
                <asp:Image ID="Clerk" runat="server" ImageUrl="images/game/game_clerk2.png"/>
                <asp:Button class="ButtonStyle3 ButtonSize2" ID="Correct2" runat="server" Text="完成"  OnClick="Check_Click"/>

                <div class="Checkout" id="Checkout">
                    <asp:Label ID="Label9" runat="server" Text="Label" Style="font-size: 80px; ">結帳區</asp:Label>
                </div>
            </div>
            
            <div class="Wallet">
                <asp:Image class="draggable" ID="Thousand" runat="server" alt="money1000" draggable="true" ImageUrl="images/game/money_1000.png"/>
                <asp:Image class="draggable" ID="FiveHundred" runat="server" alt="money500" draggable="true" ImageUrl="images/game/money_500.png"/>
                <asp:Image class="draggable" ID="Hundred" runat="server" alt="money100" draggable="true" ImageUrl="images/game/money_100.png"/>
                <asp:Image class="draggable" ID="Fifty" runat="server" alt="money50" draggable="true" ImageUrl="images/game/money_50.png"/>
                <asp:Image class="draggable" ID="Ten" runat="server" alt="money10" draggable="true" ImageUrl="images/game/money_10.png"/>
                <asp:Image class="draggable" ID="Five" runat="server" alt="money5" draggable="true" ImageUrl="images/game/money_5.png"/>
                <asp:Image class="draggable" ID="One" runat="server" alt="money1" draggable="true" ImageUrl="images/game/money_1.png"/>
            </div>

            <script type="text/javascript">
                var totalAmountInCheckout = 0;
                var totalPaymentAmount = 0;  // 初始化需付款金額
                function updateTotalPayment() {
                    // 更新顯示需付款金額的元素
                    var totalMoneyElement = document.getElementById('TotalMoney2');
                    if (totalMoneyElement) {
                        totalMoneyElement.innerText = totalPaymentAmount;
                    }
                }

                document.addEventListener('DOMContentLoaded', function () {
                    var draggableElements = document.querySelectorAll('.draggable');
                    var Checkout = document.getElementById('Checkout');

                    draggableElements.forEach(function (draggable) {
                        draggable.addEventListener('dragstart', function (event) {
                            var draggedElement = event.target.cloneNode(true);
                            draggedElement.classList.add('dragged-image');
                            document.body.appendChild(draggedElement);
                            event.dataTransfer.setData('text/plain', draggedElement.id);
                        });

                        draggable.addEventListener('dragend', function (event) {
                            var draggedElement = document.querySelector('.dragged-image');
                            if (draggedElement) {
                                draggedElement.parentNode.removeChild(draggedElement);
                            }
                        });
                    });

                    Checkout.addEventListener('dragover', function (event) {
                        event.preventDefault();
                    });

                    Checkout.addEventListener('drop', function (event) {
                        event.preventDefault();
                        var data = event.dataTransfer.getData('text/plain');
                        var draggedElement = document.getElementById(data);

                        if (draggedElement && draggedElement.classList.contains('draggable')) {
                            var moneyValue = getMoneyValue(draggedElement.id);

                            var clonedElement = draggedElement.cloneNode(true);
                            clonedElement.classList.add('draggable-item');
                            clonedElement.addEventListener('click', function () {
                                clonedElement.parentNode.removeChild(clonedElement);
                                totalAmountInCheckout -= moneyValue;
                                updateTotalAmountLabel();
                                document.getElementById("hiddentotal").value = document.getElementById('TotalAmountLabel').innerText;
                            });
                            Checkout.appendChild(clonedElement);

                            totalAmountInCheckout += moneyValue;
                            updateTotalAmountLabel();
                            document.getElementById("hiddentotal").value = document.getElementById('TotalAmountLabel').innerText;
                            console.log('總金額:', totalAmountInCheckout);
                        }
                    });
                });

                function getMoneyValue(imageId) {
                    switch (imageId) {
                        case 'Thousand':
                            return 1000;
                        case 'FiveHundred':
                            return 500;
                        case 'Hundred':
                            return 100;
                        case 'Fifty':
                            return 50;
                        case 'Ten':
                            return 10;
                        case 'Five':
                            return 5;
                        case 'One':
                            return 1;
                        default:
                            return 0;
                    }
                }
                function updateTotalAmountLabel() {
                    // 更新顯示結帳區總金額的 Label 元素
                    var totalAmountLabel = document.getElementById('TotalAmountLabel');
                    if (totalAmountLabel) {
                        totalAmountLabel.innerText = totalAmountInCheckout;
                    }
                }


            </script>
        </asp:Panel>

    <%-- <%--   <div class="GameProgress" id="GameProgressBar" runat="server" style='<%# Eval("ProgressBarStyle") %>'></div>
        <asp:Label ID="GameProgress" class="GameProgressText" runat="server" Text='<%# Eval("ProgressText") %>' ></asp:Label>
        <asp:Button class="ButtonStyle3 ButtonSize1" ID="LeaveGame" runat="server" Text="結束遊戲" PostBackUrl="~/src/game_menu.aspx" />
        <asp:Label ID="randomNum" runat="server" Text="" Visible="false"><bel"></asp:Label>--%>
        <asp:Label ID="TotalAmountLabel" runat="server" Text="0" Style="font-size: 24px;" Visible="true"></asp:Label><%--紀錄目前結帳區的金額--%>
        <input type="hidden" name="hiddentotal" id="hiddentotal" value="0"/>
    </form>

</body>
</html>