namespace ReceiptGenerator.Contracts
{
    public interface IFileManager
    {
        string LoadInputFromResourceFile(string fileName);
        string LoadOutputFromResourceFile(string fileName);
    }
}
