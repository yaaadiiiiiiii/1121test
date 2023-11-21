using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Linq;
using System.Runtime.InteropServices;
using MySqlX.XDevAPI.Relational;
using System.Web.UI.WebControls;
using System.Web;


namespace BookKeeping.src
{
    public partial class dressing_room : System.Web.UI.Page
    {
        protected string user_id;
        protected string currentHead = "";
        protected string currentBody = "";
        protected string currentPet = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            user_id = Session["UserID"] as string;

            if (!IsPostBack) // 只在初次載入頁面時執行
            {
                // 獲取資料庫連接字串
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                // 搜尋資料庫
                string query = "SELECT cloth, cloth2, gender, pet FROM `112-112502`.user WHERE user_id = @user_id";
                // 查询用户的所有衣服ID頭部
                string clothQuery = "SELECT cloth_id FROM `112-112502`.achievement_complete WHERE user_id = @user_id and cloth_id like '%body%'";
                string headQuery = "SELECT cloth_id FROM `112-112502`.achievement_complete WHERE user_id = @user_id and cloth_id like '%head%'";
                string petQuery = "SELECT cloth_id FROM `112-112502`.achievement_complete WHERE user_id = @user_id and cloth_id like '%pet%'";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@user_id", user_id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // 從資料庫抓出剛剛更新的衣服ID
                                    string currentBodyURL = reader["cloth"].ToString();
                                    string currentHeadURL = reader["cloth2"].ToString();
                                    string currentPetURL = reader["pet"].ToString();
                                    string gen = reader["gender"].ToString();

                                    // 設置 <asp:Image> 的 ImageUrl 屬性
                                    NowBody.ImageUrl = currentBodyURL;
                                    NowHead.ImageUrl = currentHeadURL;
                                    NowPet.ImageUrl = currentPetURL;
                                    gender.Text = gen;
                                }
                            }
                        }

                        using (MySqlCommand clothCmd = new MySqlCommand(clothQuery, conn))
                        {
                            clothCmd.Parameters.AddWithValue("@user_id", user_id);
                            using (MySqlDataReader clothReader = clothCmd.ExecuteReader())
                            {
                                imageRepeater.DataSource = clothReader;
                                imageRepeater.DataBind();
                            }
                        }

                        using (MySqlCommand headCmd = new MySqlCommand(headQuery, conn))
                        {
                            headCmd.Parameters.AddWithValue("@user_id", user_id);
                            using (MySqlDataReader headReader = headCmd.ExecuteReader())
                            {
                                headRepeater.DataSource = headReader;
                                headRepeater.DataBind();
                            }
                        }
                        using (MySqlCommand petCmd = new MySqlCommand(petQuery, conn))
                        {
                            petCmd.Parameters.AddWithValue("@user_id", user_id);
                            using (MySqlDataReader petReader = petCmd.ExecuteReader())
                            {
                                petRepeater.DataSource = petReader;
                                petRepeater.DataBind();
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
                   

                   
                   

                    // 查询用户的所有衣服ID套裝
                   
                    
                }
            }
            CompareClothingPaths();
        }

        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            // 獲取user選擇的新衣物ID和新頭饰ID
            string selectedClothingID = hiddenClothingID.Value;
            string selectedHeadwearID = hiddenHeadwearID.Value;
            string selectedPetID = hiddenPetID.Value;
            // 獲取用戶目前的衣物ID和頭饰ID
            string currentClothingID = NowBody.ImageUrl;
            string currentHeadwearID = NowHead.ImageUrl;
            string currentPetID = NowPet.ImageUrl;
            // 更新資料庫
            string updateQuery = "UPDATE `112-112502`.user SET cloth = @newClothingID, cloth2 = @newHeadwearID, pet = @newPetID WHERE user_id = @user_id ";

            // 獲取資料庫連接字串
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        // 根據用戶的選擇來更新或保留衣物ID和頭饰ID
                        updateCmd.Parameters.AddWithValue("@newClothingID", string.IsNullOrEmpty(selectedClothingID) ? currentClothingID : selectedClothingID);
                        updateCmd.Parameters.AddWithValue("@newHeadwearID", string.IsNullOrEmpty(selectedHeadwearID) ? currentHeadwearID : selectedHeadwearID);
                        updateCmd.Parameters.AddWithValue("@newPetID", string.IsNullOrEmpty(selectedPetID) ? currentPetID : selectedPetID);
                        updateCmd.Parameters.AddWithValue("@user_id", user_id);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // 更新成功，執行服务器端数据绑定以更新页面上的控件
                            BindUserData();
                            UpdatePage();
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
                            ClientScript.RegisterStartupScript(GetType(), "修改失败", script, true);
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

        protected (string BodyClothingURL, string HeadClothingURL, string PetURL) BindUserData()
        {

            string currentUserBodyClothingURL = "";
            string currentUserHeadClothingURL = "";
            string currentUserPetURL = "";
            // 获取数据库连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // 查询用户的衣物和头饰数据
            string userQuery = "SELECT cloth, cloth2, pet FROM `112-112502`.user WHERE user_id = @user_id";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand userCmd = new MySqlCommand(userQuery, conn))
                    {
                        userCmd.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader userReader = userCmd.ExecuteReader())
                        {
                            if (userReader.Read())
                            {
                                // 从数据库抓出用户的衣物URL和头饰URL
                                string currentClothingURL = userReader["cloth"].ToString();
                                string currentHeadURL = userReader["cloth2"].ToString();
                                string currentPetURL = userReader["pet"].ToString();

                                // 设置<asp:Image>的ImageUrl属性
                                NowBody.ImageUrl = currentClothingURL;
                                NowHead.ImageUrl = currentHeadURL;
                                NowPet.ImageUrl = currentPetURL;

                                currentUserBodyClothingURL = currentClothingURL;
                                currentUserHeadClothingURL = currentHeadURL;
                                currentUserPetURL = currentPetURL;
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
            return (currentUserBodyClothingURL, currentUserHeadClothingURL, currentUserPetURL);
        }

        protected List<string> GetUserDataBodyClothing()
        {
            List<string> clothingUrls = new List<string>();
            // 获取数据库连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // 查询用户的衣物和头饰数据
            string userQuery = "SELECT cloth, cloth2, pet FROM `112-112502`.user WHERE user_id = @user_id";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand userCmd = new MySqlCommand(userQuery, conn))
                    {
                        userCmd.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader userReader = userCmd.ExecuteReader())
                        {
                            while (userReader.Read())
                            {
                                // 从数据库中读取每行的 "cloth" 数据并添加到列表
                                string currentClothingURL = userReader["cloth_id"].ToString();
                                clothingUrls.Add(currentClothingURL);
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

            return clothingUrls;

        }

        protected List<string> GetUserDataHeadClothing()
        {
            List<string> clothingUrls = new List<string>();

            // 获取数据库连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // 查询用户的衣物数据
            string userQuery = "SELECT cloth_id FROM `112-112502`.achievement_complete WHERE user_id = @user_id and cloth_id like '%head%'";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand userCmd = new MySqlCommand(userQuery, conn))
                    {
                        userCmd.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader userReader = userCmd.ExecuteReader())
                        {
                            while (userReader.Read())
                            {
                                // 从数据库中读取每行的 "cloth" 数据并添加到列表
                                string currentClothingURL = userReader["cloth_id"].ToString();
                                clothingUrls.Add(currentClothingURL);
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

            return clothingUrls;

        }

        protected List<string> GetUserDataPet()
        {
            List<string> clothingUrls = new List<string>();
            // 查询用户的衣物数据
            string userQuery = "SELECT cloth FROM `112-112502`.achievement_complete WHERE user_id = @user_id and cloth_id like '%pet%'";

            // 获取数据库连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand userCmd = new MySqlCommand(userQuery, conn))
                    {
                        userCmd.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader userReader = userCmd.ExecuteReader())
                        {
                            while (userReader.Read())
                            {
                                // 从数据库中读取每行的 "cloth" 数据并添加到列表
                                string currentPetURL = userReader["cloth"].ToString();
                                clothingUrls.Add(currentPetURL);
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

            return clothingUrls;

        }

        protected void UpdatePage()
        {
            // 在这个函数中执行服务器端数据绑定以更新页面上的控件
            BindUserData();
            CompareClothingPaths();
        }

        protected void CompareClothingPaths()
        {
            // 获取用户当前穿着的衣物路径
            var userClothingPaths = BindUserData();

            // 遍历身体衣物的 Repeater 中的每个项
            foreach (RepeaterItem item in imageRepeater.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    // 获取当前项的 ImageButton 控件和 Label 控件
                    ImageButton imgButtonClothing = item.FindControl("image") as ImageButton;
                    Label statusLabel = item.FindControl("bodystatus") as Label;

                    // 获取与当前项相关的衣物路径
                    string clothingPath = imgButtonClothing.ImageUrl; // 从图像的 URL 获取 cloth_id

                    // 检查当前穿着的身体衣物路径是否与当前项的衣物路径匹配
                    if (string.Equals(userClothingPaths.BodyClothingURL, clothingPath, StringComparison.OrdinalIgnoreCase))
                    {
                        // 如果匹配，应用圆角背景
                        imgButtonClothing.Style["border-radius"] = "10px"; // 设置圆角半径
                        imgButtonClothing.Style["background-color"] = "#9CD9C580"; // 将背景颜色设置为蓝色
                        imgButtonClothing.Enabled = false;
                    }
                    else
                    {
                        // 如果不匹配，移除圆角背景
                        imgButtonClothing.Style.Remove("border-radius");
                        imgButtonClothing.Style.Remove("background-color");
                        imgButtonClothing.Enabled = true;
                    }
                }
            }

            if (imageRepeater.Items.Count == 1)
            {
                ImageButton imgButtonClothing = imageRepeater.Items[0].FindControl("image") as ImageButton;
                Label statusLabel = imageRepeater.Items[0].FindControl("bodystatus") as Label;
                imgButtonClothing.Enabled = false; // 禁用按钮
            }

            // 遍历头部衣物的 Repeater 中的每个项
            foreach (RepeaterItem item in headRepeater.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    // 获取当前项的 ImageButton 控件和 Label 控件
                    ImageButton imgButtonClothing = item.FindControl("headImage") as ImageButton;
                    Label statusLabel = item.FindControl("headstatus") as Label;

                    // 获取与当前项相关的衣物路径
                    string clothingPath = imgButtonClothing.ImageUrl; // 从图像的 URL 获取 cloth_id

                    // 检查当前穿着的头部衣物路径是否与当前项的衣物路径匹配
                    if (string.Equals(userClothingPaths.HeadClothingURL, clothingPath, StringComparison.OrdinalIgnoreCase))
                    {
                        // 如果匹配，应用圆角背景
                        imgButtonClothing.Style["border-radius"] = "10px"; // 设置圆角半径
                        imgButtonClothing.Style["background-color"] = "#9CD9C580"; // 将背景颜色设置为蓝色
                        imgButtonClothing.Enabled = false;
                    }
                    else
                    {
                        // 如果不匹配，移除圆角背景
                        imgButtonClothing.Style.Remove("border-radius");
                        imgButtonClothing.Style.Remove("background-color");
                        imgButtonClothing.Enabled = true;
                    }
                }
            }

            if (headRepeater.Items.Count == 1)
            {
                Label statusLabel = headRepeater.Items[0].FindControl("headstatus") as Label;
                ImageButton imgButtonClothing = headRepeater.Items[0].FindControl("headImage") as ImageButton;
                imgButtonClothing.Enabled = false;
            }

            // 遍历宠物的 Repeater 中的每个项
            foreach (RepeaterItem item in petRepeater.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    // 获取当前项的 ImageButton 控件和 Label 控件
                    ImageButton imgButtonClothing = item.FindControl("petImage") as ImageButton;
                    //Label statusLabel = item.FindControl("petstatus") as Label;

                    // 获取与当前项相关的衣物路径
                    string clothingPath = imgButtonClothing.ImageUrl; // 从图像的 URL 获取 cloth_id

                    // 检查当前穿着的宠物路径是否与当前项的衣物路径匹配
                    if (!string.IsNullOrEmpty(userClothingPaths.PetURL) &&
                        string.Equals(userClothingPaths.PetURL, clothingPath, StringComparison.OrdinalIgnoreCase))
                    {
                        // 如果匹配，应用圆角背景
                        imgButtonClothing.Style["border-radius"] = "10px"; // 设置圆角半径
                        imgButtonClothing.Style["background-color"] = "#9CD9C580"; // 将背景颜色设置为蓝色
                        imgButtonClothing.Enabled = false;
                    }
                    else
                    {
                        // 如果不匹配，移除圆角背景
                        imgButtonClothing.Style.Remove("border-radius");
                        imgButtonClothing.Style.Remove("background-color");
                        imgButtonClothing.Enabled = true;
                    }
                }
            }

            if (petRepeater.Items.Count == 1)
            {
                //Label statusLabel = petRepeater.Items[0].FindControl("petstatus") as Label;
                ImageButton imgButtonClothing = petRepeater.Items[0].FindControl("petImage") as ImageButton;
                //statusLabel.Visible = true;
                imgButtonClothing.Enabled = false;
            }
        }
    }
}