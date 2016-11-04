using System.IO;
using System.Collections.Generic;
using ReceiptGenerator.Contracts;
using ReceiptGenerator.Utilities;
using ReceiptGenerator.BusinessLogic.Exceptions;

namespace ReceiptGenerator.BusinessLogic
{
    public class CategoryManager : BaseManager, ICategoryManager
    {
        #region Private Members

        private IList<string> exemptedCategoryNames;
        private IDictionary<string, string> categoryItemsDictionary;

        #endregion

        #region Constructor 

        public CategoryManager()
        {
            LoadExemptedCategories();
            LoadCategoryItems();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get item category name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns>Returns category name.</returns>
        public string GetItemCategoryName(string itemName)
        {
            Guard.IsNotNullOrEmptyString(itemName, nameof(itemName));

            if (categoryItemsDictionary.ContainsKey(itemName.ToLower()))
            {
                return categoryItemsDictionary[itemName.ToLower()];
            }

            return null;
        }

        /// <summary>
        /// Check if an item belongs to exempt category.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <returns>Returns true if category is exempt.</returns>
        public bool IsCategoryExempt(string categoryName)
        {
            if (!string.IsNullOrEmpty(categoryName) && exemptedCategoryNames.Contains(categoryName.ToLower()))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Private Methods

        private void LoadCategoryItems()
        {
            categoryItemsDictionary = new Dictionary<string, string>();

            string categoryItemsDirectoryPath = Path.Combine(Constants.ResourcePathPrefix, $"Categories/{Constants.CatalogDirectoryName}");

            if (!Directory.Exists(categoryItemsDirectoryPath))
            {
                throw new ResourceNotFoundException(Constants.CatalogDirectoryName, categoryItemsDirectoryPath, "Category catalogs directory not found.");
            }

            DirectoryInfo dirInfo = new DirectoryInfo(categoryItemsDirectoryPath);

            foreach (var file in dirInfo.GetFiles("*.txt"))
            {
                string content = File.ReadAllText(file.FullName);
                string categoryName = Path.GetFileNameWithoutExtension(file.Name).ToLower();

                if (!string.IsNullOrEmpty(content))
                {
                    string[] items = content.Split(',');

                    foreach (var item in items)
                    {
                        string itemName = item.ToLower().Trim();
                        if (!categoryItemsDictionary.ContainsKey(itemName))
                        {
                            categoryItemsDictionary.Add(itemName, categoryName);
                        }
                        else
                        {
                            if (!categoryItemsDictionary[itemName].Equals(categoryName))
                            {
                                throw new ResourceDataConfilctException($"Item - '{itemName}' is associated with more than one category.");
                            }
                        }
                    }
                }
            }
        }

        private void LoadExemptedCategories()
        {
            exemptedCategoryNames = new List<string>();

            string exemptedCategoryFilePath = Path.Combine(Constants.ResourcePathPrefix, $"Categories/{Constants.ExemptedCategoryList}.txt");

            if (!File.Exists(exemptedCategoryFilePath))
            {
                throw new ResourceNotFoundException(Constants.ExemptedCategoryList, exemptedCategoryFilePath, "Exempted category list file not found.");
            }

            string categoriesList = File.ReadAllText(exemptedCategoryFilePath);

            string[] categories = categoriesList.Split(',');

            foreach (string category in categories)
            {
                exemptedCategoryNames.Add(category.ToLower().Trim());
            }
        }

        #endregion
    }
}
