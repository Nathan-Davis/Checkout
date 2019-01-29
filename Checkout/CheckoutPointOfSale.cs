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

        private readonly Inventory _inventory;

        public CheckoutPointOfSale(List<PricingSheetItem> pricingSheet)
        {
            _inventory = new Inventory(pricingSheet);
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
                var inventoryItem = _inventory.GetOrderItemEaches(name);
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
                var inventoryItem = _inventory.GetOrderItemWeight(name);
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

        public void AddPricingSpecial(string name, BogoSpecial pricingSpecial)
        {
            var scannedItem = GetItemFromShoppingCartOrInventory(name);
            scannedItem.SpecialPricing = pricingSpecial;
        }

        public void MarkdownItem(string name, decimal markdown)
        {
            var orderItem = GetItemFromShoppingCartOrInventory(name);
            orderItem.MarkdownPrice(markdown);
        }

        private AOrderItem GetItemFromShoppingCartOrInventory(string name)
        {
            return _shoppingCart.ContainsKey(name) ? _shoppingCart[name] : _inventory.GetOrderItem(name);
        }
    }
}
