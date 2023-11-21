using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text.RegularExpressions;


namespace BookKeeping
{
    public partial class bucket_password : System.Web.UI.Page
    {
        protected string user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            user_id = Session["UserID"] as string;

            if (!IsPostBack)
            {
                // 在页面加载时检查用户是否已经注册审核密码
                bool hasAuditPassword = CheckUserHasAuditPassword(user_id);

                if (hasAuditPassword)
                {
                    // 用户已经注册了审核密码，不需要进行额外操作
                    securityQuestion.Visible = false;
                    securityAnswer.Visible = false;
                    UserPwd.TextMode = TextBoxMode.Password;
                }
                else
                {
                    // 用户尚未注册审核密码，更改页面元素以显示相应的信息
                    ErrorMessageLabel.Text = "尚未註冊審核密碼，請於上方設定";
                    UserPwd.Visible = true; // 显示审核密码输入框
                    LoginButton.Text = "註冊"; // 更改按钮文本
                    question.Visible = true;
                    securityQuestion.Visible = true;
                    securityAnswer.Visible = true;

                    // 恢复 ViewState 中的值
                    UserPwd.Text = ViewState["enteredPassword"] as string;
                    securityAnswer.Text = ViewState["Answer"] as string;
                    securityQuestion.SelectedValue = ViewState["ques"] as string;
                }
            }
        }

        private bool CheckUserHasAuditPassword(string user_id)
        {
            int count = 0;
            string sql = "SELECT COUNT(*) FROM `112-112502`.user WHERE user_id = @user_id AND YNpassword IS NOT NULL";
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        count = Convert.ToInt32(cmd.ExecuteScalar());
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
               
                // 如果 count 大于 0，表示用户已经注册了审核密码
                return count > 0;
            }
        }



        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string enteredPassword = UserPwd.Text.Trim(); // 獲取用戶輸入的密碼
            bool userHasAuditPassword = CheckUserHasAuditPassword(user_id);
            string Answer = securityAnswer.Text.Trim();
            string ques = securityQuestion.SelectedValue;

            if (userHasAuditPassword)
            {
                string correctPassword = GetPasswordForUser(user_id);
                if (enteredPassword == correctPassword)
                {
                    // 密碼正確，將用戶重定向到審核介面
                    Response.Redirect("bucket_review.aspx");
                }
                else
                {
                    // 密碼不正確，顯示提醒文字
                    ErrorMessageLabel.Text = "密碼不正確，請重試。";
                }
            }
            else
            {
                // 用户尚未注册审核密码，将用户输入的密码更新到数据库
                // 添加验证逻辑，检查输入是否为空
                if (string.IsNullOrEmpty(enteredPassword) || string.IsNullOrEmpty(Answer) || string.IsNullOrEmpty(ques))
                {
                    ErrorMessageLabel.Text = "請填寫所有必要信息。"; // 显示错误消息
                    return; // 如果有空字段，不继续执行
                }

                if (ContainsChineseCharacters(enteredPassword))
                {
                    ErrorMessageLabel.Text = "密碼只能是英文或數字";
                    return;
                }

                if (!ContainsChineseCharacters(Answer))
                {
                    ErrorMessageLabel.Text = "回答答案只能是中文";
                    return;
                }

                ViewState["enteredPassword"] = enteredPassword;
                ViewState["Answer"] = Answer;
                ViewState["ques"] = ques;

                // 用户尚未注册审核密码，将用户输入的密码更新到数据库
                UpdateAuditPasswordForUser(user_id, enteredPassword, ques, Answer);

                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_5Y.png';";
                script += "imageBox.className = 'custom-image';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                script += "setTimeout(function() { imageBox.style.display = 'none'; window.location.href = '" + ResolveUrl("~/src/bucket_password.aspx") + "'; }, 2000);"; // 显示图像一段时间后跳转
                ClientScript.RegisterStartupScript(GetType(), "註冊成功", script, true);
            }
        }

        private void UpdateAuditPasswordForUser(string user_id, string newPassword, string ques, string Answer)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string sql = "UPDATE `112-112502`.user SET YNpassword = @newPassword, question2 = @question, answer2 = @answer WHERE user_id = @user_id";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Parameters.AddWithValue("@question", ques);
                        cmd.Parameters.AddWithValue("@answer", Answer);

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
               
               
            }
        }


        private string GetPasswordForUser(string user_id)
        {
            string password = null;
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string sql = "SELECT YNpassword FROM `112-112502`.user where user_id = @user_id";

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
                            if (reader.Read())
                            {
                                // 從資料庫中獲取密碼並返回
                                password=reader["YNpassword"].ToString();
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
            return password;
        }

        private bool ContainsChineseCharacters(string input)
        {
            // 使用正則表達式檢查輸入是否包含中文字符
            string pattern = @"[\u4e00-\u9fa5]";
            return Regex.IsMatch(input, pattern);
        }

    }
}