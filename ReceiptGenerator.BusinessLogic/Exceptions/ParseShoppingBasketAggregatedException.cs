using System.Collections.Generic;

namespace ReceiptGenerator.BusinessLogic.Exceptions
{
    public class ParseShoppingBasketAggregatedException : BaseReceiptGeneratorAggreagatedException
    {
        public ParseShoppingBasketAggregatedException(List<BaseReceiptGeneratorException> parsingExceptions) : base(parsingExceptions)
        {

        }
    }
}
