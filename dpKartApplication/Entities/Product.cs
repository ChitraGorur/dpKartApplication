using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Entities
{
    public class CartItem
    {
        public short ProductCategoryCode { get; set; }
        public short ProductTypeCode { get; set; }
        public short ProductCode { get; set; }
        public string ProductName { get; set; }
        public short VendorCode { get; set; }
        public Dictionary<string, string> AttributesSelected { get; set; }
        public short QuantitySelected { get; set; }
        public double TotalCalculatedPrice { get; set; }
        public double UnitPrice { get; set; }
        
        public string Comments { get; set; }

        public CartItem(short _ProductCode, string _ProductName,short _Quantity)
        {
              ProductCode=_ProductCode;
              ProductName = _ProductName;
              QuantitySelected = _Quantity;
        }
    }
    public class Product
    {
        public string ProductDescription { get; set; }
        public short ProductCode{get;set;}
        public string ProductName{get;set;}
        public short VendorCode{get;set;}
        public Dictionary<string, string> ProductAttributes{get;set;}
        public double UnitPrice{get;set;}

        public Product(short _ProductCode, string _ProductName)
        {
            ProductCode = _ProductCode;
            ProductName = _ProductName;
        }
        public Product()
        {
        }
    }
}