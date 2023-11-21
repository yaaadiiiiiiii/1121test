using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.EnterpriseServices;
using System.Linq;
using System.Web.UI.WebControls;

namespace BookKeeping.src
{
    public partial class game_first : System.Web.UI.Page
    {
        private List<int> faceValues = new List<int> { 1, 5, 10, 50, 100, 500, 1000 };
        private Random random = new Random();
        private int currentQuestionIndex = 0;
        private int totalQuestions = 5;
        private int currentQuestion = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                correctcnt.Text = "0";
                GameProgress.Text = currentQuestion.ToString() + "/" + totalQuestions.ToString();
                GameProgressBar.Attributes["style"] = "0";
                // 初始化游戏
                InitializeGame1();
            }
        }

        protected void InitializeGame1()
        {
            FirstGamePanel.Visible = true;

            currentQuestionIndex = random.Next(faceValues.Count);

            List<string> options = new List<string>();

            // 随机生成一个位置作为正确答案的位置
            int correctAnswerIndex = random.Next(0, 2);
            randomNum.Text = correctAnswerIndex.ToString();

            // 生成一个列表包含所有可能的错误答案的索引
            List<int> possibleWrongAnswers = Enumerable.Range(0, faceValues.Count).ToList();

            // 移除正确答案的索引
            possibleWrongAnswers.Remove(currentQuestionIndex);

            for (int i = 0; i < 3; i++)
            {
                if (i == correctAnswerIndex)
                {
                    options.Add(faceValues[currentQuestionIndex].ToString() + "元");
                }
                else
                {
                    // 随机选择一个错误答案
                    int randomWrongAnswerIndex = random.Next(possibleWrongAnswers.Count);
                    int wrongAnswerIndex = possibleWrongAnswers[randomWrongAnswerIndex];

                    options.Add(faceValues[wrongAnswerIndex].ToString() + "元");

                    // 移除已经选中的错误答案，以确保没有重复
                    possibleWrongAnswers.RemoveAt(randomWrongAnswerIndex);
                }
            }



            // 设置问题和选项
            question1.Text = "請問下圖中為多少金額？";
            Image1.ImageUrl = "images/game/money_" + faceValues[currentQuestionIndex] + ".png";
            Ans1.Text = options[0];
            Ans2.Text = options[1];
            Ans3.Text = options[2];

            UpdateProgressText();
        }
      
        private void UpdateProgressText()
        {
            int count = int.Parse(GameProgress.Text[0].ToString());
            count++;
            GameProgress.Text = count.ToString() + "/" + totalQuestions.ToString();

            // 更新进度条样式
            double progress = (double)count / totalQuestions * 100;
            string progressBarStyle = $"width: {progress}%;"; // 設置進度條寬度
            GameProgressBar.Attributes["style"] = progressBarStyle ;
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
                    InitializeGame1();
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
                    InitializeGame1();
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
            Response.Redirect("game_first.aspx"); // 跳轉到 game_first.aspx
        }

        protected void LeaveGame_Click(object sender, EventArgs e)
        {
            GameReord();
        }
    }
}