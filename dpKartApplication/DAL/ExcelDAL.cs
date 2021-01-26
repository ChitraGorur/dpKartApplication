using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Excel;
using ShoppingCartApplication.Entities;
using System.Data.OleDb;
using ShoppingCartApplication.Adapter;

namespace ShoppingCartApplication.DAL
{
    public class ExcelDAL
    {
        protected string connectionString;
        public List<Vendor_Sony.SonyProduct> GetDataFromExcel()
        {
            List<Vendor_Sony.SonyProduct> productList = new List<Vendor_Sony.SonyProduct>();
            //DataAccessObject excelDataAccessObject = new ExcelDataAccessObject();
            //productList = excelDataAccessObject.Run();

            string s = HttpContext.Current.Server.MapPath("~/App_Data/ShoppingCart.xls");
            connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties='Excel 8.0';Data Source=" + s;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);

                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        #region data reader logic
                        Vendor_Sony.SonyProduct product = new Vendor_Sony.SonyProduct();
                        product.ProductDescription = dr[0].ToString();
                        product.ProductCode = Convert.ToInt16(dr[1]);
                        product.ProductName = dr[2].ToString();
                        product.VendorCode = Convert.ToInt16(dr[3]);
                        product.UnitPrice = Convert.ToDouble(dr[4]);
                        //var row1Col0 = dr[0];

                        productList.Add(product);
                        #endregion
                    }
                }
            }
            return productList;
        }
    }
}