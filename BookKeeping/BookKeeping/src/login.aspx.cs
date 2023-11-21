using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web.UI;

namespace _BookKeeping
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void register_Click(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx"); // 導向到註冊頁面
        }

        protected void Login_click(object sender, EventArgs e)
        {
            string username = UserAcc.Text.Trim();
            string password = UserPwd.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                state.Text = "請輸入帳號和密碼";
                return; // Don't proceed with login
            }

            string sql = "SELECT * FROM `112-112502`.user WHERE BINARY user_id = @userid AND password = @password";
            string connection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        // 連接成功打開
                    }

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@userid", UserAcc.Text);
                        cmd.Parameters.AddWithValue("@password", UserPwd.Text);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Session["UserID"] = UserAcc.Text;
                                // 成功
                                Response.Redirect("~/src/main.aspx");
                            }
                            else
                            {
                                // error
                                state.Text = "帳號或密碼錯誤！";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 将数据库错误信息记录到日志或输出到控制台
                    Console.WriteLine($"数据库错误：{ex.Message}");
                    // 或者使用 ASP.NET 的日志记录机制记录错误
                    // Log.Error($"数据库错误：{ex.Message}", ex);

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
}
