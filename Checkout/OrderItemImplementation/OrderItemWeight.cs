using Checkout.Abstracts;

namespace Checkout.OrderItemImplementation
{
    internal class OrderItemWeight : AOrderItem
    {
        private decimal _weight;
        internal OrderItemWeight(string itemName, decimal price, decimal weight) : base(itemName, price)
        {
            _weight = weight;
        }

        internal void AddToWeight(decimal weightToBeAdded)
        {
            _weight += weightToBeAdded;
        }

        internal override decimal CalculateTotal()
        {
            return _weight * GetCurrentPrice();
        }

        internal override void VoidItem()
        {
            _weight = 0M;
        }        
    }
}
