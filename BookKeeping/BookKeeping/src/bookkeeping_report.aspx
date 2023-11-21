<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookkeeping_report.aspx.cs" Inherits="_BookKeeping.bookkeeping_report" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html>
<head>
	<link rel="stylesheet" type="text/css" href="styles.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<title>記帳圖表</title>
</head>

<body class="BookBody">
	<form runat="server">
	<div class="BookContent">
		<div class="BookLeft">
			<div class="BookDate">
				<asp:Button class="ButtonStyle3 DateButtonSize" ID="Button1" runat="server" Text="<" OnClick="MinusMonth_Click" CommandArgument="minus" />
				<asp:Label ID="Label1" runat="server"></asp:Label>
				<asp:Label ID="Label2" runat="server"></asp:Label>
				<asp:Button class="ButtonStyle3 DateButtonSize" ID="Button2" runat="server" Text=">" OnClick="PlusMonth_Click" CommandArgument="plus" />
			</div>

			<h1 class="BookTitleL">本月類別統計</h1>
			<div class="BookTableReport">
				<asp:GridView class="Gridview2" ID="GridView1" runat="server" AutoGenerateColumns="False">
					<Columns>
						<asp:BoundField DataField="cls_name" HeaderText="記帳類別" />
						<asp:BoundField DataField="total_cost" HeaderText="總金額" />
					</Columns>
				</asp:GridView>
			</div>
			<div class="BookTotal">
				<asp:Label ID="total_in" runat="server" Text="當月總願望: "></asp:Label>
				<asp:Label ID="amount_in" runat="server" Text=""></asp:Label>
				<br>
				<asp:Label ID="total_out" runat="server" Text="當月總支出: "></asp:Label>
				<asp:Label ID="amount_out" runat="server" Text=""></asp:Label>
			</div>
				<%--<asp:GridView class="Gridview" ID="GridView1" runat="server" AutoGenerateColumns="False">
					<Columns>
						<asp:BoundField DataField="date" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"/>
						<asp:BoundField DataField="class" HeaderText="類別"/>
						<asp:BoundField DataField="cost" HeaderText="金額" />
						<asp:BoundField DataField="mark" HeaderText="備註"/>
					</Columns>
				</asp:GridView>--%>
			

			<%--<div class="BookTotal">
				<asp:Label ID="Label2" runat="server" Text="總收入__元"></asp:Label>
				<asp:Label ID="Label3" runat="server" Text="總支出__元"></asp:Label>
			</div>--%>
		</div>

		<div class="BookRight">
			<asp:ImageButton class="BookmarkDown AddBookmark" ID="ImageButton2" runat="server" ImageUrl="images/boo/boo_button_add2.png" PostBackUrl="~/src/bookkeeping_add.aspx" />
			<asp:ImageButton class="BookmarkDown SearchBookmark" ID="ImageButton3" runat="server" ImageUrl="images/boo/boo_button_ser2.png" PostBackUrl="~/src/bookkeeping_search.aspx" />
			<asp:ImageButton class="BookmarkUp ReportBookmark" ID="ImageButton4" runat="server" ImageUrl="images/boo/boo_button_rep1.png" PostBackUrl="~/src/bookkeeping_report.aspx" />
		<h1 class="BookTitle">圖表分析</h1>
		<div class="BookReport">
			<asp:Chart ID="Chart1" runat="server" BackColor="#00000" >
				<Series>
					<asp:Series Name="Series1" ChartType="Column" Color="#006666" LabelBackColor="#ffffff" LabelForeColor="#006666">
					</asp:Series>
				</Series>
				<ChartAreas>
					<asp:ChartArea Name="ChartArea1">
					</asp:ChartArea>
				</ChartAreas>
			</asp:Chart>
		</div>
		</div>
	</div>
		<asp:ImageButton class="Back" ID="ImageButton1" runat="server" ImageUrl="images/back.png" PostBackUrl="~/src/main.aspx" />
	</form>
</body>
</html>