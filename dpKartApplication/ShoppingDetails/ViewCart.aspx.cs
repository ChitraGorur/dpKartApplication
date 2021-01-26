
using ShoppingCartApplication.Business;
using ShoppingCartApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ShoppingCartApplication.ShoppingDetails
{
    public partial class ViewCart : System.Web.UI.Page
    {
        static double TotalAmountAfterTax = 0.00;

        //COMMAND pattern Initialization
        IReceiver calculateOrderAmount = null;
        Command command = null;
        DiscountCommand discountCommand = null;
        CashBackCommand cashBackCommand = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Populate Cart Grid with current Cart Items
                grdCart.DataSource = LoadCart();
                grdCart.DataBind();

                //Initialize required variables and labels
                double totalAmount = 0.00;
                lblTotalAmount.Text = "0.00";
                lblAmountLabel.Visible = false;
                lblTotalAmount.Visible = false;
                lblEmptyCart.Visible = true;
                lblOfferAmount.Visible = false;
                btnRemove.Enabled = false;

                //Gets total amount of cart items
                var shoppinginstance = ShoppingCart.GetShoppingCart();
                totalAmount = shoppinginstance.CalculateOrderValue();

                if (totalAmount > 0)
                {
                    lblAmountLabel.Visible = true;
                    lblTotalAmount.Visible = true;
                    lblEmptyCart.Visible = false;
                    lblTotalAmount.Text = totalAmount.ToString();
                }
            }
        }
        /// <summary>
        /// Returns list of current cart items
        /// </summary>
        /// <returns>list of current cart items</returns>
        private List<CartItem> LoadCart()
        {
            var shoppinginstance = ShoppingCart.GetShoppingCart();
            return shoppinginstance.SelectedItems;
        }

        protected void grdCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveFromCart")
            {
                // Retrieve the row index stored in the CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button from the Rows collection.
                GridViewRow row = grdCart.Rows[index];
                short productCode = Convert.ToInt16(row.Cells[0].Text);
                double totalAmount = 0.00;
                var shoppinginstance = ShoppingCart.GetShoppingCart();
                CartItem cart = shoppinginstance.SelectedItems.Where(i => i.ProductCode == productCode).FirstOrDefault();
                shoppinginstance.RemoveItem(cart);
                grdCart.DataSource = LoadCart();
                grdCart.DataBind();
                totalAmount = shoppinginstance.CalculateOrderValue();
                if (totalAmount == 0)
                {
                    lblAmountLabel.Text = string.Empty;
                    lblOfferAmount.Text = string.Empty;
                    lblTotalAmount.Text = string.Empty;
                    lblAmountLabel.Visible = false;
                    lblTotalAmount.Visible = false;
                    lblOfferAmount.Visible = false;
                    lblEmptyCart.Visible = true;
                }
                else
                {
                    lblTotalAmount.Text = totalAmount.ToString();
                }
            }
        }

        protected void btnEmptyCart_Click(object sender, EventArgs e)
        {
            var shoppinginstance = ShoppingCart.GetShoppingCart();
            shoppinginstance.EmptyCart();
            grdCart.DataSource = LoadCart();
            grdCart.DataBind();
            lblAmountLabel.Text = string.Empty;
            lblOfferAmount.Text = string.Empty;
            lblTotalAmount.Text = string.Empty;
            lblAmountLabel.Visible = false;
            lblTotalAmount.Visible = false;
            lblOfferAmount.Visible = false;
            lblEmptyCart.Visible = true;
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lblTotalAmount.Text))
            {
                string message = string.Empty;
                calculateOrderAmount = new CalculateOrderAmount(Convert.ToDouble(lblTotalAmount.Text), Convert.ToDouble(rblOffers.SelectedValue));
                discountCommand = new DiscountCommand(calculateOrderAmount);
                cashBackCommand = new CashBackCommand(calculateOrderAmount);

                //Cash back offer selected
                if (rblOffers.SelectedIndex == 0)
                {
                    command = cashBackCommand;
                    message = "Cash back of Rs." + command.Execute().ToString() + " will be added to your account!!!";
                    #region Show a message
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    #endregion
                }
                //Discount Offer Selected    
                else if (rblOffers.SelectedIndex == 1)
                {
                    command = discountCommand;
                    lblTotalAmount.Text = command.Execute().ToString();

                    #region Enable/Disable Controls
                    lblOfferAmount.Text = lblTotalAmount.Text;
                    lblOfferAmount.Visible = true;
                    
                    lblOfferAmount.Font.Strikeout = true;
                    lblOfferAmount.ForeColor = System.Drawing.Color.Red;
                    #endregion
                }

                btnApply.Enabled = false;
                btnRemove.Enabled = true;
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            var shoppinginstance = ShoppingCart.GetShoppingCart();
            string message = string.Empty;

            CalculateTax.AbstractTaxFactory abstractTaxFactory = new ShoppingCartApplication.Business.CalculateTax.TaxFactory();
            if (!string.IsNullOrWhiteSpace(lblTotalAmount.Text))
            {
                //Calculate Total Amount After Tax based on the State code Selected
                TotalAmountAfterTax = 
                    abstractTaxFactory.CalculateTax(drpStateCode.SelectedValue, Convert.ToDouble(lblTotalAmount.Text)).TotalTax;
                
                lblTaxLabel.Visible = true;
                lblTotalAmountAfterTax.Visible = true;
                lblTotalAmountAfterTax.Text = TotalAmountAfterTax.ToString();
                btnCheckout.Visible = true;

                OrderProcessing orderProcessing = new OrderProcessing();
                orderProcessing.OrderProcessWorkFlow(shoppinginstance.SelectedItems, TotalAmountAfterTax);
                //string  message = orderProcessing.GetInvoice();
                message = "Invoice Successfully Generate for a bill amount: " + TotalAmountAfterTax.ToString();
            }
            else
            {
                message = "Your cart is empty!";
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            //Server.Transfer("~\\ShoppingDetails\\Invoice.aspx");
        }

        protected void rblOffers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblOffers.SelectedIndex == 0)
            {
                command = cashBackCommand;
            }
            else if (rblOffers.SelectedIndex == 1)
            {
                command = discountCommand;
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lblOfferAmount.Text))
            {
                string message = string.Empty;
                calculateOrderAmount = new CalculateOrderAmount(Convert.ToDouble(lblOfferAmount.Text), 0.00);
                discountCommand = new DiscountCommand(calculateOrderAmount);
                cashBackCommand = new CashBackCommand(calculateOrderAmount);

                if (rblOffers.SelectedIndex == 0)
                {
                    command = cashBackCommand;
                    message = "Cash back offer is not applied";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }
                else if (rblOffers.SelectedIndex == 1)
                {
                    command = discountCommand;
                    lblOfferAmount.Visible = false;
                    lblTotalAmount.Text = command.UnExecute().ToString();
                }

                btnApply.Enabled = true;
                btnRemove.Enabled = false;
            }
        }
    }
}