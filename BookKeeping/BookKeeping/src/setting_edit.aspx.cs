using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace BookKeeping.src
{
    public partial class setting_edit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 判断是否是首次加载页面，避免在 postback 时重新加载数据

                // 获取用户的原始数据
                string userId = Session["UserID"] as string;
                UserData userData = GetUserData(userId); // 假设 GetUserData 方法返回用户数据对象

                // 将数据填充到 TextBox 控件
                if (userData != null)
                {
                    SetNickname.Text = userData.Nickname;
                }
            }
        }

        public UserData GetUserData(string userId)
        {
            UserData user = new UserData();

            // 从配置文件中获取数据库连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // 创建 SQL 查询，根据用户ID检索用户数据
            string query = "SELECT nickname, gender, user_id, birthday FROM `112-112502`.user WHERE user_id = @UserId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user.Nickname = reader["nickname"].ToString();
                                gender.Text = reader["gender"].ToString();
                                account.Text = reader["user_id"].ToString();
                                user.Birthdate = Convert.ToDateTime(reader["birthday"]);

                                Birthday.Text = user.Birthdate.ToString("yyyy/MM/dd");

                                if (reader["gender"].ToString() == "男生")
                                {
                                    Avatar.ImageUrl = "images/avatar/ava_boy.png";
                                }
                                else
                                {
                                    Avatar.ImageUrl = "images/avatar/ava_girl.png";
                                }
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
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
               
            }

            return user;
        }

        public class UserData
        {
            public string Nickname { get; set; }
            public string Gender { get; set; }
            public string Account { get; set; }
            public DateTime Birthdate { get; set; }
        }

        protected void EditOk_Click(object sender, EventArgs e)
        {
            // 获取用户输入的数据
            UserData modifiedUserData = new UserData
            {
                Nickname = SetNickname.Text
            };

            // 调用保存数据的方法
            bool updateSuccess = UpdateUserData(modifiedUserData);

            if (updateSuccess)
            {
                // 如果更新成功，你可以进行相应的处理，例如重定向到设置页面或显示成功消息
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_1Y.png';";
                script += "imageBox.className = 'custom-image';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; window.location.href = '" + ResolveUrl("~/src/setting.aspx") + "'; }, 2000);"; // 显示图像一段时间后跳转
                ClientScript.RegisterStartupScript(GetType(), "修改成功", script, true);
            }
            else
            {
                // 如果更新失败，显示错误消息给用户
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
            
        }

        public bool UpdateUserData(UserData modifiedUserData)
        {
            int rowsAffected = 0;
           // 从配置文件中获取数据库连接字符串
           string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // 创建 SQL 命令，更新用户数据
            string query = "UPDATE `112-112502`.user " +
                           "SET nickname = @Nickname " +
                           "WHERE user_id = @Account";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Nickname", modifiedUserData.Nickname);
                        cmd.Parameters.AddWithValue("@Account", account.Text);

                        rowsAffected = cmd.ExecuteNonQuery();
                        
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
            return rowsAffected > 0; // 返回是否更新成功
        }



        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}