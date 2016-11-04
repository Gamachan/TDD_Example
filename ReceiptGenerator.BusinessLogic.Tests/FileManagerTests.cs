using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ReceiptGenerator.Contracts;

namespace ReceiptGenerator.BusinessLogic.Tests
{
    [TestClass]
    public class FileManagerTests
    {
        private IFileManager fileManager;

        [TestInitialize]
        public void Setup()
        {
            fileManager = new FileManager();
        }

        [TestMethod]
        public void LoadInputFromResourceFile_NullArgument_ThrowsArgumentException()
        {
            try
            {
                fileManager.LoadInputFromResourceFile(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void LoadInputFromResourceFile_EmptyStringArgument_ThrowsArgumentException()
        {
            try
            {
                fileManager.LoadInputFromResourceFile(string.Empty);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void LoadOutputFromResourceFile_NullArgument_ThrowsArgumentException()
        {
            try
            {
                fileManager.LoadOutputFromResourceFile(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void LoadOutputFromResourceFile_EmptyStringArgument_ThrowsArgumentException()
        {
            try
            {
                fileManager.LoadOutputFromResourceFile(string.Empty);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void LoadOutputFromResourceFile_NonExistingFileName_ReturnsNull()
        {
            Assert.IsNull(fileManager.LoadOutputFromResourceFile("non_existing_file.txt"));
        }

        [TestMethod]
        public void LoadInputFromResourceFile_NonExistingFileName_ReturnsNull()
        {
            Assert.IsNull(fileManager.LoadInputFromResourceFile("non_existing_file.txt"));
        }

        [TestMethod]
        public void LoadOutputFromResourceFile_ExistingFileName_ReturnsData()
        {
            Assert.IsNotNull(fileManager.LoadOutputFromResourceFile("output_1.txt"));
        }

        [TestMethod]
        public void LoadInputFromResourceFile_ExistingFileName_ReturnsData()
        {
            Assert.IsNotNull(fileManager.LoadInputFromResourceFile("input_1.txt"));
        }
    }
}
