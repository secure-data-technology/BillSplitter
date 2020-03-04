using System.IO;

namespace BillSplitterConsole.Infrastructure
{
    internal class FileSystemValidator
    {
        public bool FileExists(string _filePath)
        {
            return File.Exists(_filePath);
        }

        public void EnsureFileDoesNotExist(string _filePath)
        {
            if (FileExists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}
