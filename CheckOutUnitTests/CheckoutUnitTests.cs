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
        private decimal _orderTotal;

        [TestInitialize]
        public void InitializeTest()
        {
            _pointOfSale = new CheckoutPointOfSale(GetPricingSheet());
            _orderTotal = 0M;
        }
        [TestMethod]
        public void WhenItemIsAddedTotalIsUpdated()
        {
            _pointOfSale.ScanItem("Banana");
            _orderTotal = _pointOfSale.CalculateTotalForOrder();
            Assert.AreEqual(2.58M, _orderTotal);
            _pointOfSale.ScanItem("Apple");
            _orderTotal = _pointOfSale.CalculateTotalForOrder();
            Assert.AreEqual(3.56M, _orderTotal);
        }

        [TestMethod]
        public void WhenItemIsEnteredWithWeightTotalIsUpdated()
        {
            _pointOfSale.ScanItem("Banana", 1.3M);
            _orderTotal = _pointOfSale.CalculateTotalForOrder();
            Assert.AreEqual(_orderTotal, 3.354M);         
        }

        [TestMethod]
        public void WhenItemIsEnteredByEachesOrWeightTotalIsUpdated()
        {
            _pointOfSale.ScanItem("Banana", 1.3M);
            _pointOfSale.ScanItem("Apple");
            _orderTotal = _pointOfSale.CalculateTotalForOrder();
            Assert.AreEqual(_orderTotal, 4.334M);
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
