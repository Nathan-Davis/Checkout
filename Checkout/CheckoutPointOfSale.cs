using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout
{
    public class CheckoutPointOfSale
    {
        private List<PricingSheetItem> _pricingSheet;
        private List<AOrderItem> _shoppingCart { get; set; }
        public CheckoutPointOfSale(List<PricingSheetItem> pricingSheet)
        {
            _pricingSheet = pricingSheet;
            ImportItemsWithPricing();
        }

        public void ScanItem(string name)
        {
            var scannedItem = _shoppingCart.Where(item => item.GetName() == name).FirstOrDefault();
            scannedItem.Quantity += 1;
        }

        public void ScanItem(string name, decimal weight)
        {
            var scannedItem = (OrderItemWeight)_shoppingCart.Where(item => item.GetName() == name).FirstOrDefault();
            scannedItem.AddToWeight(weight);
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

        public void MarkdownItem(string name, decimal markdown)
        {
            var orderItem = _shoppingCart.Where(item => item.GetName() == name).FirstOrDefault();
            orderItem.MarkdownPrice(markdown);
        }

        private void ImportItemsWithPricing()
        {
            _shoppingCart = new List<AOrderItem>();
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

                _shoppingCart.Add(orderItem);
            }
        }

    }
}
