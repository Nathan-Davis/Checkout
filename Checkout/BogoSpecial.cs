using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    public class BogoSpecial
    {
        public int QuantityToBuy { get; set; }

        public int QuantityDiscounted { get; set; }

        public decimal Discount { get; set; }

        private decimal _total;

        internal decimal CalculateSpecial(int quantityScanned, decimal currentPrice)
        {
            _total = 0M;
            var quantityLeftOver = quantityScanned - QuantityToBuy;
            if (quantityLeftOver > 0)
            {
                CalculateBogo(quantityScanned, currentPrice);
            }
            else
            {
                CalculateNormalPricing(quantityScanned, currentPrice);
            }
            return _total;
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

        private void CalculateNormalPricing(int quantityScanned, decimal currentPrice)
        {
            _total += currentPrice * quantityScanned;
        }
    }
}
