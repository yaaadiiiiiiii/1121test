<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="game_third.aspx.cs" Inherits="BookKeeping.src.game_third" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- 引用你的 CSS 文件 -->
    <link rel="stylesheet" type="text/css" href="styles.css" />

    <!-- 引用 Bootstrap 的 CSS 文件 -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />

    <!-- 引用 Bootstrap 的 JavaScript 文件（jQuery 需要在 Bootstrap 之前引入） -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>遊戲三-買文具</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="GameBackground3">
        <asp:Panel ID="ThirdGamePanel1" runat="server"><%--遊戲三之一--%>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
         
            <div class="GameLeft3_1"><%--左半邊題目--%>
                <div class="SisQ" draggable="true" id='<%# Eval("SisRandomNumber") %>'>
                        <asp:Image ID="Image9" class="ListSize" runat="server" ImageUrl="images/game/game3_sis.png" />
                        <asp:Label ID="Sis" ClientIDMode="Static" runat="server" Text='<%# Eval("SisRandomNumber") %>' Visible="false"></asp:Label>
                        <span>X<%# Eval("SisRandomNumber") %></span>
                    </div>

                    <div class="GlueQ" draggable="true" id='<%# Eval("GlueRandomNumber") %>'>
                        <asp:Image ID="Image10" class="ListSize" runat="server" ImageUrl="images/game/game3_glue.png" />
                        <asp:Label ID="Glue" runat="server" Text='<%# Eval("GlueRandomNumber") %>' Visible="false"></asp:Label>
                        <span>X<%# Eval( "GlueRandomNumber") %></span>
                    </div>

                    <div class="CorQ" draggable="true" id='<%# Eval("CorRandomNumber") %>'>
                        <asp:Image ID="Image11" class="ListSize" runat="server" ImageUrl="images/game/game3_cor.png" />
                        <asp:Label ID="Cor" runat="server" Text='<%# Eval("CorRandomNumber") %>' Visible="false"></asp:Label>
                        <span>X<%# Eval("CorRandomNumber") %></span>
                    </div>

                    <div class="RulerQ" draggable="true" id='<%# Eval("RulerRandomNumber") %>'>
                        <asp:Image ID="Image12" class="ListSize" runat="server" ImageUrl="images/game/game3_ruler.png" />
                        <asp:Label ID="Ruler" runat="server" Text='<%# Eval("RulerRandomNumber") %>' Visible="false"></asp:Label>
                        <span>X<%# Eval("RulerRandomNumber") %></span>
                    </div>

                    <div class="RedQ" draggable="true" id='<%# Eval("RedRandomNumber") %>'> 
                        <asp:Image ID="Image13" class="ListSize" runat="server" ImageUrl="images/game/game3_red.png" />
                        <asp:Label ID="Red" runat="server" Text='<%# Eval("RedRandomNumber") %>' Visible="false"></asp:Label>
                        <span>X<%# Eval("RedRandomNumber") %></span>
                    </div>

                    <div class="GreenQ" draggable="true" id='<%# Eval("GreenRandomNumber") %>'>
                        <asp:Image ID="Image14" class="ListSize" runat="server" ImageUrl="images/game/game3_green.png" />
                        <asp:Label ID="Green" runat="server" Text='<%# Eval("GreenRandomNumber") %>' Visible="false"></asp:Label>
                        <span>X<%# Eval("GreenRandomNumber") %></span>
                    </div>

                    <div class="BlueQ" draggable="true" id='<%# Eval("BlueRandomNumber") %>'>
                        <asp:Image ID="Image15" class="ListSize" runat="server" ImageUrl="images/game/game3_blue.png" />
                        <asp:Label ID="Blue" runat="server" Text='<%# Eval("BlueRandomNumber") %>' Visible="false"></asp:Label>
                        <span>X<%# Eval("BlueRandomNumber") %></span>
                    </div>

                    <div class="BlackQ" draggable="true" id='<%# Eval("BlackRandomNumber") %>'>
                        <asp:Image ID="Image16" class="ListSize" runat="server" ImageUrl="images/game/game3_black.png" />
                        <asp:Label ID="Black" runat="server" Text='<%# Eval("BlackRandomNumber") %>' Visible="false"></asp:Label>
                        <span>X<%# Eval("BlackRandomNumber") %></span>
                    </div>
            </div>
              
         </ItemTemplate>
        </asp:Repeater>

            <div class="Stationery"><%--文具區--%>
                <asp:Image class="draggable PenSize" ID="Redd" runat="server" ImageUrl="images/game/game3_red2.png"/>
                <asp:Image class="draggable PenSize" ID="Greenn" runat="server" ImageUrl="images/game/game3_green2.png"/>
                <asp:Image class="draggable PenSize" ID="Bluee" runat="server" ImageUrl="images/game/game3_blue2.png"/>
                <asp:Image class="draggable PenSize" ID="Blackk" runat="server" ImageUrl="images/game/game3_black2.png"/>
                <asp:Image class="draggable StaSize" ID="Siss" runat="server" ImageUrl="images/game/game3_sis2.png"/>
                <asp:Image class="draggable StaSize" ID="Gluee" runat="server" ImageUrl="images/game/game3_glue2.png"/>
                <asp:Image class="draggable StaSize" ID="Corr" runat="server" ImageUrl="images/game/game3_cor2.png"/>
                <asp:Image class="draggable StaSize" ID="Rulerr" runat="server" ImageUrl="images/game/game3_ruler2.png"/>
            </div>

            <div class="ShoppingCart" id="ShoppingCart">
                <asp:Label ID="ShoppingCartText" runat="server" Text="Label" Style="color: #63a397;">購物車</asp:Label>
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
                            // 保存原始圖片URL
                            draggable.dataset.originalSrc = draggable.src;

                            // 修改拖曳的元素的圖片URL
                            if (draggable.id == 'Redd') {
                                draggable.src = "images/game/game3_red.png";
                            } else if (draggable.id == 'Greenn') {
                                draggable.src = "images/game/game3_green.png";
                            } else if (draggable.id == 'Bluee') {
                                draggable.src = "images/game/game3_blue.png";
                            } else if (draggable.id == 'Blackk') {
                                draggable.src = "images/game/game3_black.png";
                            } else if (draggable.id == 'Siss') {
                                draggable.src = "images/game/game3_sis.png";
                            } else if (draggable.id == 'Gluee') {
                                draggable.src = "images/game/game3_glue.png";
                            } else if (draggable.id == 'Corr') {
                                draggable.src = "images/game/game3_cor.png";
                            } else if (draggable.id == 'Rulerr') {
                                draggable.src = "images/game/game3_ruler.png";
                            } 

                            event.dataTransfer.setData('text/plain', draggable.id);
                        });

                        draggable.addEventListener('dragend', function (event) {
                            // 還原原始圖片URL
                            draggable.src = draggable.dataset.originalSrc;
                        });
                    });

                    ShoppingCart.addEventListener('dragover', function (event) {
                        event.preventDefault();
                    });

                    ShoppingCart.addEventListener('drop', function (event) {
                        event.preventDefault();
                        // 檢查是否已達到最大元素數量
                        var maxItems = 24;
                        var existingItems = ShoppingCart.getElementsByClassName('draggable-item').length;
                        if (existingItems >= maxItems) {
                            // 如果已達到最大元素數量，則不允許添加新元素
                            console.log('已達到最大元素數量，不允許添加新元素');
                            return;
                        }
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
                            // 設定元素的初始位置
                            var initialPositionX = 55; // 起始橫向位置
                            var initialPositionY = 70; // 起始縱向位置
                            clonedElement.style.left = initialPositionX + 'px';
                            clonedElement.style.top = initialPositionY + 'px';
                            // 計算新元素的位置
                            var itemsInRow = 12; // 每行元素数量
                            var newPositionX = (existingItems % itemsInRow) * 50; // 每個元素的寬度
                            var newPositionY = Math.floor(existingItems / itemsInRow) * 110; // 每個元素的高度
                            // 將初始位置加上新元素位置
                            var finalPositionX = initialPositionX + newPositionX;
                            var finalPositionY = initialPositionY + newPositionY;
                            clonedElement.style.left = finalPositionX + 'px';
                            clonedElement.style.top = finalPositionY + 'px';
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

       <div hidden="hidden"> <asp:Label ID="RedLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="GreenLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="BlueLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="BlackLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="SisLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="GlueLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="CorLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
        <asp:Label ID="RulerLabel" runat="server" Text="0" Style="font-size: 16px;"></asp:Label>
           </div>
   </asp:Panel>

  <asp:Panel ID="ThirdGamePanel2" runat="server" ><%--遊戲三之二--%>
            <div class="GameLeft3_2">
                 <p id="TotalMoney">一共是<span id="TotalMoney2">&nbsp;&nbsp;&nbsp;&nbsp;</span>元</p> <%--結帳金額--%>
                <asp:Image ID="Clerk" runat="server" ImageUrl="images/game/game_clerk.png"/>
                <asp:Button class="ButtonStyle3" ID="Correct2" runat="server" Text="完成"  OnClick="Check3_2_Click"/>
                <%--<asp:Button class="ButtonStyle3" ID="GameReset2" runat="server" Text="重置"  OnClick="Check3_2_Click"/>--%>

                <div class="Checkout" id="Checkout">
                    <asp:Label ID="Label9" runat="server" Text="Label" Style="color: #301305;">結帳區</asp:Label>
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
                            var draggedElement = event.target;
                            event.dataTransfer.setDragImage(draggedElement, 0, 0);
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
                        var maxItems = 24;
                        var existingItems = Checkout.getElementsByClassName('draggable-item').length;
                        if (existingItems >= maxItems) {
                            // 如果已達到最大元素數量，則不允許添加新元素
                            console.log('已達到最大元素數量，不允許添加新元素');
                            return;
                        }
                        var data = event.dataTransfer.getData('text/plain');
                        var draggedElement = document.getElementById(data);

                        if (draggedElement && draggedElement.classList.contains('draggable')) {
                            var moneyValue = getMoneyValue(draggedElement.id);

                            var clonedElement = draggedElement.cloneNode(true);
                            clonedElement.classList.add('draggable-item');

                            // 設定元素的初始位置
                            var initialPositionX = 80; // 起始橫向位置
                            var initialPositionY = 70; // 起始縱向位置
                            clonedElement.style.left = initialPositionX + 'px';
                            clonedElement.style.top = initialPositionY + 'px';
                            // 計算新元素的位置
                            var itemsInRow = 5; // 每行元素数量
                            var newPositionX = (existingItems % itemsInRow) * 68; // 每個元素的寬度
                            var newPositionY = Math.floor(existingItems / itemsInRow) * 50; // 每個元素的高度
                            // 將初始位置加上新元素位置
                            var finalPositionX = initialPositionX + newPositionX;
                            var finalPositionY = initialPositionY + newPositionY;
                            clonedElement.style.left = finalPositionX + 'px';
                            clonedElement.style.top = finalPositionY + 'px';
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
            <div hidden ="hidden"><asp:Label ID="TotalAmountLabel" runat="server" Text="0" Style="font-size: 24px;" ></asp:Label><%--紀錄目前結帳區的金額--%></div>
       </asp:Panel>

        <asp:Button class="ButtonStyle" ID="LeaveGame3" runat="server" Text="結束遊戲" OnClick="LeaveGame_Click" />

        <input type="hidden" name="hiddenred" id="hiddenred" value="0"/>
        <input type="hidden" name="hiddengreen" id="hiddengreen" value="0"/>
        <input type="hidden" name="hiddenblue" id="hiddenblue" value="0"/>
        <input type="hidden" name="hiddenblack" id="hiddenblack" value="0"/>
        <input type="hidden" name="hiddensis" id="hiddensis" value="0"/>
        <input type="hidden" name="hiddenglue" id="hiddenglue" value="0"/>
        <input type="hidden" name="hiddencor" id="hiddencor" value="0"/>
        <input type="hidden" name="hiddenruler" id="hiddenruler" value="0"/>
        <input type="hidden" name="hiddentotal" id="hiddentotal" value="0"/>
        <div hidden="hidden"><asp:Label ID="correctcnt" runat="server" Text="0"></asp:Label><%--紀錄答對題數--%></div>
        </div>
        <div id="resultModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="resultModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-body">
                        <p id="resultMessage"></p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" text="再玩一次" class="ButtonStyle3 ButtonSize2" OnClick="RestartGame" />
                        <asp:Button runat="server" text="結束遊戲" class="ButtonStyle3 ButtonSize2" PostBackUrl="game_menu.aspx" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>