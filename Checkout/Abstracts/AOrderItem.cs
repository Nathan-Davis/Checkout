using Checkout.Interfaces;

namespace Checkout.Abstracts
{
    internal abstract class AOrderItem
    {
        private readonly string _itemName;
        private readonly decimal _originalPrice;
        private decimal _currentPrice;

        internal AOrderItem(string itemName, decimal price)
        {
            _itemName = itemName;
            _originalPrice = price;
            _currentPrice = price;
        }       

        internal ISpecialPricing SpecialPricing { get; set; }

        internal abstract decimal CalculateTotal();
        internal abstract void VoidItem();

        internal string GetName()
        {
            return _itemName;
        }

        internal void MarkdownPrice(decimal markdown)
        {
            _currentPrice -= markdown;
        }

        protected decimal GetOriginalPrice()
        {
            return _originalPrice;
        }

        protected decimal GetCurrentPrice()
        {
            return _currentPrice;
        }
    }
}
