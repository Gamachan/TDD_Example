namespace ReceiptGenerator.Contracts
{
    public interface ICategoryManager
    {
        string GetItemCategoryName(string itemName);
        bool IsCategoryExempt(string categoryName);
    }
}
