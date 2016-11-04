using System;

namespace ReceiptGenerator.Utilities
{
    public static class Guard
    {

        public static void IsNotNull(object obj, string nameOfObject)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameOfObject);
            }
        }

        public static void IsNotNullOrEmptyString(string text, string nameOfString)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException(nameOfString);
            }
        }
    }
}
