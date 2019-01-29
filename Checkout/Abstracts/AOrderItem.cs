using Checkout.Interfaces;

namespace Checkout.Abstracts
{
    internal abstract class AOrderItem
    {
        private decimal _currentPrice;
        private readonly string _itemName;
        private readonly decimal _originalPrice;
        
        internal AOrderItem(string itemName, decimal price)
        {
            _itemName = itemName;
            _originalPrice = price;
            _currentPrice = price;
        }

        internal abstract decimal CalculateTotal();
        internal ISpecialPricing SpecialPricing { get; set; }        
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
