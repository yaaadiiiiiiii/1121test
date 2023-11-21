<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookkeeping_search.aspx.cs" Inherits="_BookKeeping.bookkeeping_search" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>記帳查詢</title>
</head>


<body class="BookBody">
    <form id="form1" runat="server">
        <div class="BookContent">
		<div class="BookLeft">
			<div class="BookDate">
				<%--<asp:Button class="ButtonStyle DateButtonSize" ID="Button1" runat="server" Text="<" OnClick="MinusMonth_Click" CommandArgument="minus" />
				<asp:Label ID="Label1" runat="server"></asp:Label>
				<asp:Button class="ButtonStyle DateButtonSize" ID="Button2" runat="server" Text=">" OnClick="PlusMonth_Click" CommandArgument="plus" />--%>
<%--				<asp:Button class="ButtonStyle EditButton" ID="Button4" runat="server" Text="編輯" OnClick="PlusMonth_Click" PostBackUrl="~/src/bookkeeping_edit.aspx" />--%>
			</div>

			<h1 class="BookTitleL">查詢結果</h1>
			<div class="BookTable">
				<asp:GridView class="Gridview" ID="SearchView" runat="server" AutoGenerateColumns="False" DataKeyNames="num" >
					<Columns>
						<asp:BoundField DataField="date" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"/>
						<asp:BoundField DataField="cls_name" HeaderText="類別"/>
						<asp:BoundField DataField="cost" HeaderText="金額" />
						<asp:BoundField DataField="mark" HeaderText="備註"/>
					</Columns>
				</asp:GridView>
				<asp:Label ID="NoDataLabel" runat="server" Text="查無資料" ></asp:Label>
			</div>
		</div>

        <div class="BookRight">
			<asp:ImageButton class="BookmarkDown AddBookmark" ID="ImageButton3" runat="server" ImageUrl="images/boo/boo_button_add2.png" PostBackUrl="~/src/bookkeeping_add.aspx" />
			<asp:ImageButton class="BookmarkUp SearchBookmark" ID="ImageButton4" runat="server" ImageUrl="images/boo/boo_button_ser1.png" PostBackUrl="~/src/bookkeeping_search.aspx" />
			<asp:ImageButton class="BookmarkDown ReportBookmark" ID="ImageButton5" runat="server" ImageUrl="images/boo/boo_button_rep2.png" PostBackUrl="~/src/bookkeeping_report.aspx" />
		<h1 class="BookTitle">查詢</h1>
        <div class="BookReport">
			<br />
			<br />
			<br />
			<asp:Label ID="Label2" runat="server" Text="日期"></asp:Label>
			<asp:DropDownList class="DropDownStyle" ID="YearList" runat="server" OnSelectedIndexChanged="YearList_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
			<asp:Label ID="YearLabel" runat="server" Text="年"></asp:Label>
			<asp:DropDownList class="DropDownStyle2" ID="MonthList" runat="server" OnSelectedIndexChanged="MonthList_SelectedIndexChanged" AutoPostBack="true">
				<asp:ListItem Text="*" Value="0"  />
			</asp:DropDownList>
			<asp:Label ID="MonthLabel" runat="server" Text="月" ></asp:Label>
			<asp:DropDownList class="DropDownStyle2" ID="DayList" runat="server">
				<asp:ListItem Text="*" Value="0"  />
			</asp:DropDownList>
			<asp:Label ID="DayLabel" runat="server" Text="日"></asp:Label>
			<%--<input type="date" id="Start" name="date"
			   value="2023-04-20"
			   min="2022-01-01" max="" />
			<script>
				document.getElementById("Start").value = '<%= DateTime.Now.ToString("yyyy-MM-dd") %>';
				var today = new Date().toISOString().split('T')[0];
				document.getElementById("Start").max = today;
			</script>--%>
			<br />
			<br />
			<asp:Label ID="Label3" runat="server" Text="類別"></asp:Label>
			<asp:DropDownList class="DropDownStyle" ID="CategoryList" runat="server">
				<asp:ListItem Text="*" Value="all"  />
				<asp:ListItem Text="願望" Value="願望"  />
				<asp:ListItem Text="飲食" Value="飲食" />
				<asp:ListItem Text="娛樂" Value="娛樂" />
				<asp:ListItem Text="其他" Value="其他" />
				<asp:ListItem Text="兌換願望" Value="兌換願望" />
			</asp:DropDownList>
			<asp:Label ID="Label10" runat="server" Text="關鍵字"></asp:Label>
			<asp:TextBox class="TextBoxStyle" ID="TxtBox" type="text" runat="server" placeholder="請輸入關鍵字"></asp:TextBox>
			<br />
			<br />
			<br />
			<br />
			<br />
			<asp:Button class="ButtonStyle3 ButtonSize1" ID="Button3" runat="server" Text="查詢" OnClick="Search_Click"/>
        </div>
        </div>
        </div>
		<asp:ImageButton class="Back" ID="ImageButton2" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/main.aspx" />
    </form>
</body>
</html>
