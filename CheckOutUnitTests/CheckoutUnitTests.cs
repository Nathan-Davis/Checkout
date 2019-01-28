﻿using System;
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
            _pointOfSale.ScanItem("Banana", 1);
            _orderTotal = _pointOfSale.CalculateTotalForOrder();
            Assert.AreEqual(2.58M, _orderTotal);
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
            _pointOfSale.ScanItem("Apple", 1);
            _orderTotal = _pointOfSale.CalculateTotalForOrder();
            Assert.AreEqual(_orderTotal, 4.334M);
        }

        private List<PricingSheetItem> GetPricingSheet()
        {
            var pricingSheet = new List<PricingSheetItem>
            {
                new PricingSheetItem("Banana", 2.58M, true),
                new PricingSheetItem("Apple", 0.98M, true),
                new PricingSheetItem("Milk", 3.55M, false),
                new PricingSheetItem("Bread", 4.23M, false),
                new PricingSheetItem("Cereal", 3.98M, false),
                new PricingSheetItem("Water", 1M, false),
                new PricingSheetItem("Cheese", 2.98M, false),
                new PricingSheetItem("Tomato", .88M, true),
                new PricingSheetItem("Lettuce", 2.99M, true),
                new PricingSheetItem("Sugar", 4.98M, false)
            };

            return pricingSheet;
        }
    }
}
