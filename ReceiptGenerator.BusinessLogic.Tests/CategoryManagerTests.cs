using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReceiptGenerator.Contracts;

namespace ReceiptGenerator.BusinessLogic.Tests
{
    [TestClass]
    public class CategoryManagerTests
    {

        private ICategoryManager categoryManager;

        [TestInitialize]
        public void Setup()
        {
            categoryManager = new CategoryManager();
        }

        [TestMethod]
        public void GetItemCategoryName_NullArgument_ThrowsArgumentException()
        {
            try
            {
                categoryManager.GetItemCategoryName(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void GetItemCategoryName_EmptyStringArgument_ThrowsArgumentException()
        {
            try
            {
                categoryManager.GetItemCategoryName(string.Empty);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void GetItemCategoryName_FoodCategoryItem_ReturnsFoodCategoryName()
        {
            string categoryName = categoryManager.GetItemCategoryName("box of chocolates");

            Assert.AreEqual("food", categoryName);
        }

        [TestMethod]
        public void GetItemCategoryName_FoodCategoryItemUpperCase_ReturnsFoodCategoryName()
        {
            string categoryName = categoryManager.GetItemCategoryName("BOX OF CHOCOLATES");

            Assert.AreEqual("food", categoryName);
        }

        [TestMethod]
        public void GetItemCategoryName_UncatalogItem_ReturnsNull()
        {
            string categoryName = categoryManager.GetItemCategoryName("box of dark chocolates");

            Assert.IsNull(categoryName);
        }

        [TestMethod]
        public void IsCategoryExpemt_NullArgument_ReturnsFalse()
        {
            Assert.IsFalse(categoryManager.IsCategoryExempt(null));
        }

        [TestMethod]
        public void IsCategoryExpemt_EmptyStringArgument_ReturnsFalse()
        {
            Assert.IsFalse(categoryManager.IsCategoryExempt(string.Empty));
        }

        [TestMethod]
        public void IsCategoryExpemt_ExempetedCategory_ReturnsTrue()
        {
            Assert.IsTrue(categoryManager.IsCategoryExempt("food"));
        }

        [TestMethod]
        public void IsCategoryExpemt_ExempetedCategoryUppercase_ReturnsTrue()
        {
            Assert.IsTrue(categoryManager.IsCategoryExempt("FOOD"));
        }

        [TestMethod]
        public void IsCategoryExpemt_NonExempetedCategory_ReturnsFalse()
        {
            Assert.IsFalse(categoryManager.IsCategoryExempt("Magazines"));
        }
    }
}
