using System.Collections.Generic;
using System.Linq;
using DirectoryCrawler.Services;

namespace DirectoryCrawler.Model
{
    public class DirectoryStructure
    {
        public DirectoryStructure(DirectoryEx pathSet, IEnumerable<FileStructure> files, IEnumerable<DirectoryStructure> directories, IDictionary<string, object> properties)
        {
            this.Name = pathSet.Name;
            this.FullPath = pathSet.FullPath;
            this.Path = pathSet.Path;
            this.UrlPath = pathSet.UrlPath;
            this.Files = files?.ToDictionary(o => o.Name);
            this.Directories = directories?.ToDictionary(o => o.Name);
            this.Properties = properties;
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public string UrlPath { get; }
        public string FullPath { get; set; }
        public IDictionary<string, DirectoryStructure> Directories { get; set; }
        public IDictionary<string, object> Properties { get; set; }
        public IDictionary<string, FileStructure> Files { get; set; }
    }
}