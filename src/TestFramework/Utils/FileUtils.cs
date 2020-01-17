using System.IO;

namespace TestFramework.Utils
{
    public class FileUtils
    {
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void RenameFile(string filePath, string sourceFileName, string targetFileName)
        {
            File.Move(Path.Combine(filePath, sourceFileName), Path.Combine(filePath, targetFileName));
        }
    }
}