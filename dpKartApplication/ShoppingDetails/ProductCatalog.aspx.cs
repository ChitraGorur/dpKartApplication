using ShoppingCartApplication.Adapter;
using ShoppingCartApplication.Business;
using ShoppingCartApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCartApplication
{
    public partial class ProductCatalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdProductCatalog.DataSource = LoadProducts();
                grdProductCatalog.DataBind();
            }
        }
        private List<Product> LoadProducts()
        {
            //List of Products
            List<Product> lstProducts = new List<Product>();

            //
            SonyAdapter sonyAdapter = new SonyAdapter();
            LGAdapter lgAdapter = new LGAdapter();

            lstProducts.AddRange(sonyAdapter.GetProductCatalog());            
            lstProducts.AddRange(lgAdapter.GetProductCatalog());

            return lstProducts;
        }

        protected void grdProductCatalog_RowCommand(object sender,GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                // Retrieve the row index stored in the CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button from the Rows collection.
                GridViewRow row = grdProductCatalog.Rows[index];

                #region Get values from that particular row of the grid
                var ddlQuantityValue = row.FindControl("ddlQuantity") as DropDownList;
                short productCode = Convert.ToInt16(row.Cells[0].Text);
                string productName = row.Cells[1].Text;
                short vendorCode = Convert.ToInt16(row.Cells[4].Text);
                short quantity = Convert.ToInt16(ddlQuantityValue.SelectedValue);
                short unitprice = Convert.ToInt16(row.Cells[3].Text);
                #endregion

                //Populate cart item
                CartItem cart = new CartItem(productCode, productName, quantity);
                cart.VendorCode = vendorCode;
                cart.UnitPrice = unitprice;
                cart.TotalCalculatedPrice = unitprice * quantity;

                //Implementing SINGLETON pattern
                //TODO: Add an item to the cart
                var shoppingInstance = ShoppingCart.GetShoppingCart();
                shoppingInstance.AddItem(cart);

            } // end of if block
        }// end of RowCommand EventHandler
    }
}