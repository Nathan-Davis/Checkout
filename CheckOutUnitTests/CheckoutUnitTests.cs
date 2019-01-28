using System;
using System.Collections.Generic;
using Checkout;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckOutUnitTests
{
    [TestClass]
    public class CheckoutUnitTests
    {
        private CheckoutPointOfSale _pointOfSale;

        [TestInitialize]
        public void InitializeTest()
        {
            _pointOfSale = new CheckoutPointOfSale(GetPricingSheet());
        }
        [TestMethod]
        public void WhenItemIsAddedTotalIsUpdated()
        {
            _pointOfSale.ScanItem("Banana");
            var orderTotal = _pointOfSale.CalculateTotalForOrder();
            Assert.AreEqual(2.58M, orderTotal);
            _pointOfSale.ScanItem("Apple");
            orderTotal = _pointOfSale.CalculateTotalForOrder();
            Assert.AreEqual(3.56M, orderTotal);
        }

        private Dictionary<string, decimal> GetPricingSheet()
        {
            var pricingSheet = new Dictionary<string, decimal>
            {
                {"Banana", 2.58M },
                {"Apple", 0.98M },
                {"Milk", 3.55M },
                {"Bread", 4.23M },
                {"Cereal", 3.98M },
                {"Water", 1M },
                {"Cheese", 2.98M },
                {"Tomato", .88M },
                {"Lettuce", 2.99M },
                {"Sugar", 4.98M }
            };

            return pricingSheet;
        }
    }
}
