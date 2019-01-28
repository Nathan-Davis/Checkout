using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    public class PricingSheetItem
    {
        private readonly string _itemName;
        private readonly decimal _itemPrice;
        private bool _shouldItemPriceBeCalculatedByWeight;

        public PricingSheetItem(string itemName, decimal itemPrice, bool shouldItemPriceBeCalculatedByWeight)
        {
            _itemName = itemName;
            _itemPrice = itemPrice;
            _shouldItemPriceBeCalculatedByWeight = shouldItemPriceBeCalculatedByWeight;
        }
        
        public string GetName()
        {
            return _itemName;
        }

        public decimal GetPrice()
        {
            return _itemPrice;
        }
        public bool GetShouldItemPriceBeCaluclatedByWeight()
        {
            return _shouldItemPriceBeCalculatedByWeight;
        }

        public void SetItemPriceCalculationByWeight()
        {
            _shouldItemPriceBeCalculatedByWeight = true;
        }

        public void SetItemPriceCalculationByEaches()
        {
            _shouldItemPriceBeCalculatedByWeight = false;
        }
    }
}
