<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookkeeping_add.aspx.cs" Inherits="_BookKeeping.bookkeeping_add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
	<link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<title>記帳新增</title>
</head>

<body class="BookBody">
	<form runat="server">
	<div class="BookContent">
		<div class="BookLeft">
			<div class="BookDate">
				<asp:Button class="ButtonStyle3 DateButtonSize" ID="Button1" runat="server" Text="<" OnClick="MinusMonth_Click" CommandArgument="minus" />
				<asp:Label ID="Label1" runat="server"></asp:Label>
				<asp:Button class="ButtonStyle3 DateButtonSize" ID="Button2" runat="server" Text=">" OnClick="PlusMonth_Click" CommandArgument="plus" />
			</div>
			<h1 class="BookTitleL">記帳紀錄</h1>
			<div class="BookTable">
                <%--<asp:GridView class="Gridview" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="num" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                    OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating">

                    <Columns>
                        <asp:BoundField DataField="num" HeaderText="資料號碼" Visible="False" />
                        <asp:BoundField DataField="date" HeaderText="日期" ReadOnly="True"  DataFormatString="{0:yyyy-MM-dd}" />
						<asp:BoundField DataField="class" HeaderText="類別"/>
						<asp:BoundField DataField="cost" HeaderText="金額" />
						<asp:BoundField DataField="mark" HeaderText="備註"/>
						<asp:TemplateField>
							<ItemTemplate>
							    <asp:Button ID="btnEdit" runat="server" Text="編輯" CommandName="Edit" ToolTip="Edit" CommandArgument='<%# Eval("date") %>'/>
								<asp:Button ID="btnDelete" runat="server" Text="刪除" CommandName="Delete" CommandArgument='<%# Eval("date") %>' OnClientClick="return confirm('確定要刪除該筆資料嗎？');" />
  							</ItemTemplate>
							<EditItemTemplate>
							    <asp:Button ID="btnSave" runat="server" Text="儲存" CommandName="Save" ToolTip="Save" CommandArgument='<%# Eval("date") %>'/>
								<asp:Button ID="btnCancel" runat="server" Text="取消" CommandName="Cancel" ToolTip="Cancel" CommandArgument='<%# Eval("date") %>'/>

							</EditItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>--%>
				<asp:GridView class="Gridview" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="num"
				  OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
				  OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound" AllowSorting="true" OnSorting="GridView1_Sorting">

				<Columns>
					<asp:TemplateField HeaderText="日期" SortExpression="date">
						<ItemTemplate>
							<asp:Label ID="num" runat="server" Text='<%# Eval("num") %>' Visible="false"></asp:Label>
							<asp:Label ID="date" runat="server" Text='<%# Eval("date", "{0:yyyy-MM-dd}") %>'></asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtdate" runat="server" Text='<%# Bind("date", "{0:yyyy-MM-dd}") %>' Width="75px"></asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateField>

					<asp:TemplateField HeaderText="類別" SortExpression="class_id">
						<ItemTemplate>
							<asp:Label ID="class" runat="server" Text='<%# Eval("cls_name") %>'></asp:Label>
						</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList ID="txtclass" runat="server">
									<asp:ListItem Text="飲食" Value="2" />
									<asp:ListItem Text="娛樂" Value="3" />
									<asp:ListItem Text="其他" Value="4" />
								</asp:DropDownList>
							</EditItemTemplate>
					</asp:TemplateField>

					<asp:TemplateField HeaderText="花費" SortExpression="cost">
						<ItemTemplate>
							<asp:Label ID="cost" runat="server" Text='<%# Eval("cost") %>'></asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtcost" runat="server" Text='<%# Bind("cost") %>' Width="40px"></asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateField>

					<asp:TemplateField HeaderText="備註" SortExpression="mark">
						<ItemTemplate>
							<asp:Label ID="mark" runat="server" Text='<%# Eval("mark") %>'></asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtmark" runat="server" Text='<%# Bind("mark") %>' Width="70px"></asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateField>

					<asp:TemplateField>
						<ItemTemplate>
							<asp:Button class="GButton" ID="btnEdit" runat="server" CommandName="Edit" ToolTip="編輯" CommandArgument='<%# Eval("date") %>' style="background-image: url('images/boo/revise.png'); background-size: 20px 20px;" />
							<asp:Button class="GButton" ID="btnDelete" runat="server" CommandName="Delete" ToolTip="刪除" CommandArgument='<%# Eval("date") %>' OnClientClick="return confirm('確定要刪除該筆資料嗎？');" style="background-image: url('images/boo/delete.png'); background-size: 20px 20px;" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:Button class="GButton" ID="btnSave" runat="server" CommandName="Update" ToolTip="確認" CommandArgument='<%# Eval("date") %>' style="background-image: url('images/boo/correct.png'); background-size: 20px 20px;" />
							<asp:Button class="GButton" ID="btnCancel" runat="server" CommandName="Cancel" ToolTip="取消" CommandArgument='<%# Eval("date") %>' style="background-image: url('images/boo/reject.png'); background-size: 20px 20px;" />
						</EditItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>

			<asp:Label ID="NoRecordsLabel" runat="server" Text="無記帳紀錄" ></asp:Label>
			</div>

			<%--<div class="BookTotal">
				<asp:Label ID="Label2" runat="server" Text="總收入__元"></asp:Label>
				<asp:Label ID="Label3" runat="server" Text="總支出__元"></asp:Label>
			</div>--%>
		</div>

		<div class="BookRight">
			<asp:ImageButton class="BookmarkUp AddBookmark" ID="ImageButton2" runat="server" ImageUrl="images/boo/boo_button_add1.png" PostBackUrl="~/src/bookkeeping_add.aspx" />
			<asp:ImageButton class="BookmarkDown SearchBookmark" ID="ImageButton3" runat="server" ImageUrl="images/boo/boo_button_ser2.png" PostBackUrl="~/src/bookkeeping_search.aspx" />
			<asp:ImageButton class="BookmarkDown ReportBookmark" ID="ImageButton4" runat="server" ImageUrl="images/boo/boo_button_rep2.png" PostBackUrl="~/src/bookkeeping_report.aspx" />
		<h1 class="BookTitle">新增</h1>
		<div class="BookChange">
			<div class="SortContainer">
				<p style="font-weight: bold;">選擇類別</p>
				<br />
				<div class="RadioButtons">
					<label class="SortRadio">
						<input type="radio" name="radio" value="1" checked="checked" />
						<span class="RadioBtn">
							<i>v</i>
							<div class="SortIcon" style="background:url('images/c_dre.png'); background-size: contain;">
								<i></i>
								<h3>願望</h3>
							</div>
						</span>
					</label>
					<label class="SortRadio">
						<input type="radio" name="radio" value="2" />
						<span class="RadioBtn">
							<i>v</i>
							<div class="SortIcon" style="background:url('images/c_eat.png'); background-size: contain;">
								<i></i>
								<h3>飲食</h3>
							</div>
						</span>
					</label>
					<label class="SortRadio">
						<input type="radio" name="radio" value="3"/>
						<span class="RadioBtn">
							<i>v</i>
							<div class="SortIcon" style="background:url('images/c_play.png'); background-size: contain;">
								<i></i>
								<h3>娛樂</h3>
							</div>
						</span>
					</label>
					<label class="SortRadio">
						<input type="radio" name="radio" value="4"/>
						<span class="RadioBtn">
							<i>v</i>
							<div class="SortIcon" style="background:url('images/c_other.png'); background-size: contain;">
								<i></i>
								<h3>其他</h3>
							</div>
						</span>
					</label>
				</div>
			</div>
			<br />
			<br />
			<div class="BooAddText">
				<asp:Label ID="Label4" runat="server" Text="日期"></asp:Label>
				<input type="date" id="Start" name="date" value="<%= DateTime.Now.ToString("yyyy-MM-dd") %>" />
				<br />
				<br />
				<asp:Label ID="Label5" runat="server" Text="金額"></asp:Label>
				<asp:TextBox class="TextBoxStyle" type="text" ID="TextBox1" runat="server"></asp:TextBox>
				<br />
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:Label ID="ErrorMessageLabel" runat="server" CssClass="ErrorMessage" Visible="false" style ="color:red"></asp:Label>
				<br />
				<asp:Label ID="Label6" runat="server" Text="備註"></asp:Label>
				<asp:TextBox class="TextBoxStyle" type="text" ID="TextBox2" runat="server"></asp:TextBox>

				<div class="AddButton">
				<input class="ButtonStyle3 ButtonSize1" type="reset" value="重新輸入" />
				<asp:Button class="ButtonStyle3 ButtonSize1" ID="Button3" runat="server" Text="確定新增" OnClick="Submit_Click" />
				</div>
			</div>
		</div>
		</div>
	</div>
		<asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/main.aspx" />
		<div id="overlay"></div>
	</form>

	<script>
        var currentDate = new Date('<%= DateTime.Now.ToString("yyyy-MM-dd") %>');
        currentDate.setDate(currentDate.getDate()); 
        var maxDate = currentDate.toISOString().split('T')[0];
		document.getElementById("Start").max = maxDate;
		var minDate = "2023-01-01";
		document.getElementById("Start").min = minDate;
    </script>
</body>
</html>