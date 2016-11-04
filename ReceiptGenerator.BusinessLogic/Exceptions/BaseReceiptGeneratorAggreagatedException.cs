using System;
using System.Collections.Generic;

namespace ReceiptGenerator.BusinessLogic.Exceptions
{
    public abstract class BaseReceiptGeneratorAggreagatedException : AggregateException
    {
        public BaseReceiptGeneratorAggreagatedException(List<BaseReceiptGeneratorException> innerExceptions) : base(innerExceptions)
        {

        }
    }
}
