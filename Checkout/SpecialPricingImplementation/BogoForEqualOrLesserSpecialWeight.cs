using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.Interfaces;

namespace Checkout.SpecialPricingImplementation
{
    public class BogoForEqualOrLesserSpecialWeight : ISpecialPricing
    {
        private decimal _total;
        public decimal Discount { get; set; }
        public decimal WeightToBuy { get; set; }      
        
        public decimal CalculateSpecial(decimal weight, decimal currentPrice)
        {
            if (weight >= WeightToBuy)
            {
                var weightLeftover = weight - WeightToBuy;
                var heavierItem = weightLeftover > WeightToBuy ? weightLeftover : WeightToBuy;
                var lighterItem = weightLeftover < WeightToBuy ? weightLeftover : WeightToBuy;
                _total += heavierItem * currentPrice;
                _total += lighterItem * currentPrice * Discount;
            }
            else
            {
                _total = weight * currentPrice;
            }

            return _total;
        }
    }
}
