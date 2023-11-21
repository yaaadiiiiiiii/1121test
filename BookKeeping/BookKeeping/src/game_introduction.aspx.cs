using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookKeeping.src
{
    public partial class game_introduction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            itd_game1.ImageUrl = "images/game/itd_game1.png";
            itd_game1.Visible = true;
            itd_game2.Visible = false;
            itd_game31.Visible = false;
            itd_game32.Visible = false;
            GameLast.Visible = false;
            GameNext.Visible = false;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            itd_game2.ImageUrl = "images/game/itd_game2.png";
            itd_game2.Visible = true;
            itd_game1.Visible = false;
            itd_game31.Visible = false;
            itd_game32.Visible = false;
            GameLast.Visible = false;
            GameNext.Visible = false;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            itd_game31.ImageUrl = "images/game/itd_game31.png";
            itd_game31.Visible = true;
            itd_game32.Visible = false;
            itd_game2.Visible = false;
            itd_game1.Visible = false;
            GameLast.Visible = false;
            GameNext.Visible = true;
        }
        protected void GameLast_Click(object sender, EventArgs e)
        {
            itd_game31.ImageUrl = "images/game/itd_game31.png";
            itd_game31.Visible = true;
            itd_game32.Visible = false;
            itd_game2.Visible = false;
            itd_game1.Visible = false;
            GameLast.Visible = false;
            GameNext.Visible = true;
        }
        protected void GameNext_Click(object sender, EventArgs e)
        {
            itd_game31.ImageUrl = "images/game/itd_game31.png";
            itd_game31.Visible = false;
            itd_game32.Visible = true;
            itd_game2.Visible = false;
            itd_game1.Visible = false;
            GameLast.Visible = true;
            GameNext.Visible = false;
        }
    }
}