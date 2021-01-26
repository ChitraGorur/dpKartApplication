/***
 * Used the Adapter Design pattern 
 * Added 2 vendors e.g. Sony and LG
 * and created Adaptee and Adapter class for each implementing Adapter Pattern
 * ***/

using ShoppingCartApplication.DataBridge;
using ShoppingCartApplication.Entities;
using ShoppingCartApplication.ShoppingDetails;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Adapter
{
    public interface IProduct
    {
        List<Product> GetProductCatalog();
    }

    #region Sony Adaptee and Adapter
    #region Sony Adaptee
    public class Vendor_Sony
    {
        public int dataSourceSelected=0;
        public class SonyProduct
        {
            public string ProductDescription { get; set; }
            public short ProductCode { get; set; }
            public string ProductName { get; set; }
            public short VendorCode { get; set; }
            public Dictionary<string, string> ProductAttributes { get; set; }
            public double UnitPrice { get; set; }

            public SonyProduct(string _ProductDescription, short _ProductCode, string _ProductName, short _VendorCode, double _UnitPrice)
            {
                ProductDescription = _ProductDescription;
                ProductCode = _ProductCode;
                ProductName = _ProductName;
                VendorCode = _VendorCode;
                UnitPrice = _UnitPrice;
            }

            public SonyProduct()
            {

            }

        }

        public List<SonyProduct> FetchProductDetails()
        {
            List<SonyProduct> lstSonyProduct = new List<SonyProduct>();
            List<SonyProduct> productList = new List<SonyProduct>();
            DataSource dataSource = new DataSource();
            //Get data source selected by an Admin : extra code
            dataSourceSelected = SelectDataSource.SelectedDataSource;
            switch (dataSourceSelected)
            {
                case 0:
                    dataSource.dataSource = new ExcelDataSource();
                    break;
                case 1:
                    dataSource.dataSource = new FileDataSource();
                    break;
                default:
                    dataSource.dataSource = new ExcelDataSource();
                    break;
            }

            productList = dataSource.GetProductList();

            #region Process product list
            foreach (SonyProduct product in productList)
            {
                SonyProduct sonyProduct = new SonyProduct();
                sonyProduct.ProductDescription = product.ProductDescription;
                sonyProduct.ProductCode = product.ProductCode;
                sonyProduct.ProductName = product.ProductName;
                sonyProduct.VendorCode = product.VendorCode;
                sonyProduct.UnitPrice = product.UnitPrice;
                lstSonyProduct.Add(sonyProduct);
            }
            #endregion 

            return lstSonyProduct;
        }
    }
    #endregion

    #region Sony Adapter

    public class SonyAdapter : IProduct
    {
        public List<Product> GetProductCatalog()
        {
            List<Product> lstProduct = new List<Product>();
            Vendor_Sony vendorSonyObj = new Vendor_Sony();
            List<Vendor_Sony.SonyProduct> lstSonyProducts = vendorSonyObj.FetchProductDetails();

            foreach (Vendor_Sony.SonyProduct sonyProduct in lstSonyProducts)
            {
                Product product = new Product();
                product.ProductDescription = sonyProduct.ProductDescription;
                product.ProductCode = sonyProduct.ProductCode;
                product.ProductName = sonyProduct.ProductName;
                product.VendorCode = sonyProduct.VendorCode;
                product.UnitPrice = sonyProduct.UnitPrice;
                lstProduct.Add(product);
            }
            return lstProduct;
        }
    }
    #endregion

    #endregion

    #region LG Adaptee and Adapter
    #region LG Adaptee
    public class Vendor_LG
    {
        public class LGProduct
        {
            #region Properties
            public string ProductDescription { get; set; }
            public short ProductCode { get; set; }
            public string ProductName { get; set; }
            public short VendorCode { get; set; }
            public Dictionary<string, string> ProductAttributes { get; set; }
            public double UnitPrice { get; set; }
            #endregion
            public LGProduct(string _ProductDescription, short _ProductCode, string _ProductName, short _VendorCode, 
                double _UnitPrice)
            {
                ProductDescription = _ProductDescription;
                ProductCode = _ProductCode;
                ProductName = _ProductName;
                VendorCode = _VendorCode;
                UnitPrice = _UnitPrice;
            }
        }

        public List<LGProduct> FetchProductDetails()
        {
            List<LGProduct> lstProducts = new List<LGProduct>();
            LGProduct prod1 = new LGProduct("LG TV LED Smart", 1, "LG Smart", 2, 3200.000);
            LGProduct prod2 = new LGProduct("LG TV LED Smart 3D", 2, "LG 3D", 2, 6400.000);
            LGProduct prod3 = new LGProduct("LG TV LED Smart Curve", 3, "LG Curve 4K", 2, 6500.000);

            lstProducts.Add(prod1);
            lstProducts.Add(prod2);
            lstProducts.Add(prod3);

            return lstProducts;
        }
    }
    #endregion

    #region LG Adapter

    public class LGAdapter : IProduct
    {
        public List<Product> GetProductCatalog()
        {
            List<Product> lstProduct = new List<Product>();
            Vendor_LG vendorLGObj = new Vendor_LG();
            List<Vendor_LG.LGProduct> lstLGProducts = vendorLGObj.FetchProductDetails();

            foreach (Vendor_LG.LGProduct lgProduct in lstLGProducts)
            {
                Product product = new Product();
                product.ProductDescription = lgProduct.ProductDescription;
                product.ProductCode = lgProduct.ProductCode;
                product.ProductName = lgProduct.ProductName;
                product.VendorCode = lgProduct.VendorCode;
                product.UnitPrice = lgProduct.UnitPrice;
                lstProduct.Add(product);
            }
            return lstProduct;
        }
    }
    #endregion
    #endregion
}