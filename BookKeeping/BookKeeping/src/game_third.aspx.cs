using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using Org.BouncyCastle.Ocsp;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace BookKeeping.src
{
    public partial class game_third : System.Web.UI.Page
    {
        public Dictionary<string, int> questionQuantities;
        

        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack) 
            {
                InitializeGame3_1();
            }
                
        }

        protected void InitializeGame3_1()
        {
            ThirdGamePanel1.Visible = true;
            ThirdGamePanel2.Visible = false;
            correctcnt.Text = "0"; /*初始化答對題數*/
            Repeater1.DataSource = GetImageData();
            Repeater1.DataBind();

            // Serialize questionQuantities to JSON
            var jsonQuestionQuantities = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(questionQuantities);

            // Inject the JSON string into the JavaScript code
            Page.ClientScript.RegisterStartupScript(this.GetType(), "QuestionQuantitiesScript", $"var questionQuantities = {jsonQuestionQuantities};", true);
        }

        public class GameImage
        {
            public int SisRandomNumber { get; set; }
            public int GlueRandomNumber { get; set; }
            public int CorRandomNumber { get; set; }
            public int RulerRandomNumber { get; set; }
            public int RedRandomNumber { get; set; }
            public int GreenRandomNumber { get; set; }
            public int BlueRandomNumber { get; set; }
            public int BlackRandomNumber { get; set; }
        }

        protected List<GameImage> GetImageData()
        {
            List<GameImage> images = new List<GameImage>();

            Random rand = new Random();

            questionQuantities = new Dictionary<string, int>();

            var itemNames = new List<string> { "Redd", "Greenn", "Bluee", "Blackk", "Siss", "Gluee", "Corr", "Rulerr" };

            foreach (var itemName in itemNames)
            {
                int randomNumber = rand.Next(1, 4);
                questionQuantities.Add(itemName, randomNumber);
            }
            images.Add(new GameImage
            {
                RedRandomNumber = questionQuantities["Redd"],
                GreenRandomNumber = questionQuantities["Greenn"],
                BlueRandomNumber = questionQuantities["Bluee"],
                BlackRandomNumber = questionQuantities["Blackk"],
                SisRandomNumber = questionQuantities["Siss"],
                GlueRandomNumber = questionQuantities["Gluee"],
                CorRandomNumber = questionQuantities["Corr"],
                RulerRandomNumber = questionQuantities["Rulerr"]

            });

            return images;
        }

        protected int[] GetRandomList(int r , int g , int b , int k ,  int s , int gl , int c , int ru ) 
        {
            int[] randomList = new int[8];
            randomList[0] = r;
            randomList[1] = g;
            randomList[2] = b;
            randomList[3] = k;
            randomList[4] = s;
            randomList[5] = gl;
            randomList[6] = c;
            randomList[7] = ru;

            return randomList;
        }

        protected void Check3_1_Click(object sender, EventArgs e)
        {
            int redQ, greenQ, blueQ, blackQ, sisQ, glueQ, corQ, rulerQ;
            int cnt = 0;
            int red = Convert.ToInt32(Request.Form["hiddenred"].ToString());
            int green = Convert.ToInt32(Request.Form["hiddengreen"].ToString());
            int blue = Convert.ToInt32(Request.Form["hiddenblue"].ToString());
            int black = Convert.ToInt32(Request.Form["hiddenblack"].ToString());
            int sis = Convert.ToInt32(Request.Form["hiddensis"].ToString());
            int glue = Convert.ToInt32(Request.Form["hiddenglue"].ToString());
            int cor = Convert.ToInt32(Request.Form["hiddencor"].ToString());
            int ruler = Convert.ToInt32(Request.Form["hiddenruler"].ToString());

            // 获取 Red 的数量
            Label redLabel = (Label)Repeater1.Controls[0].FindControl("Red");
            redQ = Convert.ToInt32(redLabel.Text);

            // 获取 Green 的数量
            Label greenLabel = (Label)Repeater1.Controls[0].FindControl("Green");
            greenQ = Convert.ToInt32(greenLabel.Text);

            // 获取 Blue 的数量
            Label blueLabel = (Label)Repeater1.Controls[0].FindControl("Blue");
            blueQ = Convert.ToInt32(blueLabel.Text);

            // 获取 Black 的数量
            Label blackLabel = (Label)Repeater1.Controls[0].FindControl("Black");
            blackQ = Convert.ToInt32(blackLabel.Text);

            // 获取 Sis 的数量
            Label sisLabel = (Label)Repeater1.Controls[0].FindControl("Sis");
            sisQ = Convert.ToInt32(sisLabel.Text);

            // 获取 Glue 的数量
            Label glueLabel = (Label)Repeater1.Controls[0].FindControl("Glue");
            glueQ = Convert.ToInt32(glueLabel.Text);

            // 获取 Cor 的数量
            Label corLabel = (Label)Repeater1.Controls[0].FindControl("Cor");
            corQ = Convert.ToInt32(corLabel.Text);

            // 获取 Ruler 的数量
            Label rulerLabel = (Label)Repeater1.Controls[0].FindControl("Ruler");
            rulerQ = Convert.ToInt32(rulerLabel.Text);


            if (red == redQ)
            {
                cnt++;
            }
            if (green == greenQ)
            {
                cnt++;
            }
            if (blue == blueQ)
            {
                cnt++;
            }
            if (black == blackQ)
            {
                cnt++;
            }
            if (sis == sisQ)
            {
                cnt++;
            }
            if (glue == glueQ)
            {
                cnt++;
            }
            if (cor == corQ)
            {
                cnt++;
            }
            if (ruler == rulerQ)
            {
                cnt++;
            }

            if (cnt == 8)
            {
                string script = "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/game/correct.png';";
                script += "imageBox.className = 'result-image';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);";
                ClientScript.RegisterStartupScript(GetType(), "答對了'", script, true);
                InitializeGame3_2();
            }
            else
            {
                string script = "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/game/wrong.png';";
                script += "imageBox.className = 'result-image';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);";
                ClientScript.RegisterStartupScript(GetType(), "答錯了'", script, true);
                GameReord();
            }
        }
        protected void InitializeGame3_2()
        {
            ThirdGamePanel1.Visible = false;
            ThirdGamePanel2.Visible = true;
            int paymentAmount = CalculatePaymentAmount();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "PaymentAmountScript", $"var totalPaymentAmount = {paymentAmount}; updateTotalPayment();", true);
        }

        private int CalculatePaymentAmount()
        {
            int redQ, greenQ, blueQ, blackQ, sisQ, glueQ, corQ, rulerQ;

            // 获取 Red 的数量
            Label redLabel = (Label)Repeater1.Controls[0].FindControl("Red");
            redQ = Convert.ToInt32(redLabel.Text);

            // 获取 Green 的数量
            Label greenLabel = (Label)Repeater1.Controls[0].FindControl("Green");
            greenQ = Convert.ToInt32(greenLabel.Text);

            // 获取 Blue 的数量
            Label blueLabel = (Label)Repeater1.Controls[0].FindControl("Blue");
            blueQ = Convert.ToInt32(blueLabel.Text);

            // 获取 Black 的数量
            Label blackLabel = (Label)Repeater1.Controls[0].FindControl("Black");
            blackQ = Convert.ToInt32(blackLabel.Text);

            // 获取 Sis 的数量
            Label sisLabel = (Label)Repeater1.Controls[0].FindControl("Sis");
            sisQ = Convert.ToInt32(sisLabel.Text);

            // 获取 Glue 的数量
            Label glueLabel = (Label)Repeater1.Controls[0].FindControl("Glue");
            glueQ = Convert.ToInt32(glueLabel.Text);

            // 获取 Cor 的数量
            Label corLabel = (Label)Repeater1.Controls[0].FindControl("Cor");
            corQ = Convert.ToInt32(corLabel.Text);

            // 获取 Ruler 的数量
            Label rulerLabel = (Label)Repeater1.Controls[0].FindControl("Ruler");
            rulerQ = Convert.ToInt32(rulerLabel.Text);
            int[] prices = { 25, 10, 53, 37, 8, 20, 45, 12 };
            int[] items = GetRandomList(redQ, greenQ, blueQ, blackQ, sisQ, glueQ, corQ, rulerQ);
            int totalAmount = 0;

            for (int i = 0; i < prices.Length; i++)
            {
                int quantity = items[i];
                int price = prices[i];

                totalAmount += quantity * price;
            }

            return totalAmount;
        }

        protected void Check3_2_Click(object sender, EventArgs e)
        {
            int paymentAmount = CalculatePaymentAmount();
            int totalAmount = Convert.ToInt32(Request.Form["hiddentotal"].ToString());
            int corcnt = Convert.ToInt32(correctcnt.Text);
            if (paymentAmount == totalAmount)
            {
                
                corcnt++;
                correctcnt.Text = corcnt.ToString();

                string script = "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/game/correct.png';";
                script += "imageBox.className = 'result-image';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);";
                ClientScript.RegisterStartupScript(GetType(), "答對了'", script, true);
                GameReord();

            }
            else
            {
                string script = "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/game/wrong.png';";
                script += "imageBox.className = 'result-image';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);";
                ClientScript.RegisterStartupScript(GetType(), "答錯了'", script, true);
                GameReord();
            }
           
        }

        protected void GameReord()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string user_id = Session["UserID"].ToString();
            int corcnt = Convert.ToInt32(correctcnt.Text);
            DateTime overtime = DateTime.Now;
            string sql = "INSERT INTO `112-112502`.`gamedata` (user_id , time , score ) VALUES (@userid , @time , @score);";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@userid", user_id);
                        cmd.Parameters.AddWithValue("@time", overtime);
                        cmd.Parameters.AddWithValue("@score", corcnt);

                        int rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected > 0)
                        {
                            // 触发模态框
                            if (corcnt > 0)
                            {
                                string script = "$('#resultMessage').text('恭喜過關'); $('#resultModal').modal('show');";
                                ClientScript.RegisterStartupScript(this.GetType(), "ShowResultModal", script, true);
                            }
                            else
                            {
                                string script = "$('#resultMessage').text('通關失敗'); $('#resultModal').modal('show');";
                                ClientScript.RegisterStartupScript(this.GetType(), "ShowResultModal", script, true);
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

        protected void RestartGame(object sender, EventArgs e)
        {
            Response.Redirect("game_third.aspx"); // 跳轉到 game_third.aspx
        }

        protected void LeaveGame_Click(object sender, EventArgs e)
        {
            GameReord();
        }
    }
}