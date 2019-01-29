using System.Collections.Generic;

namespace Checkout
{
    internal class Inventory
    {
        private readonly List<PricingSheetItem> _pricingSheet;
        private readonly Dictionary<string, AOrderItem> _inventory = new Dictionary<string, AOrderItem>();

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