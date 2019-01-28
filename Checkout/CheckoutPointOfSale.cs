using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    public class CheckoutPointOfSale
    {
        private Dictionary<string, decimal> _pricingSheet { get; set; }
        private List<Item> _shoppingCart { get; set; }
        public CheckoutPointOfSale(Dictionary<string, decimal> pricingSheet)
        {
            _pricingSheet = pricingSheet;
            ImportItemsWithPricing();
        }

        public void ScanItem(string name)
        {
            var scannedItem = _shoppingCart.Where(item => item.RetrieveName() == name).FirstOrDefault();
            scannedItem.Quantity += 1;
        }

        public void ScanItem(string name, decimal weight)
        {
            var scannedItem = _shoppingCart.Where(item => item.RetrieveName() == name).FirstOrDefault();
            scannedItem.Weight += weight;
        }

        public decimal CalculateTotalForOrder()
        {
            var orderTotal = 0M;
            foreach (var item in _shoppingCart)
            {
                orderTotal += item.CalculateTotal();
            }
            return orderTotal;
        }

        private void ImportItemsWithPricing()
        {
            _shoppingCart = new List<Item>();
            foreach (var price in _pricingSheet)
            {
                var item = new Item(price.Key, price.Value);
                _shoppingCart.Add(item);
            }
        }

    }
}
