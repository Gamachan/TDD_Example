using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ReceiptGenerator.Models;
using ReceiptGenerator.Contracts;
using ReceiptGenerator.Utilities;
using ReceiptGenerator.BusinessLogic.Exceptions;

namespace ReceiptGenerator.BusinessLogic
{
    public class ReceiptManager : BaseManager, IReceiptManager
    {
        #region Private Members

        private ICategoryManager categoryManager;

        #endregion

        #region Private Properties

        private ICategoryManager CategoryManager
        {
            get
            {
                if (categoryManager == null)
                {
                    categoryManager = new CategoryManager();
                }
                return categoryManager;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generate receipt based on input raw data.
        /// </summary>
        /// <param name="rawData">Input textual data.</param>
        /// <returns>Returns receipt entity.</returns>
        public string GenerateReceipt(string rawData)
        {
            Guard.IsNotNullOrEmptyString(rawData, nameof(rawData));
            try
            {
                ShoppingBasket shoppingBasket = ParseShoppingBasket(rawData);

                Receipt receipt = CreateReceipt(shoppingBasket);

                return PrintReceipt(receipt);
            }
            catch (Exception ex) when (ex is BaseReceiptGeneratorException ||
                                       ex is BaseReceiptGeneratorAggreagatedException)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return $"Unexpected error. {ex.Message}";
            }
        }

        #endregion

        #region Private Methods

        private Receipt CreateReceipt(ShoppingBasket shoppingBasket)
        {
            Guard.IsNotNull(shoppingBasket, nameof(shoppingBasket));

            var lines = new List<ReceiptLine>();
            var transformExceptions = new List<BaseReceiptGeneratorException>();

            foreach (var item in shoppingBasket.Items)
            {
                try
                {
                    ReceiptLine newLine = CreateReceiptLine(item);
                    lines.Add(newLine);
                }
                catch (BaseReceiptGeneratorException ex)
                {
                    transformExceptions.Add(ex);
                }
            }

            if (transformExceptions.Any())
            {
                throw new CreateReceiptException(transformExceptions);
            }

            decimal salesTaxes = lines.Sum(line => line.Tax * line.Quantity);
            return new Receipt
            {
                Lines = lines,
                SalesTaxes = salesTaxes,
                Total = lines.Sum(line => line.Price * line.Quantity) + salesTaxes
            };
        }

        private ReceiptLine CreateReceiptLine(ShoppingBasketItem item)
        {
            Guard.IsNotNull(item, nameof(item));

            string categoryName = GetItemCategoryName(item.Name);
            decimal tax = 0;
            decimal taxRatePercentage = 10;
            decimal importTaxRatePercentage = 5;

            if (!CategoryManager.IsCategoryExempt(categoryName))
            {
                tax = item.Price * taxRatePercentage / 100;
            }

            if (item.Name.ToLower().StartsWith(Constants.ImportedWithSpace))
            {
                tax += item.Price * importTaxRatePercentage / 100;
            }

            tax = RoundItemTax(tax);

            return new ReceiptLine
            {
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity,
                Tax = tax
            };
        }

        private string GetItemCategoryName(string itemName)
        {
            if (itemName.ToLower().StartsWith(Constants.ImportedWithSpace))
            {
                itemName = itemName.Substring(Constants.ImportedWithSpace.Length);
            }

            return CategoryManager.GetItemCategoryName(itemName);
        }

        private decimal RoundItemTax(decimal tax)
        {
            return Math.Ceiling(tax * 20) / 20;
        }

        private string PrintReceipt(Receipt receipt)
        {
            Guard.IsNotNull(receipt, nameof(receipt));
            StringBuilder sb = new StringBuilder();

            foreach (var line in receipt.Lines)
            {
                sb.AppendLine(PrintReceiptLine(line));
            }

            sb.AppendLine($"Sales Taxes: {receipt.SalesTaxes.ToString("F")}");
            sb.Append($"Total: {receipt.Total.ToString("F")}");
            return sb.ToString();
        }

        private string PrintReceiptLine(ReceiptLine receiptLine)
        {
            Guard.IsNotNull(receiptLine, nameof(receiptLine));

            return $"{receiptLine.Quantity} {receiptLine.Name}: {((receiptLine.Price + receiptLine.Tax) * receiptLine.Quantity).ToString("F")}";
        }

        private ShoppingBasket ParseShoppingBasket(string rawData)
        {
            Guard.IsNotNullOrEmptyString(rawData, nameof(rawData));

            string[] lines = rawData.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            var items = new List<ShoppingBasketItem>();
            var parsingExceptions = new List<BaseReceiptGeneratorException>();

            foreach (string line in lines)
            {
                try
                {
                    ShoppingBasketItem newItem = ParseShoppingBasketItem(line.Trim());

                    items.Add(newItem);
                }
                catch (BaseReceiptGeneratorException ex)
                {
                    parsingExceptions.Add(ex);
                }
            }

            if (parsingExceptions.Any())
            {
                throw new ParseShoppingBasketAggregatedException(parsingExceptions);
            }

            ShoppingBasket shoppingBasket = new ShoppingBasket();
            shoppingBasket.Items = items;

            return shoppingBasket;
        }

        private ShoppingBasketItem ParseShoppingBasketItem(string itemLine)
        {
            Guard.IsNotNullOrEmptyString(itemLine, nameof(itemLine));

            try
            {
                return new ShoppingBasketItem()
                {
                    Quantity = ParseItemQuantity(itemLine),
                    Name = ParseItemName(itemLine),
                    Price = ParseItemPrice(itemLine),
                };
            }
            catch (Exception ex)
            {
                throw new ParseShoppingBasketItemException(ex.Message, itemLine, ex);
            }
        }

        private decimal ParseItemPrice(string itemLine)
        {
            Guard.IsNotNullOrEmptyString(itemLine, nameof(itemLine));

            int index = itemLine.ToLower().IndexOf(Constants.AtWithSpaces) + 3;
            decimal price = 0;

            string priceStr = itemLine.Substring(index);
            if (!decimal.TryParse(priceStr, out price))
            {
                throw new Exception($"Failed to parse item line price - '{priceStr}'.");
            }

            return price;
        }

        private string ParseItemName(string itemLine)
        {
            Guard.IsNotNullOrEmptyString(itemLine, nameof(itemLine));

            int startIndex = itemLine.IndexOf(' ') + 1;
            int endIndex = itemLine.ToLower().IndexOf(Constants.AtWithSpaces);

            string itemName = itemLine.Substring(startIndex, endIndex - startIndex);

            Guard.IsNotNullOrEmptyString(itemName, nameof(itemName));

            return itemName;
        }

        private int ParseItemQuantity(string itemLine)
        {
            Guard.IsNotNullOrEmptyString(itemLine, nameof(itemLine));

            int index = itemLine.IndexOf(' ');
            int quantity = 0;

            string quantityStr = itemLine.Substring(0, index);
            if (!int.TryParse(quantityStr, out quantity))
            {
                throw new Exception($"Failed to parse item line quantity - '{quantityStr}'.");
            }

            return quantity;
        }

        #endregion
    }
}
