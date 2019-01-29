using Checkout.Interfaces;

namespace Checkout.SpecialPricingImplementation
{
    public class BogoSpecial : ISpecialPricing
    {
        private decimal _total;
        public decimal Discount { get; set; }
        public int Limit { get; set; }
        public int QuantityDiscounted { get; set; }
        public int QuantityToBuy { get; set; }

        public decimal CalculateSpecial(int quantityScanned, decimal currentPrice)
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
        
        private void CalculateAmountOverBundle(int quantityLeftOver, decimal currentPrice)
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

        private void CalculateBogo(int quantityLeftOver, decimal currentPrice)
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
        
        private void CalculateNormalPricing(int quantityScanned, decimal currentPrice)
        {
            _total += currentPrice * quantityScanned;
        }

        private int ProcessLimitOnSpecial(int quantityScanned, decimal currentPrice)
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
