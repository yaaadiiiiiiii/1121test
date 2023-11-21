using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web.SessionState;
using MySqlX.XDevAPI;
using System.Threading.Tasks;

namespace _BookKeeping
{
    public partial class achievement : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                BindTaskList();
                
            }
        }
        private string UserGender(MySqlConnection connection) 
        {
            string gender = "";
            string query = "SELECT gender FROM `112-112502`.user WHERE user_id = @user_id";
            string user_id = Session["UserID"].ToString();
           

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@user_id", user_id);
                string cmdGender = command.ExecuteScalar().ToString();
                if (cmdGender == "男生")
                {
                    gender += "b";
                }
                else
                {
                    gender += "g";
                }

            }
            
           

            return gender;
        }

        private void BindTaskList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string user_id = Session["UserID"].ToString();
            string query = "SELECT a_id FROM `112-112502`.achievement_complete WHERE user_id = @user_id";

            //將已完成的成就存放至陣列中
            List<string> finishTaskLists = new List<string>();
            int[] taskCount = { 5, 10, 20, 50 };
            int[] scoreCount = { 10, 20, 30 };

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string finishTaskList = reader.GetString("a_id");
                                finishTaskLists.Add(finishTaskList);
                            }

                        }
                    }
                    //將清單轉換成陣列
                    string[] finishTaskArray = finishTaskLists.ToArray();




                    // 獲取記帳次數、許願次數和使用者性別
                    int accountingCount = GetAccountingCount(connection);
                    int wishingCount = GetWishingCount(connection);
                    int gameCount = GetGameCount(connection);
                    string gender = UserGender(connection);

                    // 創建任務清單數據
                    DataTable dt = new DataTable();
                    dt.Columns.Add("TaskID", typeof(int));
                    dt.Columns.Add("ImageUrl", typeof(string));
                    dt.Columns.Add("TaskName", typeof(string));
                    dt.Columns.Add("TaskDescription", typeof(string));
                    dt.Columns.Add("ProgressBarStyle", typeof(string));
                    dt.Columns.Add("IsTaskCompleted", typeof(bool));


                    // 任務1：記帳次數達5次 (finishTaskArray 不包含 '1' 才新增）
                    for (int i = 1; i <= 7; i += 2)
                    {
                        if (!finishTaskArray.Contains(i.ToString()))
                        {
                            int countIndex = i / 2;
                            int clothIndex = countIndex + 1; // 更改为从2号衣服开始
                            int cnt = taskCount[countIndex];
                            DataRow task1 = dt.NewRow();
                            task1["TaskID"] = i.ToString();
                            task1["ImageUrl"] = ResolveUrl("~/src/images/cloth/body_" + gender + clothIndex.ToString() + ".png");
                            task1["TaskName"] = "記帳次數達" + cnt.ToString() + "次";
                            task1["TaskDescription"] = $"您已記帳 {accountingCount} 次";
                            task1["ProgressBarStyle"] = $"width: {(accountingCount >= cnt ? 100 : (accountingCount * 100 / cnt))}%";
                            task1["IsTaskCompleted"] = (accountingCount >= cnt);
                            dt.Rows.Add(task1);

                            break;
                        }
                    }


                    // 任務2：許願10次( finishTaskArray 不包含 '2' 才新增）
                    for (int j = 2; j <= 8; j += 2)
                    {
                        if (!finishTaskArray.Contains(j.ToString()))
                        {
                            int countIndex = j / 2 - 1;
                            int clothIndex = countIndex + 1;
                            int cnt = taskCount[countIndex];
                            DataRow task2 = dt.NewRow();
                            task2["TaskID"] = j.ToString();
                            task2["ImageUrl"] = ResolveUrl("~/src/images/cloth/head_" + gender + clothIndex.ToString() + ".png");
                            task2["TaskName"] = "許願次數達" + cnt.ToString() + "次";
                            task2["TaskDescription"] = $"您已許願 {wishingCount} 次";
                            task2["ProgressBarStyle"] = $"width: {(wishingCount >= cnt ? 100 : (wishingCount * 100 / cnt))}%";
                            task2["IsTaskCompleted"] = (wishingCount >= cnt);
                            dt.Rows.Add(task2);
                            break;
                        }
                    }
                    for (int k = 9; k <= 11; k += 1)
                    {
                        if (!finishTaskArray.Contains(k.ToString()))
                        {
                            int countIndex = k - 9;
                            int clothIndex = countIndex + 1;
                            int cnt = scoreCount[countIndex];
                            DataRow task3 = dt.NewRow();
                            task3["TaskID"] = k.ToString();
                            task3["ImageUrl"] = ResolveUrl("~/src/images/cloth/pet_" + clothIndex.ToString() + ".png");
                            task3["TaskName"] = "小遊戲答對題數達" + cnt.ToString() + "次";
                            task3["TaskDescription"] = $"您已答對 {gameCount} 次";
                            task3["ProgressBarStyle"] = $"width: {(gameCount >= cnt ? 100 : (gameCount * 100 / cnt))}%";
                            task3["IsTaskCompleted"] = (gameCount >= cnt);
                            dt.Rows.Add(task3);
                            break;
                        }
                    }


                    TaskRepeater.DataSource = dt;
                    TaskRepeater.DataBind();

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

        private int GetAccountingCount(MySqlConnection connection)
        {
            // 執行 SQL 查詢以獲取記帳次數
            string query = "SELECT COUNT(*) FROM `112-112502`.bookkeeping_data WHERE user_id = @user_id";
            string user_id = Session["UserID"].ToString();

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@user_id", user_id);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private int GetWishingCount(MySqlConnection connection)
        {
            // 執行 SQL 查詢以獲取許願次數
            string query = "SELECT COUNT(*) FROM `112-112502`.bucket_list WHERE user_id = @user_id";
            string user_id = Session["UserID"].ToString();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@user_id", user_id);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private int GetGameCount(MySqlConnection connection)
        {
            // 執行 SQL 查詢以獲取許願次數
            string query = "SELECT COALESCE(SUM(score), 0)  FROM `112-112502`.gamedata WHERE user_id = @user_id";
            string user_id = Session["UserID"].ToString();
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@user_id", user_id);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        protected void ClaimButton_Click(object sender, EventArgs e)
        {
            int rowsAffected = 0;



            // 验证是否成功获取了用户ID
            if (Session["UserID"] != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                string sql_ach = "INSERT INTO `112-112502`.achievement_complete (user_id, a_id, cloth_id) VALUES (@user_id, @task_id, @cloth_id)";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        Button claimButton = (Button)sender;
                        string taskId = claimButton.CommandArgument.ToString();
                        string gender = UserGender(connection);

                        // 使用 Session 中的用户ID
                        string user_id = Session["UserID"].ToString();

                        // 确保 task_id 是一个整数
                        if (int.TryParse(taskId, out int taskIdValue))
                        {
                            RepeaterItem item = (RepeaterItem)claimButton.NamingContainer;
                            Image imgTask = (Image)item.FindControl("Image1");

                            // 獲取 ImageUrl
                            string imageUrl = imgTask.ImageUrl;

                            // 基于 task_id 是否为奇数来构建 @cloth_id
                            string clothIdValue = imageUrl;



                            using (MySqlCommand cmd = new MySqlCommand(sql_ach, connection))
                            {
                                cmd.Parameters.AddWithValue("@user_id", user_id);
                                cmd.Parameters.AddWithValue("@task_id", taskId);
                                cmd.Parameters.AddWithValue("@cloth_id", clothIdValue);

                                rowsAffected = cmd.ExecuteNonQuery();
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
                    ShowResultMessage(rowsAffected);
                }

                // 重新绑定任务清单以更新任务状态
                BindTaskList();
            }
        }

        private void ShowResultMessage(int rowsAffected)
        {
            string script = "var overlay = document.getElementById('overlay');";
            script += "overlay.style.display = 'block';"; // 顯示背景遮罩
            script += "var imageBox = document.createElement('img');";
            script += rowsAffected > 0 ? "imageBox.src = 'images/alert_4Y.png';" : "imageBox.src = 'images/alert_4N.png';";
            script += "imageBox.className = 'custom-image';";
            script += "document.body.appendChild(imageBox);";
            script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
            script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
            ClientScript.RegisterStartupScript(GetType(), rowsAffected > 0 ? "領取成功" : "領取失敗", script, true);
        }
    }
}