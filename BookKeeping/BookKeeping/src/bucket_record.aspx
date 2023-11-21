<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bucket_record.aspx.cs" Inherits="BookKeeping.src.bucket_record" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>願望歷史紀錄</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="BucDiv">
        <div class="Notebook">
            <asp:Label ID="Label1" runat="server" CssClass="NoteTitle" Text="願望歷史紀錄"></asp:Label><br />
            <div class="NotebookText">
            <asp:GridView class="Gridview3" ID="GridView2" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="d_num" HeaderText="編號" Visible="false"/>
                    <asp:BoundField DataField="d_name" HeaderText="名稱" />
                    <asp:BoundField DataField="pass_amount" HeaderText="金額" />
                    <asp:TemplateField HeaderText="狀態">
                        <ItemTemplate>
                            <asp:Label ID="StatusLabel" style="color: #2f004b;" runat="server" Text='<%# GetExchangeStatus(Eval("exchange_state")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="exchange_time" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" />
                </Columns>
            </asp:GridView>
            </div>
        </div>
        </div>
        <asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/bucket_list.aspx" />
    </form>
</body>
</html>