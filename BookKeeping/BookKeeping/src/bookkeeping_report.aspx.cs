using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Drawing;

namespace _BookKeeping
{
    public partial class bookkeeping_report : System.Web.UI.Page
    {
        protected string user_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            user_id = Session["UserID"] as string;

            if (!IsPostBack)
            {
                string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                int currentMonth = DateTime.Now.Month;
                // 设置 Label1 文本
                Label1.Text = DateTime.Now.Year.ToString() + "年" + currentMonth.ToString() + "月";
                using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
                {
                    try
                    {
                        conn.Open();

                        // 将 GetMonthlyCategoryTotals 的逻辑整合到 Page_Load
                        DataTable dt = GetMonthlyCategoryTotals(conn, currentMonth, DateTime.Now.Year);
                        // 生成 Chart
                        GenerateChart(conn, currentMonth);
                        // 绑定 GridView 控件
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
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
            }
        }

        protected void SearchSelectMonth(MySqlConnection connection, int month)
        {
            // 如果有 Session 變數 "currentMonth"，則使用它作為目前顯示的月份
            if (Session["currentMonth"] != null)
            {
                month = Convert.ToInt32(Session["currentMonth"]);
            }
            else
            {
                // 如果 Session 變數 "currentMonth" 不存在，將當前月份存入 Session 變數中
                Session["currentMonth"] = month;
            }

            // 將月份限制在有效範圍內
            int currentMonth = Math.Max(1, Math.Min(DateTime.Now.Month, month));

            Label1.Text = DateTime.Now.Year.ToString() + "年" + currentMonth.ToString() + "月";
            string sql = "SELECT num, date, b.cls_name, cost, mark FROM `112-112502`.bookkeeping_data as a\r\njoin `112-112502`.bookkeeping_class as b on a.class_id = b.cls_id \r\n where user_id = @user_id and year(date) = @year and month(date) = @month order by date;";
            using (MySqlCommand cmd = new MySqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue("@year", DateTime.Now.Year);
                cmd.Parameters.AddWithValue("@month", currentMonth);
                cmd.Parameters.AddWithValue("@user_id", user_id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    // 將資料繫結到 GridView 控制項上
                    GridView1.DataSource = reader;
                    GridView1.DataBind();
                }

            }
        }

        protected void GenerateChart(MySqlConnection connection, int month)
        {
            string year = DateTime.Now.Year.ToString();

            Label1.Text = year + "年" + month.ToString() + "月";

            string sql = "SELECT sum(cost) FROM `112-112502`.bookkeeping_data as a\r\njoin `112-112502`.bookkeeping_class as b on a.class_id = b.cls_id  WHERE user_id=@user_id AND b.cls_name=@class AND YEAR(date)=@year AND MONTH(date)=@month";

            // 定義五個類別
            string[] categories = { "願望", "飲食", "娛樂", "其他", "兌換願望" };
            Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
           
            bool dataExists = false; // 用于标记是否存在数据

            foreach (string category in categories)
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, connection)) 
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.Parameters.AddWithValue("@class", category);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@month", month);

                    object result = cmd.ExecuteScalar();
                    if (result != null && !Convert.IsDBNull(result))
                    {
                        int cost = Convert.ToInt32(result);
                        Chart1.Series["Series1"].Points.AddXY(category, cost);
                        dataExists = true;

                    }
                }
               

                // 如果没有数据存在，隐藏图表
                Chart1.Visible = dataExists;

            }
            if (dataExists) 
            {
                foreach (DataPoint point in Chart1.Series["Series1"].Points)
                {
                    double percentage = (point.YValues[0] / Chart1.Series["Series1"].Points.Sum(p => p.YValues[0])) * 100;
                    point.Label = string.Format("{1:F2}%", point.AxisLabel, percentage);
                }
            }
           




        }
        protected void PlusMonth_Click(object sender, EventArgs e)
        {
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // 獲取當前日期
            DateTime currentDate = DateTime.ParseExact(Label1.Text, "yyyy年M月", System.Globalization.CultureInfo.InvariantCulture);

            // 增加一個月
            DateTime newDate = currentDate.AddMonths(1);

            // 更新 Label 控件的文本，使用新的日期提取月份
            Label1.Text = newDate.ToString("yyyy年M月");

            using (MySqlConnection conn = new MySqlConnection(connectionStrings))
            {
                try
                {
                    conn.Open();
                    UpdateGridView(conn, newDate.Month, newDate.Year);
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
        }

        protected void MinusMonth_Click(object sender, EventArgs e)
        {
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // 獲取當前日期
            DateTime currentDate = DateTime.ParseExact(Label1.Text, "yyyy年M月", System.Globalization.CultureInfo.InvariantCulture);

            // 減少一個月
            DateTime newDate = currentDate.AddMonths(-1);

            // 更新 Label 控件的文本，使用新的日期提取月份
            Label1.Text = newDate.ToString("yyyy年M月");

            using (MySqlConnection conn = new MySqlConnection(connectionStrings))
            {
                try
                {
                    conn.Open();
                    UpdateGridView(conn, newDate.Month, newDate.Year);
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
        }

        private void UpdateGridView(MySqlConnection connection, int month, int year)
        {
            // 使用新的月份和年份來綁定 GridView
            DataTable dt = GetMonthlyCategoryTotals(connection, month, year);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            // 重新生成圓餅圖
            GenerateChart(connection, month);
        }

        protected DataTable GetMonthlyCategoryTotals(MySqlConnection connection, int month, int year)
        {
            DataTable dt = new DataTable();

            string query = "SELECT b.cls_name, SUM(cost) AS total_cost " +
                           "FROM `112-112502`.bookkeeping_data as a\r\n" +
                           "join `112-112502`.bookkeeping_class as b on a.class_id = b.cls_id " +
                           "WHERE MONTH(date) = @month AND YEAR(date) = @year AND user_id = @user_id " +
                           "GROUP BY b.cls_name";


            using (MySqlCommand cmd = new MySqlCommand(query, connection)) 
            {
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@user_id", user_id);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) 
                {
                    adapter.Fill(dt);
                }
            }
            // count in and out amount
            decimal totalIncome = 0;
            decimal totalExpenses = 0;

            foreach (DataRow row in dt.Rows)
            {
                string category = row["cls_name"].ToString();
                decimal totalCost = Convert.ToDecimal(row["total_cost"]);

                if (category == "願望")
                {
                    totalIncome = totalCost;
                }
                else
                {
                    totalExpenses += totalCost;
                }
            }

            // 更新Label
            amount_in.Text = ((int)totalIncome).ToString();
            amount_out.Text = ((int)totalExpenses).ToString();

            return dt;
        }

      
    }
}