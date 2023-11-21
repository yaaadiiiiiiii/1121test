using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _BookKeeping
{
    public partial class bookkeeping_search : System.Web.UI.Page
    {
        protected string user_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            user_id = Session["UserID"] as string;

            if (!IsPostBack)
            {
                //填充年份、月份下拉式選單內容
                int currentYear = DateTime.Now.Year;
                for (int y = currentYear; y >= currentYear - 5; y--)
                {
                    YearList.Items.Add(new ListItem(y.ToString(), y.ToString()));
                }
                for (int m = 1; m <= 12; m++)
                {
                    MonthList.Items.Add(new ListItem(m.ToString(), m.ToString()));
                }

                // 隱藏查無資料的提示
                NoDataLabel.Visible = false;
            }
        }

        protected void SearchData(string sql, string year, string month, string day, string category, string keyword)
        {
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@month", month);
                        cmd.Parameters.AddWithValue("@day", day);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@keyword", keyword);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 將資料繫結到 GridView 控制項上
                            SearchView.DataSource = reader;
                            SearchView.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    /// 將資料庫錯誤訊息顯示在頁面上
                    string errorMessage = $"資料庫錯誤：{ex.Message}";
                    ClientScript.RegisterStartupScript(GetType(), "DatabaseError", $"alert('{errorMessage}');", true);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            if (SearchView.Rows.Count == 0)
            {
                // 若 GridView 中沒有資料，顯示查無資料的提示
                NoDataLabel.Visible = true;
            }
            else
            {
                // 若有資料，隱藏提示
                NoDataLabel.Visible = false;
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string sql = "SELECT num, date, b.cls_name, cost, mark FROM `112-112502`.bookkeeping_data as a\r\njoin `112-112502`.bookkeeping_class as b on a.class_id = b.cls_id where user_id=@user_id and year(date)=@year ";

            string year = YearList.SelectedValue;
            string month = MonthList.SelectedValue;
            string day = DayList.SelectedValue;

            string category = CategoryList.SelectedValue;

            if (month != "0")
            {
                sql += " ";
                sql += "and month(date)=@month";
            }

            if (day != "0")
            {
                sql += " ";
                sql += "and day(date)=@day";
            }

            if (category != "all")
            {
                sql += " ";
                sql += "and b.cls_name=@category ";
            }

            string keyword = "%"+TxtBox.Text.Trim()+"%";

            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " ";
                sql += " and (cost like @keyword or mark like @keyword) ";
            }
            // 清除查無資料的提示
            NoDataLabel.Visible = false;

            SearchData(sql, year, month, day, category, keyword);
        }

        protected void YearList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(YearList.SelectedValue);
            int month = Convert.ToInt32(MonthList.SelectedValue);
            int i = 0;
            bool isLeapYear = false;
            DayList.Items.Clear();
            DayList.Items.Add(new ListItem("*", 0.ToString()));

            //判斷閏年
            if (year % 4 == 0)
            {
                if (year % 100 != 0 || year % 400 == 0)
                {
                    isLeapYear = true;
                }
            }

            //判斷月份的天數
            if (month != 0) 
            {
                if (month == 2 && isLeapYear)
                {
                    i = 29;
                }
                else if (month == 2)
                {
                    i = 28;
                }
                else if (month == 4 || month == 6 || month == 9 || month == 11)
                {
                    i = 30;
                }
                else 
                {
                    i = 31;
                }
                for (int j = 1; j <= i; j++) 
                {
                    DayList.Items.Add(new ListItem(j.ToString(), j.ToString()));
                }
            }


        }

        protected void MonthList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(YearList.SelectedValue);
            int month = Convert.ToInt32(MonthList.SelectedValue);
            int i = 0;
            bool isLeapYear = false;
            DayList.Items.Clear();
            DayList.Items.Add(new ListItem("*", 0.ToString()));

            //判斷閏年
            if (year % 4 == 0)
            {
                if (year % 100 != 0 || year % 400 == 0)
                {
                    isLeapYear = true;
                }
            }

            //判斷月份的天數
            if (month != 0)
            {
                if (month == 2 && isLeapYear)
                {
                    i = 29;
                }
                else if (month == 2)
                {
                    i = 28;
                }
                else if (month == 4 || month == 6 || month == 9 || month == 11)
                {
                    i = 30;
                }
                else
                {
                    i = 31;
                }
                for (int j = 1; j <= i; j++)
                {
                    DayList.Items.Add(new ListItem(j.ToString(), j.ToString()));
                }
            }
           
        }
    }
}