/***
 *Implementing Facade Design Pattern
 * ***/
using ShoppingCartApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace ShoppingCartApplication.Business
{
    public class OrderProcessing
    {
        static string Invoice = null;
        public void OrderProcessWorkFlow(List<CartItem> cartItems,double totalAmount) 
        {
            foreach(CartItem cartItem in cartItems)
            {
                #region Update Inventory logic
                switch (cartItem.VendorCode)
                {
                    case 1:
                        Sony_OrderProcess sonyProcess = new Sony_OrderProcess();
                        if(sonyProcess.UpdateVendor())
                        {
                            UpdateInventory();
                        }
                        break;
                    case 2:
                        LG_OrderProcess lgProcess = new LG_OrderProcess();
                        if (lgProcess.UpdateVendor())
                        {
                            UpdateInventory();
                        }
                        break;
                }
                #endregion
            }

           Invoice = GenerateInvoice(cartItems, totalAmount);
        }

        public string GetInvoice()
        {
            return Invoice;
        }
        //Business logic to update inventory
        public void UpdateInventory() 
        {
            //Business logic to update inventory
        }

        //Logic to generate Invoice for cart items
        public string GenerateInvoice(List<CartItem> cartItems, double totalAmount) 
        {
            string message = "Invoice Successfully Generated :" + Environment.NewLine;
            foreach (CartItem cartItem in cartItems)
            {
                message += cartItem.ProductName + "\t" + cartItem.UnitPrice + "\t" + cartItem.QuantitySelected + Environment.NewLine;
            }
            message += "Total Amount : " + totalAmount.ToString();

            

            return message;
        }

        public void DispatchOrder() { }

    }

    public class Sony_OrderProcess
    {
        public bool UpdateVendor()
        {
            return true;
        }
    }

    public class LG_OrderProcess
    {
        public bool UpdateVendor()
        {
            return true;
        }
    }

}