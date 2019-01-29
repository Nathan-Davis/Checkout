using Checkout.Abstracts;

namespace Checkout.OrderItemImplementation
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
