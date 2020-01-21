using System.Collections.Generic;

namespace DirectoryCrawler.Model
{
    public class DirectoryMetadata
    {
        public DirectoryMetadata(DirectoryPropertiesSet directoryProperties, IDictionary<string, FilePropertiesSet> filesProperties)
        {
            DirectoryProperties = directoryProperties;
            FilesProperties = filesProperties;
        }

        public DirectoryPropertiesSet DirectoryProperties { get; }
        public IDictionary<string, FilePropertiesSet> FilesProperties { get; }
    }
}