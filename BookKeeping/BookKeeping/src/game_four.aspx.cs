using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookKeeping.src
{
    public partial class game_four : System.Web.UI.Page
    {

        int[] prices = { 25, 10, 53, 37, 8, 20, 45, 12 };
        int[] itemCount = { 3, 2, 3, 1, 2, 2, 1, 2 };


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeGame3_2();
            }
        }

        protected void InitializeGame3_2()
        {
            int paymentAmount = CalculatePaymentAmount(itemCount, prices);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "PaymentAmountScript", $"var totalPaymentAmount = {paymentAmount}; updateTotalPayment();", true);


        }

        private int CalculatePaymentAmount(int[] items, int[] prices)
        {
            int totalAmount = 0;

            for (int i = 0; i <prices.Length; i++)
            {
                int quantity = items[i];
                int price = prices[i];

                totalAmount += quantity * price;
            }

            return totalAmount;
        }

        protected void Check_Click(object sender, EventArgs e)
        {
            int paymentAmount = CalculatePaymentAmount(itemCount, prices);
            int totalAmount = Convert.ToInt32(Request.Form["hiddentotal"].ToString());
            if (paymentAmount == totalAmount)
            {
                ClientScript.RegisterStartupScript(GetType(), "答對了", "alert('答對了！');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "答錯了", "alert('答錯了！');", true);
            }
        }
    }
}