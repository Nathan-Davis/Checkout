using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.Abstracts;
using Checkout.Interfaces;
using Checkout.OrderItemImplementation;

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


        public void AddPricingSpecial(string name, ISpecialPricing pricingSpecial)
        {
            var scannedItem = GetItemFromShoppingCartOrInventory(name);
            scannedItem.SpecialPricing = pricingSpecial;
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

        public decimal LineItemVoid(string name)
        {
            if (_shoppingCart.ContainsKey(name))
            {
                var scannedItem = _shoppingCart[name];
                scannedItem.VoidItem();
            }

            return CalculateTotalForOrder();
        }

        public void MarkdownItem(string name, decimal markdown)
        {
            var orderItem = GetItemFromShoppingCartOrInventory(name);
            orderItem.MarkdownPrice(markdown);
        }

        public decimal ScanItem(string name)
        {
            if (_shoppingCart.ContainsKey(name))
            {
                var scannedItem = (OrderItemEaches)_shoppingCart[name];
                scannedItem.AddLineItem();
            }
            else
            {
                var inventoryItem = _inventory.GetOrderItemEaches(name);
                inventoryItem.AddLineItem();
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

        private AOrderItem GetItemFromShoppingCartOrInventory(string name)
        {
            return _shoppingCart.ContainsKey(name) ? _shoppingCart[name] : _inventory.GetOrderItem(name);
        }
    }
}
