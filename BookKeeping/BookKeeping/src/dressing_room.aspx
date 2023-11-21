<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dressing_room.aspx.cs" Inherits="BookKeeping.src.dressing_room" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>更衣室</title>
</head>
<body>
    <form id="DreForm" runat="server">
        <div class="DreRoom">
            <div class="DrePerson" id="imageContainer">
                <asp:Image ID="NowHead" runat="server" ClientIDMode="Static" />
                <asp:Image ID="NowBody" runat="server" ClientIDMode="Static" />
                <asp:Image ID="NowPet" runat="server" ClientIDMode="Static" />
            </div>

            <div class="Left">
                <div class="Head" id="femaleClothing">
                    <asp:Repeater ID="headRepeater" runat="server" Visible="true">
                        <ItemTemplate>
                            <asp:ImageButton CssClass="ClothButton HeadImage" ID="headImage" runat="server" ImageUrl='<%# Eval("cloth_id") %>' 
                                OnClientClick='<%# "return setHeadwearImage(\"" + Eval("cloth_id") + "\");" %>' />
                            <asp:Label ID="headstatus" runat="server" text="使用中" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="Cloth" id="femaleHead"> 
                    <asp:Repeater ID="imageRepeater" runat="server" Visible="true">
                        <ItemTemplate>
                            <asp:ImageButton CssClass="ClothButton ClothImage" ID="image" runat="server" ImageUrl='<%# Eval("cloth_id") %>'
                            OnClientClick='<%# "return setPreviewImage(\"" + Eval("cloth_id") + "\");" %>' />
                            <asp:Label ID="bodystatus" runat="server" text="使用中" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="Pet"> <%--右區塊--%>
                <asp:Repeater ID="petRepeater" runat="server" Visible="true">
                    <ItemTemplate>
                        <asp:ImageButton CssClass="ClothButton PetImage" ID ="petImage" runat="server" ImageUrl='<%# Eval("cloth_id") %>'
                            OnClientClick='<%# "return setPetImage(\"" + Eval("cloth_id") + "\");" %>'/>
                        <asp:Label ID="petstatus" runat="server" text="使用中" Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="DreButton">
            <asp:Button ID="btnCancel" CssClass="ButtonStyle3 ButtonSize4" runat="server" Text="取消修改" OnClientClick="return cancelChanges();" Enabled="false" />
            <asp:Button ID="btnConfirm" CssClass="ButtonStyle3 ButtonSize4" runat="server" Text="確認修改" OnClick="BtnConfirm_Click" Enabled="false" />
            </div>
            <asp:HiddenField ID="hiddenClothingID" runat="server" />
            <asp:HiddenField ID="hiddenHeadwearID" runat="server" />
            <asp:HiddenField ID="hiddenPetID" runat="server" />
            <asp:label ID="gender" runat="server" text="" Visible="false"></asp:label>
        </div>
        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/main.aspx" />
        <div id="overlay"></div>

        <script type="text/javascript">
            function updateImages(newClothingURL, newHeadwearURL, newPetURL) {
                // 更新衣物图片
                document.getElementById("NowBody").src = newClothingURL;
                // 更新头饰图片
                document.getElementById("NowHead").src = newHeadwearURL;

                document.getElementById("NowPet").src = newPetURL;
            }

            // 在页面加载时执行检查和设置状态
            window.onload = function () {
                checkImagesInRepeater();
            }

            function setPetImage(imagePath) {
                var petImage = document.getElementById("NowPet");
                petImage.src = imagePath;

                document.getElementById('<%= hiddenPetID.ClientID %>').value = imagePath;

                var confirmButton = document.getElementById('<%= btnConfirm.ClientID %>');
                var cancelButton = document.getElementById('<%= btnCancel.ClientID %>');
                confirmButton.disabled = false;
                cancelButton.disabled = false;

                // 在此处调用updateImages，传递新的衣物URL和头饰URL
                updateImages(document.getElementById("NowBody").src, document.getElementById("NowHead").src, imagePath);

                return false;
            }

            function setPreviewImage(imagePath) {
                var nowImage = document.getElementById("NowBody");
                nowImage.src = imagePath;

                // 设置隐藏字段的值為user所選的衣服ID
                document.getElementById('<%= hiddenClothingID.ClientID %>').value = imagePath;

                // 启用确认修改按钮
                var confirmButton = document.getElementById('<%= btnConfirm.ClientID %>');
                var cancelButton = document.getElementById('<%= btnCancel.ClientID %>');
                confirmButton.disabled = false;
                cancelButton.disabled = false;

                // 在此处调用updateImages，传递新的衣物URL和头饰URL
                updateImages(imagePath, document.getElementById("NowHead").src, document.getElementById("NowPet").src);

                return false; // 阻止頁面刷新
            }

            function setHeadwearImage(imagePath) {
                var headImage = document.getElementById("NowHead");
                headImage.src = imagePath;

                // 设置隐藏字段的值为用户选择的头饰ID
                document.getElementById('<%= hiddenHeadwearID.ClientID %>').value = imagePath;

                // 启用确认修改按钮
                var confirmButton = document.getElementById('<%= btnConfirm.ClientID %>');
                var cancelButton = document.getElementById('<%= btnCancel.ClientID %>');
                confirmButton.disabled = false;
                cancelButton.disabled = false;

                // 在此处调用updateImages，传递新的衣物URL和头饰URL
                updateImages(document.getElementById("NowBody").src, imagePath, document.getElementById("NowPet").src);

                return false;
            }
        </script>
    </form>
</body>
</html>