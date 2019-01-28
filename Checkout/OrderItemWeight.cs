using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    internal class OrderItemWeight : AOrderItem
    {
        private decimal _weight;
        internal OrderItemWeight(string itemName, decimal price, decimal weight) : base(itemName, price)
        {
            _weight = weight;
        }
        internal override decimal CalculateTotal()
        {
            return _weight * GetPrice();
        }

        internal decimal GetWeight()
        {
            return _weight;
        }

        internal void AddToWeight(decimal weightToBeAdded)
        {
            _weight += weightToBeAdded;
        }
    }
}
