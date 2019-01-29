﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Checkout;
using Checkout.SpecialPricingImplementation;
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
            _orderTotal = _pointOfSale.ScanItem("Banana", 1.3M);
            Assert.AreEqual(_orderTotal, 3.354M);
        }

        [TestMethod]
        public void WhenItemIsEnteredByEachesOrWeightTotalIsUpdated()
        {
            _orderTotal = _pointOfSale.ScanItem("Banana", 1.3M);
            _orderTotal = _pointOfSale.ScanItem("Apple", 1);
            Assert.AreEqual(_orderTotal, 4.334M);
        }

        [TestMethod]
        public void WhenMarkDownIsAddedToItemTotalIsUpdatedByEaches()
        {
            var markdown = .30M;
            _pointOfSale.MarkdownItem("Milk", markdown);
            _orderTotal = _pointOfSale.ScanItem("Milk");
            Assert.AreEqual(3.25M, _orderTotal);
        }

        [TestMethod]
        public void WhenMarkDownIsAddedToItemTotalIsUpdatedByWeight()
        {
            _pointOfSale.ScanItem("Apple", .5M);
            var markdown = .30M;
            _pointOfSale.MarkdownItem("Apple", markdown);
            _orderTotal = _pointOfSale.ScanItem("Apple", .5M);
            Assert.AreEqual(.68M, _orderTotal);
        }

        [TestMethod]
        public void WhenShoppingCartIsLargePerformanceIsntGreatlyImpacted()
        {
            var watch = new Stopwatch();
            watch.Start();
            for (var i = 0; i < 1000; i++)
            {
                _orderTotal = ScanEntireInventory();
            }
            Assert.IsTrue(watch.Elapsed < new TimeSpan(0, 0, 1));
        }

        [TestMethod]
        public void WhenABogoSpecialIsAddedTheOrderTotalIsAdjusted()
        {
            var pricingSpecial = new BogoSpecial {QuantityToBuy = 2, QuantityDiscounted = 2, Discount = .50M };
            _pointOfSale.AddPricingSpecial("Water", pricingSpecial);
            ScanItemMultipleTimes("Water", 21);
            Assert.AreEqual(_orderTotal, 16M);
        }

        [TestMethod]
        public void WhenAnXForYSpecialIsAddedTheOrderTotalIsAdjusted()
        {
            var pricingSpecial = new XForYSpecial { QuantityToBuy = 3, SpecialPrice = 10M};
            _pointOfSale.AddPricingSpecial("Bread", pricingSpecial);
            ScanItemMultipleTimes("Bread", 8);
            Assert.AreEqual(_orderTotal, 28.46M);
        }

        [TestMethod]
        public void WhenALimitIsAddedToXForYSpecialTotalIsAdjusted()
        {
            var pricingSpecial = new XForYSpecial { QuantityToBuy = 3, SpecialPrice = 1M, Limit = 6 };
            _pointOfSale.AddPricingSpecial("Water", pricingSpecial);
            ScanItemMultipleTimes("Water", 9);
            Assert.AreEqual(_orderTotal, 5M);
        }

        [TestMethod]
        public void WhenALimitIsAddedToBogoSpecialTotalIsAdjusted()
        {
            var pricingSpecial = new BogoSpecial() { QuantityToBuy = 3, QuantityDiscounted = 1, Discount = 0M, Limit = 4 };
            _pointOfSale.AddPricingSpecial("Bread", pricingSpecial);
            ScanItemMultipleTimes("Bread", 20);
            Assert.AreEqual(_orderTotal, 80.37M);
        }

        private static List<PricingSheetItem> GetPricingSheet()
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

        private decimal ScanEntireInventory()
        {
            var pricingSheet = GetPricingSheet();
            foreach (var pricingSheetItem in pricingSheet)
            {
                _orderTotal = pricingSheetItem.GetShouldItemPriceBeCaluclatedByWeight() ? _pointOfSale.ScanItem(pricingSheetItem.GetName(), .25M) :
                    _pointOfSale.ScanItem(pricingSheetItem.GetName());
            }
            return _orderTotal;
        }

        private void ScanItemMultipleTimes(string name, int numberOfTimesToScan)
        {
            for (var i = 0; i < numberOfTimesToScan; i++)
            {
                _orderTotal = _pointOfSale.ScanItem(name);
            }
        }
    }
}
