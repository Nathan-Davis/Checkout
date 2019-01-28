using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    internal abstract class AOrderItem
    {
        private readonly string _itemName;
        private readonly decimal _price;

        internal AOrderItem(string itemName, decimal price)
        {
            _itemName = itemName;
            _price = price;
        }

        internal decimal Quantity { get; set; }

        internal abstract decimal CalculateTotal();       

        internal string GetName()
        {
            return _itemName;
        }

        protected decimal GetPrice()
        {
            return _price;
        }
    }
}
