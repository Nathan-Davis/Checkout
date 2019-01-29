using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    internal class OrderItemEaches : AOrderItem
    {
        internal OrderItemEaches(string itemName, decimal price) : base(itemName, price)
        {

        }

        internal override decimal CalculateTotal()
        {
            if (SpecialPricing != null)
            {
                return SpecialPricing.CalculateSpecial(Quantity, GetCurrentPrice());
            }
            return Quantity * GetCurrentPrice();
        }
    }
}
