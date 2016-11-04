using System;

namespace ReceiptGenerator.BusinessLogic.Exceptions
{
    public class ParseShoppingBasketItemException : BaseReceiptGeneratorException
    {
        public string ItemLine { get; set; }

        public ParseShoppingBasketItemException(string message, string itemLine, Exception innerException) : base(message, innerException)
        {
            ItemLine = itemLine;
        }
    }
}
