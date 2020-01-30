using DirectoryCrawler.Services;
using System;
using System.Collections.Generic;

namespace DirectoryCrawler.Model
{
    public class FileStructure
    {
        private readonly FileEx file;

        public FileStructure(FileEx file, IDictionary<string, object> properties)
        {
            this.file = file;
            this.Properties = properties;

        }

        public IDictionary<string, object> Properties { get; }
        public string Name => this.file.Name;
        public string Path => this.file.Path;
        public string UrlPath => this.file.UrlPath;
        public string FullPath => this.file.FullPath;
        public DateTime CreationTime => this.file.CreationTime;
        public DateTime LastAccessTime => this.file.LastAccessTime;
        public DateTime LastWriteTime => this.file.LastWriteTime;
    }
}