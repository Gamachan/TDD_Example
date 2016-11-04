namespace ReceiptGenerator.Contracts
{
    public interface IReceiptManager
    {
        string GenerateReceipt(string rawData);
    }
}
