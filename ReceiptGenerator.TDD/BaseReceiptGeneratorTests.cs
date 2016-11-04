using ReceiptGenerator.Contracts;
using ReceiptGenerator.BusinessLogic;

namespace ReceiptGenerator.TDD
{
    public class BaseReceiptGeneratorTests
    {
        private IFileManager fileManager { get;set;}

        protected IFileManager FileManager
        {
            get
            {
                if (fileManager == null)
                {
                    fileManager = new FileManager();
                }
                return fileManager; 
            }
        }
    }
}
