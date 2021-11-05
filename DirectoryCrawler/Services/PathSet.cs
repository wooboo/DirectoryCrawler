namespace DirectoryCrawler.Services
{
    public class PathSet
    {
        public PathSet(string name, string relativePath, string fullPath)
        {
            this.Name = name;
            this.RelativePath = relativePath;
            this.FullPath = fullPath;
        }
        public string Name { get; }
        public string RelativePath { get; }
        public string FullPath { get; }
    }
}