using System;
using System.Collections.Generic;
using ReceiptGenerator.Contracts;
using ReceiptGenerator.Utilities;
using ReceiptGenerator.BusinessLogic;

namespace ReceiptGenerator.ConsoleApplication
{
    public class ReceiptGeneratorHandler
    {
        internal void Run()
        {
            IFileManager fileManager = new FileManager();
            IReceiptManager receiptManager = new ReceiptManager();

            List<string> inputs = new List<string>();

            int counter = 1;

            Console.WriteLine("Input");

            while (true)
            {
                string input = fileManager.LoadInputFromResourceFile($"input_{counter}.txt");

                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                Console.WriteLine($"{Constants.SingleIndentation}Shopping Basket {counter}:");
                PrintLinesWithIndentation(input);
                inputs.Add(input);
                counter++;
            }

            counter = 1;

            Console.WriteLine("Output");

            foreach (var input in inputs)
            {
                string result = receiptManager.GenerateReceipt(input);

                Console.WriteLine($"{Constants.SingleIndentation}Output {counter}:");
                PrintLinesWithIndentation(result);
                counter++;
            }
        }

        private void PrintLinesWithIndentation(string text)
        {
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                Console.WriteLine($"{Constants.DoubleIndentation}{line}");
            }
        }
    }
}

