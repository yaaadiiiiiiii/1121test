using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.Types;
using System.Web.SessionState;
using System.Text.RegularExpressions;

namespace _BookKeeping
{
    public partial class bookkeeping_add : System.Web.UI.Page
    {
        protected string user_id;
        protected int currentMonth;

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            user_id = Session["UserID"] as string;

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(user_id))
                {
                    // 如果使用者未登入，可以執行相應的處理，例如導向到登入頁面
                    Response.Redirect("login.aspx");
                }
                string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                currentMonth = DateTime.Now.Month; // 紀錄顯示的月份
                using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
                {
                    try
                    {
                        conn.Open();
                        
                        SearchSelectMonth(conn, currentMonth);
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
            // 使用 DataTable 來存儲數據
            DataTable dataTable = new DataTable();
            Label1.Text = DateTime.Now.Year.ToString() + "年" + currentMonth.ToString() + "月";
            string sql = "SELECT num, date, b.cls_name, cost, mark FROM `112-112502`.bookkeeping_data as a\r\njoin `112-112502`.bookkeeping_class as b on a.class_id = b.cls_id where user_id = @user_id and year(date) = @year and month(date) = @month order by date;";
            using (MySqlCommand cmd = new MySqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue("@year", DateTime.Now.Year);
                cmd.Parameters.AddWithValue("@month", currentMonth);
                cmd.Parameters.AddWithValue("@user_id", user_id);
                // 使用 MySqlDataAdapter 從數據庫中檢索數據並填充 DataTable
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }

            }

            // 設置 GridView 的數據來源為 DataTable
            GridView1.DataSource = dataTable;
            GridView1.DataBind();

            // 如果 GridView 為空，顯示 NoRecordsLabel
            NoRecordsLabel.Visible = GridView1.Rows.Count == 0;
        }

