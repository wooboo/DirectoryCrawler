namespace FileSorter.Application.Get
{
    public class PathInfoResponse
    {
        public PathInfoResponse(string fullPath, bool exists, bool isDirectory)
        {
            FullPath = fullPath ?? throw new System.ArgumentNullException(nameof(fullPath));
            Exists = exists;
            IsDirectory = isDirectory;
        }

        public string FullPath { get; }
        public bool Exists { get; }
        public bool IsDirectory { get; }
    }
}