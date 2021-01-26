using ShoppingCartApplication.DAL;
using ShoppingCartApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCartApplication.Adapter;

namespace ShoppingCartApplication.DataBridge
{
    interface IDataSource
    {
        List<Vendor_Sony.SonyProduct> GetData();
    }

    class ExcelDataSource : IDataSource
    {
        const string SOURCE_NAME = "ExcelDataSource";
        ExcelDAL excelData = new ExcelDAL();
        List<Vendor_Sony.SonyProduct> IDataSource.GetData()
        {
            return excelData.GetDataFromExcel();
        }
    }

    class FileDataSource : IDataSource
    {
        const string SOURCE_NAME = "FileDataSource";
        FileDAL fileDAL = new FileDAL();
        List<Vendor_Sony.SonyProduct> IDataSource.GetData()
        {
            return fileDAL.GetDataFromFile();
        }
    }

    class DataSource
    {
        IDataSource currentDataSource = null;
        List<Vendor_Sony.SonyProduct> productList = new List<Vendor_Sony.SonyProduct>();
        public IDataSource dataSource
        {
            get
            {
                return currentDataSource;
            }
            set
            {
                currentDataSource = value;
            }
        }

        public List<Vendor_Sony.SonyProduct> GetProductList()
        {
            if (currentDataSource != null)
            {
               productList =  currentDataSource.GetData();
            }
            else
            {
                productList = null;
            }
            return productList;
        }
    }
}