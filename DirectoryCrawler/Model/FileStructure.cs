using DirectoryCrawler.Services;
using System.Collections.Generic;

namespace DirectoryCrawler.Model
{
    public class FileStructure
    {
        public FileStructure(FileEx pathSet, IDictionary<string, object> properties)
        {
            this.Properties = properties;
            this.Name = pathSet.Name;
            this.Path = pathSet.Path;
            this.FullPath = pathSet.FullPath;
        }

        public string Path { get; set; }

        public IDictionary<string, object> Properties { get; set; }

        public string Name { get; set; }

        public string FullPath { get; set; }
    }
}