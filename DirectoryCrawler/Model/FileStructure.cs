using DirectoryCrawler.Services;
using System.Collections.Generic;

namespace DirectoryCrawler.Model
{
    public class FileStructure
    {
        public FileStructure(PathSet pathSet, FilePropertiesSet properties)
        {
            this.Properties = properties;
            this.Name = pathSet.Name;
            this.FullPath = pathSet.FullPath;
            this.RelativePath = pathSet.RelativePath;
        }

        public string RelativePath { get; set; }

        public FilePropertiesSet Properties { get; set; }

        public string Name { get; set; }

        public string FullPath { get; set; }
    }
}