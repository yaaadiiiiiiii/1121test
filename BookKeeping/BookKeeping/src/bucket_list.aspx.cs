using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZstdSharp.Unsafe;
using System.Web.SessionState;
using System.Xml.Linq;

namespace _BookKeeping
{

    public partial class bucket_list : System.Web.UI.Page
    {
        protected string user_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            user_id = Session["UserID"] as string;
            if (!IsPostBack)
            {
                DisplayWishList();
            }
        }
        protected void DisplayWishList()
        {
            //搜尋願望
            string wishQuery = "SELECT d_num, d_name, pass_amount FROM bucket_list WHERE user_id = @user_id AND pass_state = 'y' and exchange_state is null";
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection (connectionStrings))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(wishQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        //使用Repeater 繫結資料
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            Wish_Repeater.DataSource = reader;
                            Wish_Repeater.DataBind();
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
        //檢查是否能兌換願望
        protected bool IsRedeemVisible(object passAmountObj)
        {
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            if (passAmountObj == null || passAmountObj == DBNull.Value)
            {
                return false;
            }

            int passAmount = Convert.ToInt32(passAmountObj);
            int currentUserAmount = 0;
            // 计算当前存款
            string depositQuery = @"
                    SELECT 
                    COALESCE((SELECT SUM(cost) FROM `112-112502`.bookkeeping_data WHERE user_id = @user_id AND class_id = '1'), 0) -
                    COALESCE((SELECT SUM(cost) FROM `112-112502`.bookkeeping_data WHERE user_id = @user_id AND class_id = '5'), 0) AS 現有存款";

            using (MySqlConnection conn =new MySqlConnection(connectionStrings))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand depositCommand = new MySqlCommand(depositQuery, conn))
                    {
                        depositCommand.Parameters.AddWithValue("@user_id", user_id);

                        // 执行查询并获取结果
                        currentUserAmount += Convert.ToInt32(depositCommand.ExecuteScalar());
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

            // 检查是否可以兑换
            return currentUserAmount >= passAmount;

        }

        protected void Repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int deleteRowsAffected = 0;
            int redeemRowsAffected = 0;


            string deleteQuery = "UPDATE bucket_list SET run_state = 'd' , exchange_state = 'D', exchange_time = now() WHERE d_num = @dNum";
             //獲取願望金額以及願望名稱
            string wishDataQuery = "SELECT d_name,pass_amount FROM `112-112502`.bucket_list where d_num = @dNum";
            //更新願望狀態
            string deleteYQuery = "UPDATE bucket_list SET run_state = 'r' , exchange_state = 'Y' , exchange_time = now() WHERE d_num = @dNum";
            //新增願望兌換的記帳資料
            string redeemQuery = "INSERT INTO `112-112502`.bookkeeping_data (user_id, date ,class_id, cost, mark) VALUES (@user_id, @date , '5', @wish_amount, @wish_name);";
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            if (e.CommandName == "DeleteWish")
            {
                string dNum = e.CommandArgument.ToString();

                using (MySqlConnection conn = new MySqlConnection(connectionStrings))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@dNum", dNum);
                            int rowsAffected = deleteCmd.ExecuteNonQuery();
                            if (rowsAffected > 0) // 彈出視窗
                            {
                                string script = "var overlay = document.getElementById('overlay');";
                                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                                script += "var imageBox = document.createElement('img');";
                                script += "imageBox.src = 'images/alert_3Y.png';";
                                script += "imageBox.className = 'custom-image';";
                                script += "document.body.appendChild(imageBox);";
                                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                                ClientScript.RegisterStartupScript(GetType(), "刪除成功", script, true);
                            }
                            else
                            {
                                string script = "var overlay = document.getElementById('overlay');";
                                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                                script += "var imageBox = document.createElement('img');";
                                script += "imageBox.src = 'images/alert_3N.png';";
                                script += "imageBox.className = 'custom-image';";
                                script += "document.body.appendChild(imageBox);";
                                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                                ClientScript.RegisterStartupScript(GetType(), "刪除失敗", script, true);
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
                   
                    DisplayWishList();         
                }
            }
            else if (e.CommandName == "RedeemWish")
            {
                // 獲取要兌換的願望的 d_num
                string dNum = e.CommandArgument.ToString();
                string wishName = "";
                int wishAmount = 0;

                using (MySqlConnection conn = new MySqlConnection(connectionStrings))
                {
                    try
                    {
                        conn.Open();

                        using (MySqlCommand deleteCmd = new MySqlCommand(deleteYQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@dNum", dNum);
                            deleteRowsAffected = deleteCmd.ExecuteNonQuery();
                        }

                        using (MySqlCommand wishAmountCmd = new MySqlCommand(wishDataQuery, conn))
                        {
                            wishAmountCmd.Parameters.AddWithValue("dNum", dNum);
                            using (MySqlDataReader reader = wishAmountCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // 读取数据库中的值并存储在相应的变量中
                                    wishName = reader["d_name"].ToString();
                                    wishAmount += Convert.ToInt32(reader["pass_amount"]);
                                }
                            }
                        }

                        using (MySqlCommand redeemCmd = new MySqlCommand(redeemQuery, conn))
                        {
                            redeemCmd.Parameters.AddWithValue("@user_id", user_id);
                            redeemCmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                            redeemCmd.Parameters.AddWithValue("@wish_amount", wishAmount);
                            redeemCmd.Parameters.AddWithValue("@wish_name", wishName);

                            redeemRowsAffected = redeemCmd.ExecuteNonQuery();
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
                    int rowsAffected = deleteRowsAffected + redeemRowsAffected;
                    DisplayWishList();

                    if (rowsAffected > 1) // 彈出視窗
                    {
                        string script = "var overlay = document.getElementById('overlay');";
                        script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                        script += "var imageBox = document.createElement('img');";
                        script += "imageBox.src = 'images/alert_6Y.png';";
                        script += "imageBox.className = 'custom-image';";
                        script += "document.body.appendChild(imageBox);";
                        script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                        script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                        ClientScript.RegisterStartupScript(GetType(), "兌換成功", script, true);
                    }
                    else
                    {
                        string script = "var overlay = document.getElementById('overlay');";
                        script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                        script += "var imageBox = document.createElement('img');";
                        script += "imageBox.src = 'images/alert_6N.png';";
                        script += "imageBox.className = 'custom-image';";
                        script += "document.body.appendChild(imageBox);";
                        script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                        script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                        ClientScript.RegisterStartupScript(GetType(), "兌換失敗", script, true);
                    }
                }
            }
        }







    }
}
