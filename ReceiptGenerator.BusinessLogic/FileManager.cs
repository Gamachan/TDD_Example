using System.IO;
using ReceiptGenerator.Contracts;
using ReceiptGenerator.Utilities;

namespace ReceiptGenerator.BusinessLogic
{
    public class FileManager : BaseManager, IFileManager
    { 
        #region Public Methods

        /// <summary>
        /// Loading data from input file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Returns data from file.</returns>
        public string LoadInputFromResourceFile(string fileName)
        {
            Guard.IsNotNullOrEmptyString(fileName, nameof(fileName));

            string filePath = $"{resourcePath}/Inputs//{fileName}";

            if (!File.Exists(filePath))
            {
                return null;
            }

            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Loading data from output file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Returns data from file.</returns>
        public string LoadOutputFromResourceFile(string fileName)
        {
            Guard.IsNotNullOrEmptyString(fileName, nameof(fileName));

            string filePath = $"{resourcePath}//Outputs//{fileName}";

            if (!File.Exists(filePath))
            {
                return null;
            }

            return File.ReadAllText(filePath);
        } 

        #endregion
    }
}
