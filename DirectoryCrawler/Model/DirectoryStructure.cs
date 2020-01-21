using System.Collections.Generic;
using System.Linq;
using DirectoryCrawler.Services;

namespace DirectoryCrawler.Model
{
    public class DirectoryStructure
    {
        public DirectoryStructure(PathSet pathSet, IEnumerable<FileStructure> files, IEnumerable<DirectoryStructure> directories, PropertiesSet properties)
        {
            this.Name = pathSet.Name;
            this.FullPath = pathSet.FullPath;
            this.RelativePath = pathSet.RelativePath;
            this.Files = files?.ToDictionary(o => o.Name);
            this.Directories = directories?.ToDictionary(o => o.Name);
            this.Properties = properties;
        }

        public string Name { get; set; }
        public string RelativePath { get; set; }
        public string FullPath { get; set; }
        public IDictionary<string, DirectoryStructure> Directories { get; set; }
        public PropertiesSet Properties { get; set; }
        public IDictionary<string, FileStructure> Files { get; set; }
    }
}