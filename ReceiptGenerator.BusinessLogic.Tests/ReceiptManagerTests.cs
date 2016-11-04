using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReceiptGenerator.Contracts;
using ReceiptGenerator.BusinessLogic.Exceptions;
using System.Linq;

namespace ReceiptGenerator.BusinessLogic.Tests
{
    [TestClass]
    public class ReceiptManagerTests
    {
        private IReceiptManager receiptManager;

        [TestInitialize]
        public void Setup()
        {
            receiptManager = new ReceiptManager();
        }

        [TestMethod]
        public void GenerateReceipt_NullInput_ThrowsArgumentException()
        {
            try
            {
                receiptManager.GenerateReceipt(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void GenerateReceipt_EmptyStringInput_ThrowsArgumentException()
        {
            try
            {
                receiptManager.GenerateReceipt(string.Empty);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void GenerateReceipt_LineWithoutValidQuantity_ThrowsParseShoppingItemException()
        {
            string input = "one book at 12.95";
            try
            {
                receiptManager.GenerateReceipt(input);
            }
            catch (ParseShoppingBasketAggregatedException ex)
            {
                Assert.AreEqual(1, ex.InnerExceptions.Count);
                Assert.IsInstanceOfType(ex.InnerExceptions.First(), typeof(ParseShoppingBasketItemException));
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void GenerateReceipt_LineWithoutValidPrice_ThrowsParseShoppingItemException()
        {
            string input = "2 book at 2$";
            try
            {
                receiptManager.GenerateReceipt(input);
            }
            catch (ParseShoppingBasketAggregatedException ex)
            {
                Assert.AreEqual(1, ex.InnerExceptions.Count);
                Assert.IsInstanceOfType(ex.InnerExceptions.First(), typeof(ParseShoppingBasketItemException));
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void GenerateReceipt_LineWithAnEmptyName_ThrowsParseShoppingItemException()
        {
            string input = "2 at 12.95";
            try
            {
                receiptManager.GenerateReceipt(input);
            }
            catch (ParseShoppingBasketAggregatedException ex)
            {
                Assert.AreEqual(1, ex.InnerExceptions.Count);
                Assert.IsInstanceOfType(ex.InnerExceptions.First(), typeof(ParseShoppingBasketItemException));
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void GenerateReceipt_ImportedSingleExemptedItem()
        {
            string input = "1 imported box of chocolates at 12.95";
            string expectedResult = string.Format(
                           "1 imported box of chocolates: 13.60{0}Sales Taxes: 0.65{0}Total: 13.60", Environment.NewLine);
            string result = receiptManager.GenerateReceipt(input);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GenerateReceipt_ImportedMultipleExemptedItem()
        {
            string input = "2 imported box of chocolates at 12.95";
            string expectedResult = string.Format(
                           "2 imported box of chocolates: 27.20{0}Sales Taxes: 1.30{0}Total: 27.20", Environment.NewLine);

            string result = receiptManager.GenerateReceipt(input);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GenerateReceipt_NotImportedSingleExemptedItem()
        {
            string input = "1 box of chocolates at 12.95";
            string expectedResult = string.Format(
                           "1 box of chocolates: 12.95{0}Sales Taxes: 0.00{0}Total: 12.95", Environment.NewLine);
            string result = receiptManager.GenerateReceipt(input);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GenerateReceipt_NotImportedMultipleExemptedItem()
        {
            string input = "2 box of chocolates at 12.95";
            string expectedResult = string.Format(
                           "2 box of chocolates: 25.90{0}Sales Taxes: 0.00{0}Total: 25.90", Environment.NewLine);

            string result = receiptManager.GenerateReceipt(input);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GenerateReceipt_ImportedSingleNotExemptedItem()
        {
            string input = "1 imported bottle of perfume at 54.65";
            string expectedResult = string.Format(
                           "1 imported bottle of perfume: 62.85{0}Sales Taxes: 8.20{0}Total: 62.85", Environment.NewLine);
            string result = receiptManager.GenerateReceipt(input);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GenerateReceipt_ImportedMultipleNotExemptedItem()
        {
            string input = "2 imported bottle of perfume at 54.65";
            string expectedResult = string.Format(
                           "2 imported bottle of perfume: 125.70{0}Sales Taxes: 16.40{0}Total: 125.70", Environment.NewLine);

            string result = receiptManager.GenerateReceipt(input);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GenerateReceipt_NotImportedSingleNotExemptedItem()
        {
            string input = "1 bottle of perfume at 54.65";
            string expectedResult = string.Format(
                           "1 bottle of perfume: 60.15{0}Sales Taxes: 5.50{0}Total: 60.15", Environment.NewLine);
            string result = receiptManager.GenerateReceipt(input);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GenerateReceipt_NotImportedMultipleNotExemptedItem()
        {
            string input = "2 bottle of perfume at 54.65";
            string expectedResult = string.Format(
                           "2 bottle of perfume: 120.30{0}Sales Taxes: 11.00{0}Total: 120.30", Environment.NewLine);

            string result = receiptManager.GenerateReceipt(input);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
