using ShoppingCartApplication.Adapter;
using ShoppingCartApplication.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.DAL
{
    public class FileDAL
    {
        public List<Vendor_Sony.SonyProduct> GetDataFromFile()
        {
            List<Vendor_Sony.SonyProduct> productList = new List<Vendor_Sony.SonyProduct>();
            string path = HttpContext.Current.Server.MapPath("~/App_Data/ShoppingCart.txt");

            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] col = line.Split(new char[] { ';' });
                    // process col[0], col[1], col[2]
                    Vendor_Sony.SonyProduct product = new Vendor_Sony.SonyProduct();
                    product.ProductDescription = col[0].ToString();
                    product.ProductCode = Convert.ToInt16(col[1]);
                    product.ProductName = col[2].ToString();
                    product.VendorCode = Convert.ToInt16(col[3]);
                    product.UnitPrice = Convert.ToDouble(col[4]);
                    productList.Add(product);
                }
            }
            return productList;
        }
    }
}