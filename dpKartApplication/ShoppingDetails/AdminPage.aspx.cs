using ShoppingCartApplication.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCartApplication.ShoppingDetails
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SelectDataSource.SelectedDataSource = Convert.ToInt32(rblSelectDataSource.SelectedValue);
        }

        protected void rblSelectDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectDataSource.SelectedDataSource = Convert.ToInt32(rblSelectDataSource.SelectedValue);
            
        }

        protected void btnSelectDataSource_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ShoppingDetails/ProductCatalog");
        }
    }

    public class SelectDataSource
    {
        public static int SelectedDataSource { get; set; }

    }
}