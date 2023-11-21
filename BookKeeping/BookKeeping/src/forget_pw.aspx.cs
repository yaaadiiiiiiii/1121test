using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BookKeeping.src
{
    public partial class forget_pw : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }



        protected void Comfirm_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string selectquestion = secretQuestion.SelectedValue;
                string selectanswer = secretAnswer.Text.Trim();
                string newpwd = newanswer.Text.Trim();
                string confirmPwd = TextBox1.Text.Trim();
                string account = enteraccount.Text.Trim();

                if (string.IsNullOrEmpty(newpwd) || string.IsNullOrEmpty(confirmPwd))
                {
                    // Display message if new password or confirmation password is empty
                    string script = "var overlay = document.getElementById('overlay');";
                    script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                    script += "var imageBox = document.createElement('img');";
                    script += "imageBox.src = 'images/alert_pw_n.png';";
                    script += "imageBox.className = 'custom-image2';";
                    script += "document.body.appendChild(imageBox);";
                    script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                    script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                    ClientScript.RegisterStartupScript(GetType(), "請完整填寫密碼", script, true);
                    return;
                }

                if (newpwd != confirmPwd)
                {
                    // Display message if new password and confirmation password do not match
                    string script = "var overlay = document.getElementById('overlay');";
                    script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                    script += "var imageBox = document.createElement('img');";
                    script += "imageBox.src = 'images/alert_pw_n_same.png';";
                    script += "imageBox.className = 'custom-image2';";
                    script += "document.body.appendChild(imageBox);";
                    script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                    script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                    ClientScript.RegisterStartupScript(GetType(), "確認密碼不匹配", script, true);
                    return;
                }

                string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                string selectQuery = "UPDATE `112-112502`.user SET password = @password WHERE user_id = @user_id and question1 = @question1 and answer1 = @answer1";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, conn))
                        {
                            selectCommand.Parameters.AddWithValue("@question1", selectquestion);
                            selectCommand.Parameters.AddWithValue("@answer1", selectanswer);
                            selectCommand.Parameters.AddWithValue("@password", newpwd);
                            selectCommand.Parameters.AddWithValue("@user_id", account);

                            int rowsUpdated = selectCommand.ExecuteNonQuery();
                            if (rowsUpdated > 0)
                            {
                                // Password updated successfully
                                secretAnswer.Text = "";
                                newanswer.Text = "";
                                enteraccount.Text = "";

                                string script = "var overlay = document.getElementById('overlay');";
                                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                                script += "var imageBox = document.createElement('img');";
                                script += "imageBox.src = 'images/alert_1Y.png';";
                                script += "imageBox.className = 'custom-image';";
                                script += "document.body.appendChild(imageBox);";
                                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                                script += "setTimeout(function() { imageBox.style.display = 'none'; window.location.href = '" + ResolveUrl("~/src/login.aspx") + "'; }, 2000);"; // 显示图像一段时间后跳转
                                ClientScript.RegisterStartupScript(GetType(), "修改成功", script, true);
                            }
                            else
                            {
                                string script = "var overlay = document.getElementById('overlay');";
                                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                                script += "var imageBox = document.createElement('img');";
                                script += "imageBox.src = 'images/alert_saveq_n.png';";
                                script += "imageBox.className = 'custom-image2';";
                                script += "document.body.appendChild(imageBox);";
                                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                                ClientScript.RegisterStartupScript(GetType(), "安全問題不正確", script, true);
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
        }
    }
}