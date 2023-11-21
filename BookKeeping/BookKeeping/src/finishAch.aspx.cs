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

namespace _BookKeeping
{
    public partial class finishAch : System.Web.UI.Page
    {
        protected string user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            user_id = Session["UserID"] as string;

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
            //將已完成的成就存放至陣列中
            List<string> finishTaskLists = new List<string>();
            int[] taskCount = { 5, 10, 20, 50 };
            int[] scoreCount = { 10, 20, 30 };

            string query = "SELECT a_id FROM `112-112502`.achievement_complete WHERE user_id = @user_id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user_id", user_id);
                        // 執行 SQL 查詢
                        using (MySqlDataReader reader = command.ExecuteReader())
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
                    // 獲取使用者性別

                    string gender = UserGender(connection);

                    // 創建任務清單數據
                    DataTable dt = new DataTable();
                    dt.Columns.Add("TaskID", typeof(int));
                    dt.Columns.Add("ImageUrl", typeof(string));
                    dt.Columns.Add("TaskName", typeof(string));
                    dt.Columns.Add("TaskDescription", typeof(string));
                    dt.Columns.Add("ProgressBarStyle", typeof(string));
                    dt.Columns.Add("ImageURLField", typeof(string));


                    // 任務1：記帳次數達5次 (finishTaskArray 包含 '1' 才新增）
                    for (int i = 1; i <= 7; i += 2)
                    {
                        if (finishTaskArray.Contains(i.ToString()))
                        {
                            int countIndex = i / 2;
                            int clothIndex = countIndex + 1;
                            int cnt = taskCount[countIndex];
                            DataRow task1 = dt.NewRow();
                            task1["TaskID"] = i.ToString();
                            task1["ImageUrl"] = ResolveUrl("~/src/images/cloth/body_" + gender + clothIndex.ToString() + ".png");
                            task1["TaskName"] = "記帳次數達" + cnt.ToString() + "次";
                            task1["ImageURLField"] = ResolveUrl("~/src/images/checked.png");
                            dt.Rows.Add(task1);
                        }
                    }


                    // 任務2：許願10次( finishTaskArray 不包含 '2' 才新增）
                    for (int j = 2; j <= 8; j += 2)
                    {
                        if (finishTaskArray.Contains(j.ToString()))
                        {
                            int countIndex = j / 2 - 1;
                            int clothIndex = countIndex + 1;
                            int cnt = taskCount[countIndex];
                            DataRow task2 = dt.NewRow();
                            task2["TaskID"] = j.ToString();
                            task2["ImageUrl"] = ResolveUrl("~/src/images/cloth/head_" + gender + clothIndex.ToString() + ".png");
                            task2["TaskName"] = "許願次數達" + cnt.ToString() + "次";
                            task2["ImageURLField"] = ResolveUrl("~/src/images/checked.png");
                            dt.Rows.Add(task2);
                        }
                    }
                    for (int k = 9; k <= 11; k += 1)
                    {
                        if (finishTaskArray.Contains(k.ToString()))
                        {
                            int countIndex = k - 9;
                            int clothIndex = countIndex + 1;
                            int cnt = scoreCount[countIndex];
                            DataRow task3 = dt.NewRow();
                            task3["TaskID"] = k.ToString();
                            task3["ImageUrl"] = ResolveUrl("~/src/images/cloth/pet_" + clothIndex.ToString() + ".png");
                            task3["TaskName"] = "小遊戲答對題數達" + cnt.ToString() + "次";
                            task3["ImageURLField"] = ResolveUrl("~/src/images/checked.png");
                            dt.Rows.Add(task3);
                        }
                    }
                    FinishRepeater.DataSource = dt;
                    FinishRepeater.DataBind();


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

       

        
    }
}