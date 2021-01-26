/***
 *Implementing Singleton Design Pattern
 * ***/
using ShoppingCartApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Business
{
    /// <summary>
    /// Implementing SINGLE-TON pattern
    /// </summary>
    public class ShoppingCart
    {
       
        public List<CartItem> SelectedItems;
        public double ShoppingCartValue;
        // TODO: declare private static attribute 
        private static ShoppingCart ShoppingCartInstance;

        // TODO : implement an inaccessible constructor   
        private ShoppingCart()
        {
            SelectedItems = new List<CartItem>();
        }

        //TODO: public operation to get a single instance of ShoppingCart class
        public static ShoppingCart GetShoppingCart()
        {
            //Creating Single Cart object
            if (ShoppingCartInstance == null)
            {
                ShoppingCartInstance = new ShoppingCart();
            }
            return ShoppingCartInstance;
        }
        public bool AddItem(CartItem selectedItem)
        {
            ShoppingCartInstance.SelectedItems.Add(selectedItem);
            return true;
        }
        public void RemoveItem(CartItem selectedItem)
        {
            ShoppingCartInstance.SelectedItems.Remove(selectedItem);
        }

        public void EmptyCart()
        {
            ShoppingCartInstance.SelectedItems.Clear();
        }
        public void CheckOut()
        {
        }

        public void ValidateExpiry()
        {
        }
        /// <summary>
        /// Gets total amount of cart items
        /// </summary>
        /// <returns>Total Amount</returns>
        public double CalculateOrderValue()
        {
            double TotalAmount=0.00;
            foreach(CartItem amount in SelectedItems)
            {
                TotalAmount += amount.TotalCalculatedPrice;
            }
            return TotalAmount;
        }
        

        
    }
}