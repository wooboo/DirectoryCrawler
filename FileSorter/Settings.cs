using System.IO;

namespace FileSorter
{
    public class Settings
    {
        public string FilesLocation { get; set; }


        public string GetPhisicalPath(string path)
        {
            return Path.Combine(this.FilesLocation, (path ?? "").Replace('/', Path.DirectorySeparatorChar));
        }
    }
}
