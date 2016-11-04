using System;

namespace ReceiptGenerator.BusinessLogic.Exceptions
{
    public abstract class BaseReceiptGeneratorException : Exception
    {
        public BaseReceiptGeneratorException(string message, Exception innerException) : base(message, innerException)
        {
            // Log error.
        }

        public BaseReceiptGeneratorException(string message) : base(message)
        {
            // Log error.
        }
    }
}
