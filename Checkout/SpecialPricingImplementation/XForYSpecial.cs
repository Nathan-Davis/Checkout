using Checkout.Interfaces;

namespace Checkout.SpecialPricingImplementation
{
    public class XForYSpecial : ISpecialPricing
    {
        public int Limit { get; set; }
        public int QuantityToBuy { get; set; }
        public decimal SpecialPrice { get; set; }
        public decimal CalculateSpecial(int quantityScanned, decimal currentPrice)
        {
            var total = 0M;
            if (Limit > 0 && quantityScanned > Limit)
            {
                total = (quantityScanned - Limit) * currentPrice;
                quantityScanned = Limit;             
            }

            var bundles = quantityScanned / QuantityToBuy;
            var leftovers = quantityScanned % QuantityToBuy;
            total += CalculatePrice(bundles, SpecialPrice);
            total += CalculatePrice(leftovers, currentPrice);

            return total;
        }

        private static decimal CalculatePrice(int quantity, decimal price)
        {
            return quantity * price;
        }
    }
}
