using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace _BookKeeping
{
    public partial class register : System.Web.UI.Page
    {
        protected DateTime BirthDate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            Session["UserBirthDate"] = DateTime.Now;
            if (IsPostBack)
            {
                // 获取用户选择的出生日期
                if (DateTime.TryParse(Request.Form["BirthDate"], out DateTime birthdate))
                {
                    BirthDate = birthdate;
                }
            }
            if (!IsPostBack)
            {
                // 检查 Session 是否包含生日值，如果不存在，则创建一个空的 Session 变量
                if (Session["UserBirthDate"] == null)
                {
                    Session["UserBirthDate"] = DateTime.Now; // 设置为今天的日期
                }

                // 获取生日值并将其设置给日期选择器
                DateTime birthDate = (DateTime)Session["UserBirthDate"];
                BirthDate = birthDate;

                // 更新日期选择器的值
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SetBirthDate", "document.getElementById('BirthDate').value = '" + birthDate.ToString("yyyy-MM-dd") + "';", true);
            }

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string userid = RegAcc.Text;
            string nickname = RegNickname.Text;
            string gender = RadioButton1.Checked ? "男生" : "女生";
            string password = RegPwd.Text;
            string selectedQuestion1 = Question1.SelectedValue;
            string answer1 = Answer1.Text;
            string confirmPassword = ReRegPwd.Text;
            DateTime selectedDate = BirthDate.Date;
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            if (selectedDate != DateTime.MinValue)
            {
                // 将生日存储在 Session 中
                Session["UserBirthDate"] = selectedDate;
            }

            // 检查是否有字段为空
            if (string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(nickname) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(answer1))
            {
                // 有一个或多个字段为空，显示错误消息
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_n_all.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                ClientScript.RegisterStartupScript(GetType(), "請填寫所有欄位", script, true);
                return; // 阻止注册流程
            }

            //檢查是否有選擇性別
            if (!RadioButton1.Checked && !RadioButton2.Checked)
            {
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_n_all.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                ClientScript.RegisterStartupScript(GetType(), "請選擇性別", script, true);
                return;
            }

            // 检查生日是否选择
            if (selectedDate == DateTime.MinValue)
            {
                // 日期没有选择，显示错误消息
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_n_birthday.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                ClientScript.RegisterStartupScript(GetType(), "請選擇生日日期", script, true);
                return; // 阻止注册流程
            }

            // 检查帐号只包含英文和数字
            if (ContainsNonChineseCharacters(userid) || userid.Contains(" "))
            {
                // 帐号包含非英文或数字字符，显示错误消息
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_id_rule.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                ClientScript.RegisterStartupScript(GetType(), "帳號只能含英文及數字", script, true);
                return; // 阻止注册流程
            }

            if (ContainsNonChineseCharacters(password) || password.Contains(" "))
            {
                // 帐号包含非英文或数字字符，显示错误消息
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_pw_rule.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                ClientScript.RegisterStartupScript(GetType(), "密碼只能含英文及數字", script, true);
                return; // 阻止注册流程
            }

            if (ContainsNonChineseCharacters(confirmPassword) || confirmPassword.Contains(" "))
            {
                // 帐号包含非英文或数字字符，显示错误消息
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_cpw_rule.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                ClientScript.RegisterStartupScript(GetType(), "確認密碼只能含英文及數字", script, true);
                return; // 阻止注册流程
            }

            // 检查密码和确认密码是否匹配
            if (password != confirmPassword)
            {
                // 密码和确认密码不匹配，显示错误消息
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_pw_n_same.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                ClientScript.RegisterStartupScript(GetType(), "確認密碼不匹配", script, true);
                return; // 阻止注册流程
            }

            // 检查答案只包含中文
            if (!ContainsChineseCharacters(answer1))
            {
                // 答案包含非中文字符，显示错误消息
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_ans_rule.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                ClientScript.RegisterStartupScript(GetType(), "安全答案只能含中文", script, true);
                return; // 阻止注册流程
            }

            if (IsUsernameAlreadyExists(userid))
            {
                // 帐号已存在，显示错误消息
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_id_repeat.png';";
                script += "imageBox.className = 'custom-image2';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                ClientScript.RegisterStartupScript(GetType(), "此帳號名稱已存在", script, true);
                return; // 阻止注册流程
            }

            // 以上验证通过后，可以执行注册流程

            string defaultClothing = string.Empty;
            string defaultBody = string.Empty;
            string defaultPet = string.Empty;
            if (RadioButton1.Checked)
            {
                // 用户选择了男生，设置男性衣服的默认值
                defaultClothing = "/src/images/cloth/defaulthead_b.png"; // 请根据实际情况设置男性衣服的值
                defaultBody = "/src/images/cloth/defaultbody_b.png";
                defaultPet = "/src/images/cloth/defaultpet.png";
            }
            else if (RadioButton2.Checked)
            {
                // 用户选择了女生，设置女性衣服的默认值
                defaultClothing = "/src/images/cloth/defaulthead_g.png"; // 请根据实际情况设置女性衣服的值
                defaultBody = "/src/images/cloth/defaultbody_g.png";
                defaultPet = "/src/images/cloth/defaultpet.png";
            }

            string sql = "INSERT INTO `112-112502`.user (user_id, nickname, gender, password, question1, answer1, birthday, cloth, cloth2, pet) VALUES (@user_id, @nickname, @gender, @password, @question1, @answer1, @birthdate, @defaultBody, @defaultClothing, @defaultPet)";
            string petsql = "INSERT INTO `112-112502`.achievement_complete (user_id, cloth_id, a_id) VALUES (@user_id, @pet, 'DP')";
            string SQL = "INSERT INTO `112-112502`.achievement_complete (user_id, cloth_id, a_id) VALUES (@user_id, @cloth_id, 'DB')";
            string clothSQL = "INSERT INTO `112-112502`.achievement_complete (user_id, cloth_id, a_id) VALUES (@user_id, @cloth_id, 'DH')";
            int rowsaffected = 0;
            int rowsAffetedPet = 0;
            int rowsAffectedClothing = 0;
            int rowsAffectedBody = 0;
            using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userid);
                        cmd.Parameters.AddWithValue("@nickname", nickname);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@question1", selectedQuestion1);
                        cmd.Parameters.AddWithValue("@answer1", answer1);
                        cmd.Parameters.AddWithValue("@birthdate", selectedDate);
                        cmd.Parameters.AddWithValue("@defaultClothing", defaultClothing);
                        cmd.Parameters.AddWithValue("@defaultBody", defaultBody);
                        cmd.Parameters.AddWithValue("@defaultPet", defaultPet);
                        rowsaffected = cmd.ExecuteNonQuery();
                    }
                    using (MySqlCommand petcmd = new MySqlCommand(petsql, conn))
                    {
                        petcmd.Parameters.AddWithValue("@user_id", userid);
                        petcmd.Parameters.AddWithValue("@pet", defaultPet);

                        rowsAffetedPet = petcmd.ExecuteNonQuery();

                        petcmd.Parameters.Clear();
                    }
                    using (MySqlCommand sqlcmd = new MySqlCommand(SQL, conn))
                    {
                        sqlcmd.Parameters.AddWithValue("@user_id", userid);
                        sqlcmd.Parameters.AddWithValue("@cloth_id", defaultClothing); // 插入默认衣物的路径

                        rowsAffectedClothing = sqlcmd.ExecuteNonQuery();

                        sqlcmd.Parameters.Clear(); // 清除之前的参数
                    }
                    using (MySqlCommand clothcmd = new MySqlCommand(clothSQL, conn))
                    {
                        clothcmd.Parameters.AddWithValue("@user_id", userid);
                        clothcmd.Parameters.AddWithValue("@cloth_id", defaultBody); // 插入默认身体的路径

                        rowsAffectedBody = clothcmd.ExecuteNonQuery();

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
            // 彈出視窗
            if (rowsaffected > 0 && rowsAffectedBody > 0 && rowsAffectedClothing > 0 &&rowsAffetedPet>0)
            {
                RegAcc.Text = "";
                RegNickname.Text = "";
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
                RegPwd.Text = "";
                Answer1.Text = "";
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_5Y.png';";
                script += "imageBox.className = 'custom-image';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; window.location.href = '" + ResolveUrl("~/src/login.aspx") + "'; }, 2000);"; // 显示图像一段时间后跳转
                ClientScript.RegisterStartupScript(GetType(), "註冊成功", script, true);
            }
            else
            {
                string script = "var overlay = document.getElementById('overlay');";
                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                script += "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/alert_5N.png';";
                script += "imageBox.className = 'custom-image';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                ClientScript.RegisterStartupScript(GetType(), "註冊失敗", script, true);
            }
        }


        private bool IsUsernameAlreadyExists(string userid)
        {
            int count = 0;
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string sql = "SELECT COUNT(*) FROM `112-112502`.user WHERE user_id = @userid";
            using (MySqlConnection conn = new MySqlConnection(connectionStrings))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@userid", userid);

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
                
            }
            return count > 0;
        }

        private bool ContainsChineseCharacters(string input)
        {
            // 使用正則表達式檢查輸入是否包含中文字符
            string pattern = @"[\u4e00-\u9fa5]";
            return Regex.IsMatch(input, pattern);
        }

        private bool ContainsNonChineseCharacters(string input)
        {
            // 使用正则表达式检查输入是否包含非中文字符
            string pattern = @"[^\u4e00-\u9fa5]";
            return !Regex.IsMatch(input, pattern);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}