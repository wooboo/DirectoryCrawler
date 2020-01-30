using System;
using System.Collections.Generic;
using System.Linq;
using DirectoryCrawler.Services;

namespace DirectoryCrawler.Model
{
    public class DirectoryStructure
    {
        private readonly DirectoryEx directory;

        public DirectoryStructure(DirectoryEx directory, IEnumerable<FileStructure> files, IEnumerable<DirectoryStructure> directories, IDictionary<string, object> properties)
        {
            this.Files = files?.OrderBy(o => o.Name)?.ToDictionary(o => o.Name);
            this.Directories = directories?.OrderBy(o => o.Name)?.ToDictionary(o => o.Name);
            this.directory = directory;
            this.Properties = properties;
        }

        public string Name => this.directory.Name;
        public string Path => this.directory.Path;
        public string UrlPath => this.directory.UrlPath;
        public string FullPath => this.directory.FullPath;
        public DateTime CreationTime => this.directory.CreationTime;
        public DateTime LastAccessTime => this.directory.LastAccessTime;
        public DateTime LastWriteTime => this.directory.LastWriteTime;
        public IDictionary<string, DirectoryStructure> Directories { get; set; }
        public IDictionary<string, object> Properties { get; set; }
        public IDictionary<string, FileStructure> Files { get; set; }
    }
}