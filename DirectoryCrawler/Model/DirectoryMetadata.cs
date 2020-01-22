using System.Collections.Generic;

namespace DirectoryCrawler.Model
{
    public class DirectoryMetadata
    {
        public DirectoryMetadata(IDictionary<string, object> directoryProperties, IDictionary<string, IDictionary<string, object>> filesProperties)
        {
            DirectoryProperties = directoryProperties;
            FilesProperties = filesProperties;
        }

        public IDictionary<string, object> DirectoryProperties { get; }
        public IDictionary<string, IDictionary<string, object>> FilesProperties { get; }
    }
}