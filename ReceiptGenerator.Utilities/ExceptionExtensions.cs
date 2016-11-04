using System;
using System.Text;

namespace ReceiptGenerator.Utilities
{
    public static class ExceptionExtensions
    {

        public static string GetFullExceptionMessage(this Exception exception)
        {

            StringBuilder sb = new StringBuilder();

            int exceptionLevel = 1;
            while (exception != null)
            {
                sb.AppendLine(
                    string.Format(
                        "Exception type - '{0}'.{1}Exception level - '{2}'.{1}Exception Message - '{3}'.{1}Exception stack - '{4}'.",
                    exception.GetType().ToString(), Environment.NewLine, exceptionLevel, exception.Message, exception.StackTrace));

                exception = exception.InnerException;

                exceptionLevel++;
            }

            return sb.ToString();
        }
    }
}
