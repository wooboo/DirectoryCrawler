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
            this.UrlPath = pathSet.UrlPath;
        }

        public string Path { get; }

        public IDictionary<string, object> Properties { get; }

        public string Name { get; }

        public string FullPath { get; }

        public string UrlPath { get; }
    }
}