using System.Collections.Generic;

namespace ReceiptGenerator.BusinessLogic.Exceptions
{
    public class CreateReceiptException : BaseReceiptGeneratorAggreagatedException
    {
        public CreateReceiptException(List<BaseReceiptGeneratorException> innerExceptions) : base(innerExceptions)
        {

        }
    }
}
