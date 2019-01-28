using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    public class CheckoutPointOfSale
    {
        private readonly List<PricingSheetItem> _pricingSheet;
        private readonly Dictionary<string, AOrderItem> _shoppingCart = new Dictionary<string, AOrderItem>();
        private readonly Dictionary<string, AOrderItem> _inventory = new Dictionary<string, AOrderItem>();
        public CheckoutPointOfSale(List<PricingSheetItem> pricingSheet)
        {
            _pricingSheet = pricingSheet;
            ImportItemsWithPricing();
        }

        public decimal ScanItem(string name)
        {
            if (_shoppingCart.ContainsKey(name))
            {
                var scannedItem = _shoppingCart[name];
                scannedItem.Quantity += 1;
            }
            else
            {
                var inventoryItem = _inventory[name];
                inventoryItem.Quantity += 1;
                _shoppingCart.Add(inventoryItem.GetName(), inventoryItem);
            }
            return CalculateTotalForOrder();
        }

        public decimal ScanItem(string name, decimal weight)
        {
            if (_shoppingCart.ContainsKey(name))
            {
                var scannedItem = (OrderItemWeight)_shoppingCart[name];
                scannedItem.AddToWeight(weight);
            }
            else
            {
                var inventoryItem = (OrderItemWeight)_inventory[name];
                inventoryItem.AddToWeight(weight);
                _shoppingCart.Add(inventoryItem.GetName(), inventoryItem);
            }
            return CalculateTotalForOrder();
        }

        public decimal CalculateTotalForOrder()
        {
            var orderTotal = 0M;
            foreach (var item in _shoppingCart)
            {
                orderTotal += item.Value.CalculateTotal();
            }
            return orderTotal;
        }

        public void MarkdownItem(string name, decimal markdown)
        {
            var orderItem = _shoppingCart.ContainsKey(name) ? _shoppingCart[name] : _inventory[name];
            orderItem.MarkdownPrice(markdown);
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
