/***
 *Implementing Factory Design Pattern
 * ***/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Business
{
    public class CalculateTax
    {
        #region Abstract and concrete tax classes
        public abstract class AbstractTax
        {
            public double OrderValue;
            public double SalesTax;
            public double VAT;
            public double TotalTax;

            public abstract void CalculateTotalTax();
        }

        public class Tax_India : AbstractTax
        {
            public override void CalculateTotalTax()
            {
                SalesTax = 5.00;
                VAT = 4.00;
                TotalTax = (((OrderValue * VAT / 100) + OrderValue) * SalesTax / 100) + OrderValue;
            }
        }

        public class Tax_OutsideIndia : AbstractTax
        {
            public override void CalculateTotalTax()
            {
                SalesTax = 12.00;
                VAT = 5.50;
                TotalTax = (((OrderValue * VAT / 100) + OrderValue) * SalesTax / 100) + OrderValue;
            }
        }
        #endregion

        #region abstract and concrete creator with factory method
        public abstract class AbstractTaxFactory
        {
            public abstract AbstractTax GetTaxObject(string StateCode);

            public AbstractTax CalculateTax(string StateCode,double OrderValue)
            {
                AbstractTax objTax = this.GetTaxObject(StateCode);
                if(objTax!=null)
                {
                    objTax.OrderValue = OrderValue;
                    objTax.CalculateTotalTax();
                }
                return objTax;
            }

        }

        public class TaxFactory:AbstractTaxFactory
        {
            public override AbstractTax GetTaxObject(string StateCode)
            {
                switch(StateCode)
                {
                    case "IND":
                        return new Tax_India();
                    case "OutsideIndia":
                        return new Tax_OutsideIndia();
                    default:
                        return new Tax_India();
                }
                //throw new NotImplementedException();
            }
        }

        #endregion
    }
}