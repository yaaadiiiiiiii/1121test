using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BookKeeping.src
{
    public partial class game_second : System.Web.UI.Page
    {
        private int totalQuestions = 5;
        private int currentQuestion = 0;
        //public Dictionary<string, int> questionQuantities;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                correctcnt.Text = "0";
                GameProgress.Text = currentQuestion.ToString() + "/" + totalQuestions.ToString();
                GameProgressBar.Attributes["style"] = "0";
                // 初始化游戏
                InitializeGame2();
            }
        }

        protected void InitializeGame2()
        {
            SecondGamePanel.Visible = true;
            // 生成隨機數量（例如，0到3之間的隨機數量）
            Random random = new Random();

            // 面額數量的字典
            Dictionary<int, int> denominationCounts = new Dictionary<int, int>
                {
                    {1000, 0},
                    {500, 0},
                    {100, 0},
                    {50, 0},
                    {10, 0},
                    {5, 0},
                    {1, 0}
                };

            // 遍歷每個面額，生成隨機數量的圖片並添加到對應的<asp:Panel>元素
            var tempDenominationCounts = new Dictionary<int, int>(denominationCounts);

            foreach (var denomination in tempDenominationCounts.Keys)
            {
                int randomQuantity = random.Next(0, 3); // 0到3之間的隨機數

                // 更新字典中的數量
                denominationCounts[denomination] = randomQuantity;

                // 找到對應的<asp:Panel>元素
                Panel denominationPanel = (Panel)FindControl("Panel" + denomination);

                // 清空<asp:Panel>元素的內容
                denominationPanel.Controls.Clear();

                // 生成隨機數量的圖片，然後將它們添加到<asp:Panel>元素中
                for (int i = 0; i < randomQuantity; i++)
                {
                    Image image = new Image();
                    image.ImageUrl = "images/game/money_" + denomination + ".png"; // 圖片的URL
                    image.Height = GetImageHeight(denomination); // 設置圖片高度
                    image.Width = GetImageWidth(denomination); // 設置圖片寬度
                    denominationPanel.Controls.Add(image); // 將圖片添加到<asp:Panel>元素中
                }
            }


            // 計算總金額
            int totalAmount = CalculateTotalAmount(denominationCounts);


            GenerateAnswers(totalAmount);

            UpdateProgressText();
        }


        // 根據面額返回圖片高度
        private int GetImageHeight(int denomination)
        {
            switch (denomination)
            {
                case 1000:
                    return 45;
                case 500:
                    return 42;
                case 100:
                    return 40;
                case 50:
                    return 45;
                case 10:
                    return 40;
                case 5:
                    return 37;
                default:
                    return 32;
            }
        }


        // 根據面額返回圖片寬度
        private int GetImageWidth(int denomination)
        {
            switch (denomination)
            {
                case 1000:
                    return 120;
                case 500:
                    return 110;
                case 100:
                    return 95;
                case 50:
                    return 45;
                case 10:
                    return 40;
                case 5:
                    return 37;
                default:
                    return 32;
            }
        }


        // 計算總金額

        private int CalculateTotalAmount(Dictionary<int, int> denominationCounts)
        {
            int totalAmount = 0;

            // 創建一個臨時集合以遍歷
            var denominationsToIterate = denominationCounts.Keys.ToList();

            foreach (var denomination in denominationsToIterate)
            {
                totalAmount += denomination * denominationCounts[denomination];
            }

            return totalAmount;
        }
        private void GenerateAnswers(int totalAmount)
        {
            Random random = new Random();

            // 生成兩個不同的錯誤答案
            int wrongAnswer1 = totalAmount;
            int wrongAnswer2 = totalAmount;
            while (wrongAnswer1 == totalAmount)
            {
                int changeAmount = random.Next(1, 500); // 隨機生成一個金額來加減
                bool addAmount = random.Next(0, 2) == 0; // 隨機決定是加還是減
                if (addAmount)
                    wrongAnswer1 += changeAmount;
                else
                    wrongAnswer1 -= changeAmount;
                    if (wrongAnswer1 <= 0) 
                    {
                    wrongAnswer1 += changeAmount * 2;
                    }
            }

            while (wrongAnswer2 == totalAmount || wrongAnswer2 == wrongAnswer1)
            {
                int changeAmount = random.Next(1, 500); // 隨機生成一個金額來加減
                bool addAmount = random.Next(0, 2) == 0; // 隨機決定是加還是減
                if (addAmount)
                    wrongAnswer2 += changeAmount;
                else
                    wrongAnswer2 -= changeAmount;
                    if (wrongAnswer2 <= 0) 
                    {
                        wrongAnswer2 += changeAmount * 2;
                    }
            }

            // 隨機分配答案到按鈕
            int correctButtonIndex = random.Next(4, 7); // 隨機選擇1、2或3
            int answer1 = 0, answer2 = 0, answer3 = 0;
            randomNum.Text = correctButtonIndex.ToString();
            if (correctButtonIndex == 4)
            {
                answer1 = totalAmount;
                answer2 = wrongAnswer1;
                answer3 = wrongAnswer2;
            }
            else if (correctButtonIndex == 5)
            {
                answer1 = wrongAnswer1;
                answer2 = totalAmount;
                answer3 = wrongAnswer2;
            }
            else
            {
                answer1 = wrongAnswer1;
                answer2 = wrongAnswer2;
                answer3 = totalAmount;
            }

            // 設置按鈕的文字為答案
            Ans4.Text = answer1.ToString() + "元";
            Ans5.Text = answer2.ToString() + "元";
            Ans6.Text = answer3.ToString() + "元";
        }



        private void UpdateProgressText()
        {
            int count = int.Parse(GameProgress.Text[0].ToString());
            count++;
            GameProgress.Text = count.ToString() + "/" + totalQuestions.ToString();

            // 更新进度条样式
            double progress = (double)count / totalQuestions * 100;
            string progressBarStyle = $"width: {progress}%;"; // 設置進度條寬度
            GameProgressBar.Attributes["style"] = progressBarStyle;
        }

        protected void CheckAnswer(object sender, EventArgs e)
        {
            // 获取用户选择的答案
            Button selectedButton = (Button)sender;
            int selectedAnswerIndex = int.Parse(selectedButton.CommandArgument);
            int correctAnswerIndex = int.Parse(randomNum.Text);
            int nextquestion = int.Parse(GameProgress.Text[0].ToString()) + 1;
            int corcnt = Convert.ToInt32(correctcnt.Text);
            // 检查答案是否正确
            if (selectedAnswerIndex == correctAnswerIndex)
            {
                corcnt++;
                correctcnt.Text = corcnt.ToString();
                string script = "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/game/correct.png';";
                script += "imageBox.className = 'result-image';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);";
                ClientScript.RegisterStartupScript(GetType(), "答對了'", script, true);

                if (nextquestion <= totalQuestions)
                {
                    InitializeGame2();
                }
                else
                {
                    GameReord();
                }

            }
            else
            {
                string script = "var imageBox = document.createElement('img');";
                script += "imageBox.src = 'images/game/wrong.png';";
                script += "imageBox.className = 'result-image';";
                script += "document.body.appendChild(imageBox);";
                script += "setTimeout(function() { imageBox.style.display = 'none'; }, 2000);";
                ClientScript.RegisterStartupScript(GetType(), "答錯了'", script, true);

                if (nextquestion <= totalQuestions)
                {
                    InitializeGame2();
                }
                else
                {
                    GameReord();
                }
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
                            string script = "$('#resultMessage').text('答對了 " + corcnt + " 題'); $('#resultModal').modal('show');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ShowResultModal", script, true);
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
            Response.Redirect("game_second.aspx"); // 跳轉到 game_second.aspx
        }

        protected void LeaveGame_Click(object sender, EventArgs e)
        {
            GameReord();
        }
    }
}