        protected void MinusMonth_Click(object sender, EventArgs e)
        {
            //邊界值
            int MinMonth = 1;
            //求現在顯示的月份
            int indexMonth = Convert.ToInt32(Label1.Text.Substring(Label1.Text.IndexOf("年") + 1, Label1.Text.Length - 6).ToString());
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
            {
                try 
                {
                    conn.Open();
                    if (indexMonth > MinMonth)
                    {
                        indexMonth -= 1;
                        currentMonth = indexMonth; // 更新顯示的月份
                        Session["currentMonth"] = currentMonth; // 更新 Session 變數
                        SearchSelectMonth(conn, indexMonth);
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

           
        }


        protected void PlusMonth_Click(object sender, EventArgs e)
        {
            //邊界值
            int MaxMonth = DateTime.Now.Month;
            //求現在顯示的月份
            int indexMonth = Convert.ToInt32(Label1.Text.Substring(Label1.Text.IndexOf("年") + 1, Label1.Text.Length - 6).ToString());
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
            {
                try
                {
                    conn.Open();
                    if (indexMonth < MaxMonth)
                    {
                        indexMonth += 1;
                        currentMonth = indexMonth; // 更新顯示的月份
                        Session["currentMonth"] = currentMonth; // 更新 Session 變數
                        SearchSelectMonth(conn, indexMonth);
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
        }


        protected void Submit_Click(object sender, EventArgs e)
        {
            // 資料庫連接
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string sql = "INSERT INTO `112-112502`.bookkeeping_data(user_id, date, class_id, cost, mark) VALUES (@name, @date, @category, @cost, @mark)";
            DateTime datetime = DateTime.Parse(Request.Form["date"]);
            DateTime date = datetime.Date;
            string category = Request.Form["radio"].ToString();
            string costInput = TextBox1.Text;
            using (MySqlConnection conn = new MySqlConnection(connectionStrings))
            {
                try
                {
                    conn.Open();

                    if (!string.IsNullOrEmpty(costInput))
                    {
                        if (int.TryParse(costInput, out int cost))
                        {
                            string mark = TextBox2.Text;

                            // 檢查備註欄位的字數
                            if (mark.Length > 8)
                            {
                                string script = "var overlay = document.getElementById('overlay');";
                                script += "overlay.style.display = 'block';";
                                script += "var imageBox = document.createElement('img');";
                                script += "imageBox.src = 'images/alert_eight_word.png';";
                                script += "imageBox.className = 'custom-image2';";
                                script += "document.body.appendChild(imageBox);";
                                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);";
                                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);";
                                ClientScript.RegisterStartupScript(GetType(), "備註欄位最多只能輸入8個字!", script, true);
                                return; // 停止執行，不執行後續的程式碼
                            }
                            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@name", user_id);
                                cmd.Parameters.AddWithValue("@category", category);
                                cmd.Parameters.AddWithValue("@date", date);
                                cmd.Parameters.AddWithValue("@cost", cost);
                                cmd.Parameters.AddWithValue("@mark", mark);
                                int rows_affect = cmd.ExecuteNonQuery();
                                if (rows_affect > 0)
                                {
                                    TextBox1.Text = null;
                                    TextBox2.Text = null;
                                    ErrorMessageLabel.Text = "";
                                    string script = "var overlay = document.getElementById('overlay');";
                                    script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                                    script += "var imageBox = document.createElement('img');";
                                    script += "imageBox.src = 'images/alert_2Y.png';";
                                    script += "imageBox.className = 'custom-image';";
                                    script += "document.body.appendChild(imageBox);";
                                    script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                                    script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                                    ClientScript.RegisterStartupScript(GetType(), "新增成功", script, true);

                                    SearchSelectMonth(conn, date.Month);
                                }
                                else
                                {
                                    string script = "var overlay = document.getElementById('overlay');";
                                    script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                                    script += "var imageBox = document.createElement('img');";
                                    script += "imageBox.src = 'images/alert_2N.png';";
                                    script += "imageBox.className = 'custom-image';";
                                    script += "document.body.appendChild(imageBox);";
                                    script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                                    script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                                    ClientScript.RegisterStartupScript(GetType(), "新增失敗", script, true);
                                }
                            }
                        }
                        else
                        {
                            ErrorMessageLabel.Visible = true;
                            ErrorMessageLabel.Text = "請輸入有效的數字!";
                        }
                    }
                    else
                    {
                        ErrorMessageLabel.Visible = true;
                        ErrorMessageLabel.Text = "請輸入數字!";
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
        }




        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 取得要刪除的資料的主鍵值
            string num = GridView1.DataKeys[e.RowIndex]["num"].ToString();

            // 執行刪除資料的操作
            DeleteRecord(num);

            // 取得目前顯示的月份
            int currentMonth = Convert.ToInt32(Session["currentMonth"]);

            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // 重新載入該月份的資料
            using (MySqlConnection conn = new MySqlConnection(connectionStrings))
            {
                try
                {
                    conn.Open();
                    SearchSelectMonth(conn, currentMonth);
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




        protected void DeleteRecord(string num)
        {
            string sql = "DELETE FROM `112-112502`.bookkeeping_data WHERE num = @num";

            // 資料庫連接
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@num", num);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            string script = "var overlay = document.getElementById('overlay');";
                            script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                            script += "var imageBox = document.createElement('img');";
                            script += "imageBox.src = 'images/alert_3Y.png';";
                            script += "imageBox.className = 'custom-image';";
                            script += "document.body.appendChild(imageBox);";
                            script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                            script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                            ClientScript.RegisterStartupScript(GetType(), "刪除成功", script, true);
                        }
                        else
                        {
                            string script = "var overlay = document.getElementById('overlay');";
                            script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                            script += "var imageBox = document.createElement('img');";
                            script += "imageBox.src = 'images/alert_3N.png';";
                            script += "imageBox.className = 'custom-image';";
                            script += "document.body.appendChild(imageBox);";
                            script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                            script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                            ClientScript.RegisterStartupScript(GetType(), "刪除失敗", script, true);
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
        }


        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
           
            GridView1.EditIndex = e.NewEditIndex;
            int indexMonth = Convert.ToInt32(Label1.Text[Label1.Text.IndexOf("年") + 1].ToString());
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
            {
                try
                {
                    conn.Open();
                    SearchSelectMonth(conn, indexMonth);
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


        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            int indexMonth = Convert.ToInt32(Label1.Text[Label1.Text.IndexOf("年") + 1].ToString());
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionStrings))
            {
                try
                {
                    conn.Open();

                    SearchSelectMonth(conn, indexMonth);
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

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = GridView1.Rows[e.RowIndex];

            // 取得要編輯的資料的主鍵值
            string num = GridView1.DataKeys[e.RowIndex]["num"].ToString();

            // 取得編輯後的資料
            string dateStr = ((TextBox)row.FindControl("txtdate")).Text;
            string category = ((DropDownList)row.FindControl("txtclass")).SelectedValue;
            string costStr = ((TextBox)row.FindControl("txtcost")).Text;
            string mark = ((TextBox)row.FindControl("txtmark")).Text;

            if (!IsNumeric(costStr))
            {
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';";
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_money_rule.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);";
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);";
                ClientScript.RegisterStartupScript(GetType(), "金額欄位只能是數字", script, true);
                return; // 停止執行，不執行後續的程式碼
            }

            // 檢查備註欄位的字數
            if (mark.Length > 8)
            {
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';";
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_eight_word.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);";
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);";
                ClientScript.RegisterStartupScript(GetType(), "備註欄位最多只能輸入8個字!", script, true);
                return; // 停止執行，不執行後續的程式碼
            }

            // 將字串轉換為日期型別
            DateTime date = DateTime.Parse(dateStr);

            // 將字串轉換為整數型別
            int cost = int.Parse(costStr);

            // 更新資料庫中的資料
            string sql = "UPDATE `112-112502`.bookkeeping_data SET date = @date, class_id = @category, cost = @cost, mark = @mark WHERE user_id = @user_id AND num = @num";
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@cost", cost);
                        cmd.Parameters.AddWithValue("@mark", mark);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Parameters.AddWithValue("@num", num);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            string script = "var overlay = document.getElementById('overlay');";
                            script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                            script += "var imageBox = document.createElement('img');";
                            script += "imageBox.src = 'images/alert_1Y.png';";
                            script += "imageBox.className = 'custom-image';";
                            script += "document.body.appendChild(imageBox);";
                            script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                            script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                            ClientScript.RegisterStartupScript(GetType(), "修改成功", script, true);
                        }
                        else
                        {
                            string script = "var overlay = document.getElementById('overlay');";
                            script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                            script += "var imageBox = document.createElement('img');";
                            script += "imageBox.src = 'images/alert_1N.png';";
                            script += "imageBox.className = 'custom-image';";
                            script += "document.body.appendChild(imageBox);";
                            script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                            script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                            ClientScript.RegisterStartupScript(GetType(), "修改失敗", script, true);
                        }
                        GridView1.EditIndex = -1;

                        // 取得目前顯示的月份
                        int currentMonth = Convert.ToInt32(Session["currentMonth"]);

                        // 重新載入該月份的資料
                        SearchSelectMonth(conn, currentMonth);
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
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // 获取当前行数据
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string category = rowView["cls_name"].ToString(); // 这里假设你的类别信息在 "class" 列中

                // 找到编辑和删除按钮的控件
                Button btnEdit = (Button)e.Row.FindControl("btnEdit");
                Button btnDelete = (Button)e.Row.FindControl("btnDelete");

                // 如果類別是"願望"或"兌換願望"，則隱藏編輯和刪除按鈕
                if (category == "願望" || category == "兌換願望")
                {
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            // 獲取排序的方向
            string direction = GetSortDirection(e.SortExpression);

            // 設置新的排序方向
            e.SortDirection = direction == "ASC" ? SortDirection.Descending : SortDirection.Ascending;

            // 獲取排序欄位
            string sortExpression = e.SortExpression;

            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // 構建 SQL 查詢
            string query = "SELECT a.num, a.date, b.cls_name, a.cost, a.mark FROM `112-112502`.bookkeeping_data as a\r\njoin `112-112502`.bookkeeping_class as b on a.class_id = b.cls_id WHERE a.user_id = @user_id and month(date) = @month and year(date) = @year ORDER BY " + sortExpression + " " + direction;

            // 使用 DBConnection 方法建立 MySQL 連接
            using (MySqlConnection connection = new MySqlConnection(connectionStrings))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user_id", user_id);
                        command.Parameters.AddWithValue("@sortExpression", sortExpression);
                        command.Parameters.AddWithValue("@direction", direction);
                        command.Parameters.AddWithValue("@month", Session["currentMonth"]);
                        command.Parameters.AddWithValue("@year", DateTime.Now.Year.ToString());

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            GridView1.DataSource = dataTable;
                            GridView1.DataBind();
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
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                
            }
        }

        // 獲取排序的方向
        private string GetSortDirection(string column)
        {
            // 默認為升序
            string sortDirection = "ASC";

            // 如果排序列和當前排序列相同，則切換排序方向
            if (ViewState["SortExpression"] != null && ViewState["SortExpression"].ToString() == column)
            {
                sortDirection = (ViewState["SortDirection"].ToString() == "ASC") ? "DESC" : "ASC";
            }

            // 保存當前排序列和方向到 ViewState
            ViewState["SortExpression"] = column;
            ViewState["SortDirection"] = sortDirection;

            return sortDirection;
        }

        private bool IsNumeric(string input)
        {
            // 使用正則表達式檢查輸入是否為數字
            string pattern = @"^\d+$";
            return Regex.IsMatch(input, pattern);
        }

    }
}