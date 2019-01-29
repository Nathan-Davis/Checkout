using Checkout.Interfaces;

namespace Checkout.SpecialPricingImplementation
{
    public class XForYSpecial : ISpecialPricing
    {
        public int Limit { get; set; }
        public int QuantityToBuy { get; set; }
        public decimal SpecialPrice { get; set; }
        public decimal CalculateSpecial(decimal units, decimal currentPrice)
        {
            var total = 0M;
            var quantityScanned = (int)units;
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

        private static decimal CalculatePrice(decimal quantity, decimal price)
        {
            return quantity * price;
        }
    }
}
