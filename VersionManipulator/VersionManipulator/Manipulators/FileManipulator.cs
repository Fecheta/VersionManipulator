namespace VersionManipulator.Manipulators
{
    public class FileManipulator
    {
        public string ReadVersionFromFile(string path)
        {
            try
            {
                var fileContent = File.ReadAllText(path);
                return fileContent;
            }
            catch
            {
                throw;
            }
        }

        public void WriteVersionFromFile(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
            }
            catch
            {
                throw;
            }
        }
    }
}
