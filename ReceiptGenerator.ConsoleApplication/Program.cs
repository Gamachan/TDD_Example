using ReceiptGenerator.Utilities;
using System;

namespace ReceiptGenerator.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application start.");
            try
            {
                var receiptGeneratorHandler = new ReceiptGeneratorHandler();
                receiptGeneratorHandler.Run();

            }
            catch (Exception ex)
            {
               Console.WriteLine($"Error: {ex.GetFullExceptionMessage()}");
            }
          
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
    }
}
