using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    public class Item
    {
        private string _itemName { get; set; }
        private decimal _price { get; set; }

        internal Item(string itemName, decimal price)
        {
            _itemName = itemName;
            _price = price;
        }

        internal decimal Quantity { get; set; }

        internal decimal CalculateTotal()
        {
            return Quantity * _price;
        }

        internal string RetrieveName()
        {
            return this._itemName;
        }
    }
}
