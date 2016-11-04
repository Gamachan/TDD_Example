namespace ReceiptGenerator.BusinessLogic.Exceptions
{
    public class ResourceNotFoundException : BaseReceiptGeneratorException
    {
        public string ResourceName { get; }

        public string ResourcePath { get; }

        public ResourceNotFoundException(string resourceName,string resourcePath, string message) : base(message)
        {
            ResourceName = resourceName;
            ResourcePath = resourcePath;
        }
    }
}
