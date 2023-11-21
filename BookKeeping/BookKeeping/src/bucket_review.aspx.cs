using _BookKeeping;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.Security;
using Google.Protobuf.WellKnownTypes;
using System.Web.SessionState;

namespace BookKeeping.src
{
    public partial class bucket_review : System.Web.UI.Page
    {
        protected string user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            user_id = Session["UserID"] as string;
            if (!IsPostBack)
            {
                DisplayWindows();
            }
        }

        protected void DisplayWindows()
        {
            if (GetArray().Length > 0)
            {
                label1.Text = FindName() + "想要";
                label2.Text = GetArray()[0];
                label3.Text = "可以嗎？ > <";
                IndexCount.Text = "0";
            }
            else
            {
                label1.Text = "";
                label2.Text = "";
                label3.Text = "";
                RadioButtonList1.Visible = false;
                Last.Visible = false;
                Next.Visible = false;
                Panel2.Visible = false;
                Button3.Text = "返回";
                Button3.Click -= Submit_Click;
                Button3.PostBackUrl = "bucket_list.aspx";
                label7.Text = "目前沒有要審核的願望喔!";
            }
        }

        protected string FindName()
        {
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string sql = "SELECT nickname FROM `112-112502`.user\r\nwhere user_id = @user_id";
            string user_name = string.Empty;
            using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
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
                                user_name = reader.GetString(0);
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
            return user_name;
        }

        protected string[] GetArray()
        {
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            List<string> list = new List<string>();
            string sql = "SELECT d_name FROM `112-112502`.bucket_list\r\nwhere user_id = @user_id and pass_state = \'r\';";
            using (MySqlConnection conn = new MySqlConnection(connectionStrings))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string data = reader.GetString(0);
                                list.Add(data);
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
            string[] wish_list = list.ToArray();
            return wish_list;

        }
        //切換須審核的願望
        protected void ChangeWish_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index_value = Convert.ToInt32(IndexCount.Text);
            int length = GetArray().Length;
            if (btn.CommandArgument == "plus")
            {
                index_value++;
            }
            if (btn.CommandArgument == "minus")
            {
                if (index_value > 0)
                {
                    index_value--;
                }
                else
                {
                    index_value = GetArray().Length - 1;
                }
            }
            IndexCount.Text = index_value.ToString();
            label2.Text = GetArray()[index_value % length];

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = RadioButtonList1.SelectedValue;
            if (selectedValue == "y")
            {
                Panel2.Visible = true;
                Panel3.Visible = false;
            }
            if (selectedValue == "n")
            {
                Panel2.Visible = false;
                Panel3.Visible = true;
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string selectedValue = RadioButtonList1.SelectedValue;
            string wish_name = label2.Text;
            string dName;

            string dNum = "SELECT d_num FROM `112-112502`.bucket_list where `d_name` = @wish_name and `user_id` = @user_id and `pass_state` = 'r'";

            using (MySqlConnection conn = new MySqlConnection(connectionStrings)) 
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(dNum, conn))
                    {
                        cmd.Parameters.AddWithValue("@wish_name", wish_name);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            dName = reader.GetString(0);
                        }
                    }

                    if (selectedValue == "y")
                    {
                        int amount;
                        string sql = "update `112-112502`.bucket_list set pass_amount = @amount , pass_state = 'y' where(`d_num` = @dName)";
                        if (!int.TryParse(MoneyTextbox.Text, out amount))
                        {
                            ErrorMessagel.Text = "請輸入有效的金額!";
                            ErrorMessagel.Visible = true;
                            return;
                        }
                        using (MySqlCommand command = new MySqlCommand(sql, conn))
                        {
                            command.Parameters.AddWithValue("@amount", amount);
                            command.Parameters.AddWithValue("@dName", dName);
                            int rows_affected = command.ExecuteNonQuery();
                            if (rows_affected > 0)
                            {
                                MoneyTextbox.Text = null;
                                CauseTextbox.Text = null;
                                ErrorMessagel.Visible = false;
                                string script = "var overlay = document.getElementById('overlay');";
                                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                                script += "var imageBox = document.createElement('img');";
                                script += "imageBox.src = 'images/alert_2Y.png';";
                                script += "imageBox.className = 'custom-image';";
                                script += "document.body.appendChild(imageBox);";
                                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                                script += "setTimeout(function() { imageBox.style.display = 'none'; window.location.href = '" + ResolveUrl("~/src/bucket_review.aspx") + "'; }, 2000);"; // 显示图像一段时间后跳转
                                ClientScript.RegisterStartupScript(GetType(), "新增成功", script, true);
                                DisplayWindows();
                            }
                            else
                            {
                                MoneyTextbox.Text = null;
                                CauseTextbox.Text = null;
                                ErrorMessagel.Visible = false;
                                string script = "var overlay = document.getElementById('overlay');";
                                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                                script += "var imageBox = document.createElement('img');";
                                script += "imageBox.src = 'images/alert_2N.png';";
                                script += "imageBox.className = 'custom-image';";
                                script += "document.body.appendChild(imageBox);";
                                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                                ClientScript.RegisterStartupScript(GetType(), "新增失敗", script, true);
                                DisplayWindows();
                            }
                        }
                    }
                    if (selectedValue == "n")
                    {
                        string reason = CauseTextbox.Text;
                        if (string.IsNullOrEmpty(reason))
                        {
                            ErrorMessage2.Text = "請輸入拒絕原因!";
                            ErrorMessage2.Visible = true;
                            return;
                        }
                        string sql = "update `112-112502`.bucket_list set reason = @reason , pass_state = 'n' where(`d_num` = @dName)";

                        using (MySqlCommand command = new MySqlCommand(sql, conn))
                        {
                            command.Parameters.AddWithValue("@reason", reason);
                            command.Parameters.AddWithValue("@dName", dName);
                            int rows_affect = command.ExecuteNonQuery();

                            if (rows_affect > 0)
                            {
                                MoneyTextbox.Text = null;
                                CauseTextbox.Text = null;
                                string script = "var overlay = document.getElementById('overlay');";
                                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                                script += "var imageBox = document.createElement('img');";
                                script += "imageBox.src = 'images/alert_2Y.png';";
                                script += "imageBox.className = 'custom-image';";
                                script += "document.body.appendChild(imageBox);";
                                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                                script += "setTimeout(function() { imageBox.style.display = 'none'; window.location.href = '" + ResolveUrl("~/src/bucket_review.aspx") + "'; }, 2000);"; // 显示图像一段时间后跳转
                                ClientScript.RegisterStartupScript(GetType(), "新增成功", script, true);
                                DisplayWindows();
                            }
                            else
                            {
                                MoneyTextbox.Text = null;
                                CauseTextbox.Text = null;
                                string script = "var overlay = document.getElementById('overlay');";
                                script += "overlay.style.display = 'block';"; // 顯示背景遮罩
                                script += "var imageBox = document.createElement('img');";
                                script += "imageBox.src = 'images/alert_2N.png';";
                                script += "imageBox.className = 'custom-image';";
                                script += "document.body.appendChild(imageBox);";
                                script += "setTimeout(function() { overlay.style.display = 'none'; }, 2000);"; // 隱藏背景遮罩
                                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);"; // 自动隐藏图像
                                ClientScript.RegisterStartupScript(GetType(), "新增失敗", script, true);
                                DisplayWindows();
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
        }       
        }
    }
