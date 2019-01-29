using System.Collections.Generic;
using Checkout.Abstracts;
using Checkout.OrderItemImplementation;

namespace Checkout
{
    internal class Inventory
    {
        private readonly Dictionary<string, AOrderItem> _inventory = new Dictionary<string, AOrderItem>();
        private readonly List<PricingSheetItem> _pricingSheet;
        
        internal Inventory(List<PricingSheetItem> pricingSheet)
        {
            _pricingSheet = pricingSheet;
            ImportItemsWithPricing();
        }

        internal AOrderItem GetOrderItem(string name)
        {
            return _inventory[name];
        }

        internal OrderItemEaches GetOrderItemEaches(string name)
        {
            return (OrderItemEaches)_inventory[name];
        }

        internal OrderItemWeight GetOrderItemWeight(string name)
        {
            return (OrderItemWeight)_inventory[name];
        }

        private void ImportItemsWithPricing()
        {
            foreach (var pricingSheetItem in _pricingSheet)
            {
                AOrderItem orderItem;
                if (pricingSheetItem.GetShouldItemPriceBeCaluclatedByWeight())
                {
                    orderItem = new OrderItemWeight(pricingSheetItem.GetName(), pricingSheetItem.GetPrice(), 0M);
                }
                else
                {
                    orderItem = new OrderItemEaches(pricingSheetItem.GetName(), pricingSheetItem.GetPrice());
                }

                _inventory.Add(pricingSheetItem.GetName(), orderItem);
            }
        }
    }
}