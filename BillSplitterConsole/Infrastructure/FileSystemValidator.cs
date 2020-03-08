using System.IO;

namespace BillSplitterConsole.Infrastructure
{
    internal class FileSystemValidator
    {
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public void EnsureFileDoesNotExist(string filePath)
        {
            if (FileExists(filePath)) File.Delete(filePath);
        }
    }
}