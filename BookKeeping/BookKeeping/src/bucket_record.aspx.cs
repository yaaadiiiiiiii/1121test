using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.SessionState;

namespace BookKeeping.src
{
    public partial class bucket_record : System.Web.UI.Page
    {
        protected string user_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                user_id = Session["UserID"] as string;
                string connectionStrings = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                using (MySqlConnection conn = new MySqlConnection(connectionStrings))
                {
                    try
                    {
                        conn.Open();
                        BucketRecord(conn);
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

        protected void BucketRecord(MySqlConnection connection)
        {
            string sql = "SELECT d_num, d_name, pass_amount, exchange_time, exchange_state FROM `112-112502`.bucket_list WHERE user_id = @user_id and exchange_state in ('D', 'R', 'Y');";

            using (MySqlCommand cmd = new MySqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue("@user_id", user_id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    GridView2.DataSource = reader;
                    GridView2.DataBind();
                }
            }
        }

        protected string GetExchangeStatus(object exchangeState)
        {
            if (exchangeState != null && exchangeState.ToString() == "Y")
            {
                return "已兌換";
            }
            else if(exchangeState != null && exchangeState.ToString() == "D")
            {
                return "刪除";
            }

            else
            {
                return "拒絕";
            }
        }

    }
}
