/***
 *Implementing Command Design Pattern
 * ***/
using ShoppingCartApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Business
{
    /// <summary>
    /// Command Class
    /// </summary>
    abstract class Command
    {
        protected IReceiver _receiver = null;
        public Command(IReceiver receiver)
        {
            _receiver = receiver;
        }
        public abstract double Execute();
        public abstract double UnExecute();

    }
    /// <summary>
    /// Concrete Command classes
    /// </summary>
    class CashBackCommand : Command
    {
        public CashBackCommand(IReceiver receiver)
            : base(receiver)
        {

        }
        public override double Execute()
        {
            _receiver.SetAction(ACTION_LIST.CASHBACK);
            return _receiver.GetResult();
        }

        public override double UnExecute()
        {
            _receiver.SetAction(ACTION_LIST.REMOVE_CASHBACK);
            return _receiver.GetResult();
        }
    }

    class DiscountCommand : Command
    {
        public DiscountCommand(IReceiver reciever)
            : base(reciever)
        {

        }
        public override double Execute()
        {
            _receiver.SetAction(ACTION_LIST.DISCOUNT);
            return _receiver.GetResult();
        }

        public override double UnExecute()
        {
            _receiver.SetAction(ACTION_LIST.REMOVE_DISCOUNT);
            return _receiver.GetResult();
        }
    }

    /// <summary>
    /// Receiver Interface
    /// </summary>
    interface IReceiver
    {
        void SetAction(ACTION_LIST action);
        double GetResult();
    }

    enum ACTION_LIST
    {
        CASHBACK,
        DISCOUNT,
        REMOVE_CASHBACK,
        REMOVE_DISCOUNT
    }
    class CalculateOrderAmount : IReceiver
    {
        double x_;
        double y_;
        double discount = 0.00;

        ACTION_LIST currentAction;
        /// <summary>
        /// Constructor to set the Order value and Cashback/Discount percentage
        /// </summary>
        public CalculateOrderAmount(double x, double y)
        {
            x_ = x;
            y_ = y;
        }

        #region IReciever Members

        public void SetAction(ACTION_LIST action)
        {
            currentAction = action;
        }
        /// <summary>
        /// Logic to apply/remove cashback or discount offers
        /// </summary>
        /// <returns>Returns order amount after applying/removing the offer</returns>
        public double GetResult()
        {
            double result;
            discount = (x_ * y_ / 100);
            if (currentAction == ACTION_LIST.CASHBACK)
            {
                result = x_ * y_ / 100; // Cash back value

            }
            else if (currentAction == ACTION_LIST.DISCOUNT)
            {
                result = x_ - discount; // Amount after discount
            }
            else if (currentAction == ACTION_LIST.REMOVE_CASHBACK)
            {
                result = x_;// Remove cashback
            }
            else if (currentAction == ACTION_LIST.REMOVE_DISCOUNT)
            {
                result = x_ + discount; // Remove discount
            }
            else
            {
                result = x_;
            }
            return result;
        }

        #endregion
    }
}
