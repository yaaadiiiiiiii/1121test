using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.SessionState;

namespace _BookKeeping
{
    public partial class bucket_trashcan : System.Web.UI.Page
    {
        protected int currentRowIndex = 0; // 用于跟踪当前数据的索引
        protected int totalRows = 0; // 用于跟踪查询结果的总行数
        protected string user_id;
        protected string currentWishName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            user_id = Session["UserID"] as string;
            if (!IsPostBack)
            {
                
                ShowCannotBuyReason();
            }
        }

        protected void ShowCannotBuyReason()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string sql = "SELECT d_name, reason FROM `112-112502`.bucket_list WHERE user_id = @user_id AND exchange_state is null and pass_state = 'n'";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // 获取查询结果的总行数
                                totalRows = 0;
                                while (reader.Read())
                                {
                                    totalRows++;
                                }

                                // 更新当前数据的索引，确保它在有效范围内
                                if (currentRowIndex < 0)
                                {
                                    currentRowIndex = totalRows - 1;
                                }
                                if (currentRowIndex >= totalRows)
                                {
                                    currentRowIndex = 0;
                                }
                            }
                        }
                        // 查询当前索引的数据
                        cmd.CommandText = sql + $" LIMIT {currentRowIndex}, 1";
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                string d_name = dataReader["d_name"].ToString();
                                string reason = dataReader["reason"].ToString();
                                CantBuy.Text = "不能買：" + d_name;
                                Cause.Text = "因為" + reason;
                                wish.Text = d_name;// 存儲願望名稱
                                wish.Visible = false;
                            }

                            else
                            {
                                NoCantBuy.Text = "目前沒有被拒絕的願望喔~";
                                CantBuy.Text = "";
                                Cause.Text = "";
                            }
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

        protected void Next_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Next button clicked"); // 或者使用 Response.Write
            currentRowIndex++; // 切换到下一个数据

            // 如果 currentRowIndex 超出了总行数，将其重置为0
            if (currentRowIndex >= totalRows)
            {
                currentRowIndex = 0;
            }

            ShowCannotBuyReason();
        }


        protected void Last_Click(object sender, EventArgs e)
        {
            currentRowIndex--; // 切换到上一个数据

            // 如果 currentRowIndex 小于0，将其设置为最后一笔数据的索引
            if (currentRowIndex < 0)
            {
                currentRowIndex = totalRows - 1;
            }

            ShowCannotBuyReason();
        }

        protected void Submit1_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // 使用 UPDATE SQL 語句將 exchange_state 更新為 "R"，確保只更新符合特定條件的資料行
            string updateSql = "UPDATE `112-112502`.bucket_list SET exchange_state = 'R', exchange_time = now() WHERE d_name = @d_name and user_id = @user_id";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@d_name", wish.Text);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.ExecuteNonQuery();
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

               

                // 更新資料庫後，可以將該筆資料從當前頁面上移除或隱藏
                // 例如，您可以重新載入頁面，以確保該筆資料不再顯示在當前頁面上
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            
        }




    }
}
