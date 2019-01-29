using Checkout.Abstracts;

namespace Checkout.OrderItemImplementation
{
    internal class OrderItemEaches : AOrderItem
    {
        internal OrderItemEaches(string itemName, decimal price) : base(itemName, price)
        {

        }

        private int _quantity { get; set; }

        internal void AddLineItem()
        {
            _quantity += 1;
        }

        internal override decimal CalculateTotal()
        {
            if (SpecialPricing != null)
            {
                return SpecialPricing.CalculateSpecial(_quantity, GetCurrentPrice());
            }
            return _quantity * GetCurrentPrice();
        }

        internal override void VoidItem()
        {
            _quantity -= 1;
        }
    }
}
