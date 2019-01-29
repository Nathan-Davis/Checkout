using Checkout.Interfaces;

namespace Checkout.SpecialPricingImplementation
{
    public class BogoSpecialEaches : ISpecialPricing
    {
        private decimal _total;
        public decimal Discount { get; set; }
        public decimal Limit { get; set; }
        public decimal QuantityDiscounted { get; set; }
        public decimal QuantityToBuy { get; set; }

        public decimal CalculateSpecial(decimal quantityScanned, decimal currentPrice)
        {
            _total = 0M;
            quantityScanned = ProcessLimitOnSpecial(quantityScanned, currentPrice);
            var quantityLeftover = quantityScanned - QuantityToBuy;
            if (quantityLeftover > 0)
            {
                CalculateBogo(quantityScanned, currentPrice);
            }
            else
            {
                CalculateNormalPricing(quantityScanned, currentPrice);
            }
            return _total;
        }
        
        private void CalculateAmountOverBundle(decimal quantityLeftOver, decimal currentPrice)
        {
            quantityLeftOver -= QuantityToBuy;
            _total += QuantityToBuy * currentPrice;
            if (quantityLeftOver >= QuantityDiscounted)
            {
                quantityLeftOver -= QuantityDiscounted;
                _total += QuantityDiscounted * currentPrice * Discount;
                CalculateBogo(quantityLeftOver, currentPrice);
            }
            else
            {
                _total += quantityLeftOver * Discount;
            }
        }

        private void CalculateBogo(decimal quantityLeftOver, decimal currentPrice)
        {
            if (quantityLeftOver >= QuantityToBuy)
            {
                CalculateAmountOverBundle(quantityLeftOver, currentPrice);
            }
            else
            {
                _total += quantityLeftOver * currentPrice;
            }
        }
        
        private void CalculateNormalPricing(decimal quantityScanned, decimal currentPrice)
        {
            _total += currentPrice * quantityScanned;
        }

        private decimal ProcessLimitOnSpecial(decimal quantityScanned, decimal currentPrice)
        {
            if (Limit > 0 && quantityScanned > Limit)
            {
                _total += (quantityScanned - Limit) * currentPrice;
                quantityScanned = Limit;
            }

            return quantityScanned;
        }
    }
}
