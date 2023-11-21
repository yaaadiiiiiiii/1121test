using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Security.Principal;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;

namespace BookKeeping.src
{
    public partial class main : System.Web.UI.Page
    {
        protected string user_id; // 新增成員變數
        protected string currentClothingID;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            user_id = Session["UserID"] as string; // 將 Session["UserID"] 賦值給成員變數 user_id
            PigChange();

            if (!string.IsNullOrEmpty(user_id))
            {
                // 使用用户 ID 查询数据库以获取用户数据
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                // 创建 SQL 查询
                string query = "SELECT nickname, user_id, gender, cloth, cloth2, pet FROM `112-112502`.user WHERE user_id = @UserID";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try 
                    {
                        connection.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@UserID", user_id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string currentHeadID = reader["cloth2"].ToString();
                                    MainHead.ImageUrl = currentHeadID;
                                    string currentBodyID = reader["cloth"].ToString();
                                    MainBody.ImageUrl = currentBodyID;
                                    string currentPetID = reader["pet"].ToString();
                                    MainPet.ImageUrl = currentPetID;

                                    // 从数据库中获取用户数据并填充到 Label 中
                                    NickName.Text = reader["nickname"].ToString();
                                    UId.Text = reader["user_id"].ToString();

                                    if (string.IsNullOrEmpty(NickName.Text))
                                    {
                                        NickName.Text = user_id;
                                        UId.Text = "";
                                    }
                                    else
                                    {
                                        NickName.Text = reader["nickname"].ToString();
                                        UId.Text = reader["user_id"].ToString();
                                    }

                                    if (reader["gender"].ToString() == "男生")
                                    {
                                        AvatarHead.ImageUrl = "images/avatar/ava_boy.png";
                                    }
                                    else
                                    {
                                        AvatarHead.ImageUrl = "images/avatar/ava_girl.png";
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
            }   
        }
        //抓取願望類別的總金額
        private int GetWishAmount() 
        {
            int wish_amount = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            int count;
            //判斷願望類別是否有資料
            string sql_count = "SELECT count(*) FROM `112-112502`.bookkeeping_data where user_id = @user_id and class_id = '1'";

            string sql_wish = @"
                    SELECT 
                    COALESCE((SELECT SUM(cost) FROM `112-112502`.bookkeeping_data WHERE user_id = @user_id AND class_id = '1'), 0) -
                    COALESCE((SELECT SUM(cost) FROM `112-112502`.bookkeeping_data WHERE user_id = @user_id AND class_id = '5'), 0) AS 現有存款";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd_count = new MySqlCommand(sql_count, conn))
                    {
                        cmd_count.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader reader_count = cmd_count.ExecuteReader())
                        {
                            reader_count.Read();
                            count = reader_count.GetInt32(0);
                        }
                    }
                    //抓取願望類別總金額
                    if (count > 0)
                    {
                        using (MySqlCommand cmd_wish = new MySqlCommand(sql_wish, conn))
                        {
                            cmd_wish.Parameters.AddWithValue("@user_id", user_id);
                            using (MySqlDataReader reader_wish = cmd_wish.ExecuteReader())
                            {
                                reader_wish.Read();
                                wish_amount += reader_wish.GetInt32(0);
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
                return wish_amount;
         }
        //抓取目標願望的金額
        private int GetTargetAmount()
        {
            int count;
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            int wish_amount = GetWishAmount();

            //判斷是否有目標
            int target_amount = 0;
            //判斷願望類別是否有資料
            string sql_count = "SELECT count(*) FROM `112-112502`.bucket_list where user_id = @user_id and pass_state='y' and exchange_state is null ";
            using (MySqlConnection conn = new MySqlConnection(connectionString)) 
            {
                try 
                {
                    conn.Open();
                    using (MySqlCommand cmd_count = new MySqlCommand(sql_count, conn))
                    {
                        cmd_count.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader reader_count = cmd_count.ExecuteReader())
                        {
                            reader_count.Read();
                            count = reader_count.GetInt32(0);
                        }
                    }
                    if (count > 0)
                    {
                        string sql_target = "SELECT pass_amount FROM `112-112502`.bucket_list where user_id = @user_id and pass_state='y'and exchange_state is null";
                        using (MySqlCommand cmd_target = new MySqlCommand(sql_target, conn))
                        {
                            cmd_target.Parameters.AddWithValue("@user_id", user_id);
                            using (MySqlDataReader target_reader = cmd_target.ExecuteReader())
                            {
                                List<int> wishAmountList = new List<int>();
                                while (target_reader.Read())
                                {
                                    int passAmount = target_reader.GetInt32("pass_amount");
                                    wishAmountList.Add(passAmount);
                                }

                                wishAmountList.Sort();

                                int maxnum = wishAmountList.Max();

                                foreach (int num in wishAmountList)
                                {
                                    if (wish_amount < num || num == maxnum)
                                    {
                                        target_amount = num;
                                        break;
                                    }
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
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                
                //抓取目標願望金額
                     
            }
            return target_amount;
        }

        //小豬撲滿變化
        protected void PigChange() 
        { 
            float wish_amount = GetWishAmount();
            float target_amount = GetTargetAmount();
            

            if (target_amount > 0)
            {
                double progress_percent = Math.Round((wish_amount / target_amount * 100));

                if (progress_percent >= 100)
                {
                    PigProgress.Text = "目標已經完成囉!";
                    if (progress_percent > 100)
                    {
                        Pig.ImageUrl = "images/main/pig/pig100up.png";
                    }
                    else
                    {
                        Pig.ImageUrl = "images/main/pig/pig100.png";
                    }
                }
                else 
                {
                    PigProgress.Text = "目標進度:" + wish_amount.ToString() + '/' + target_amount.ToString();

                    if (progress_percent >= 90)
                    {
                        Pig.ImageUrl = "images/main/pig/pig90.png";
                    }
                    else if (progress_percent >= 80) 
                    {
                        Pig.ImageUrl = "images/main/pig/pig80.png";
                    }
                    else if (progress_percent >= 70)
                    {
                        Pig.ImageUrl = "images/main/pig/pig70.png";
                    }
                    else if (progress_percent >= 60)
                    {
                        Pig.ImageUrl = "images/main/pig/pig60.png";
                    }
                    else if (progress_percent >= 50)
                    {
                        Pig.ImageUrl = "images/main/pig/pig50.png";
                    }
                    else if (progress_percent >= 40)
                    {
                        Pig.ImageUrl = "images/main/pig/pig40.png";
                    }
                    else if (progress_percent >= 30)
                    {
                        Pig.ImageUrl = "images/main/pig/pig30.png";
                    }
                    else if (progress_percent >= 20)
                    {
                        Pig.ImageUrl = "images/main/pig/pig20.png";
                    }
                    else if (progress_percent >= 10)
                    {
                        Pig.ImageUrl = "images/main/pig/pig10.png";
                    }
                    else 
                    {
                        Pig.ImageUrl = "images/main/pig/pig0.png";
                    }
                }
            }
            else 
            {
                Pig.ImageUrl = "images/main/pig/pig_cry.png";
                PigProgress.Text = "目前沒有願望目標喔!";
            }

        }
    }
}