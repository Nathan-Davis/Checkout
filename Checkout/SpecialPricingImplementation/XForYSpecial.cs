using Checkout.Interfaces;

namespace Checkout.SpecialPricingImplementation
{
    public class XForYSpecial : ISpecialPricing
    {
        public int QuantityToBuy { get; set; } 
        public decimal SpecialPrice { get; set; }
        public decimal CalculateSpecial(int quantityScanned, decimal currentPrice)
        {
            var bundles = quantityScanned / QuantityToBuy;
            var leftovers = quantityScanned % QuantityToBuy;
            var total = CalculatePrice(bundles, SpecialPrice);
            total += CalculatePrice(leftovers, currentPrice);
            return total;
        }

        private static decimal CalculatePrice(int quantity, decimal price)
        {
            return quantity * price;
        }
    }
}
