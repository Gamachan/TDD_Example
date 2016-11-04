using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReceiptGenerator.Contracts;
using ReceiptGenerator.BusinessLogic;

namespace ReceiptGenerator.TDD
{
    [TestClass]
    public class ReceiptGeneratorTests : BaseReceiptGeneratorTests
    {

        private IReceiptManager receiptManager;

        [TestInitialize]
        public void Setup()
        {
            receiptManager = new ReceiptManager();
        }

        [TestMethod]
        public void Input1_Output1()
        {
            // Setup
            string input_1 = FileManager.LoadInputFromResourceFile("Input_1.txt");
            string output_1 = FileManager.LoadOutputFromResourceFile("Output_1.txt");

            // Action
            string result = receiptManager.GenerateReceipt(input_1);

            // Assert
            Assert.AreEqual(output_1, result);
        }

        [TestMethod]
        public void Input2_Output2()
        {
            // Setup
            string input_2 = FileManager.LoadInputFromResourceFile("Input_2.txt");
            string output_2 = FileManager.LoadOutputFromResourceFile("Output_2.txt");

            // Action
            string result = receiptManager.GenerateReceipt(input_2);

            // Assert
            Assert.AreEqual(output_2, result);
        }

        [TestMethod]
        public void Input3_Output3()
        {
            // Setup
            string input_3 = FileManager.LoadInputFromResourceFile("Input_3.txt");
            string output_3 = FileManager.LoadOutputFromResourceFile("Output_3.txt");

            // Action
            string result = receiptManager.GenerateReceipt(input_3);

            // Assert
            Assert.AreEqual(output_3, result);
        }

        [TestMethod]
        public void TestAllInputFiles()
        {
            string input;
            string output;

            int counter = 1;
            while (true)
            {
                // Setup
                input = FileManager.LoadInputFromResourceFile($"input_{counter}");
                output = FileManager.LoadOutputFromResourceFile($"output_{counter}");

                if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(output))
                {
                    return;
                }

                // Action
                string result = receiptManager.GenerateReceipt(input);

                // Assert
                Assert.AreEqual(output, result);

                counter++;
            }
        }
    }
}
