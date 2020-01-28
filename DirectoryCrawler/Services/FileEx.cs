using Newtonsoft.Json;
using System.IO;

namespace DirectoryCrawler.Services
{
    public class FileEx
    {
        public FileEx(DirectoryEx parent, string name)
        {
            this.Parent = parent;
            this.Name = name;
            this.Path = System.IO.Path.Combine(parent.Path, name);
            this.FullPath = System.IO.Path.Combine(parent.FullPath, name);
            this.root = parent.Root;
            this.UrlPath = this.Path.Replace(System.IO.Path.DirectorySeparatorChar, '/');
        }

        public string Name { get; }

        public string Path { get; }

        public string FullPath { get; }

        private DirectoryEx root;

        public string UrlPath { get; }

        public DirectoryEx Parent { get; }

        public string GetAllText()
        {
            return File.ReadAllText(this.FullPath);
        }

        public T GetFromJson<T>()
        {
            return JsonConvert.DeserializeObject<T>(this.GetAllText());
        }

        public override string ToString()
        {
            return this.Path;
        }
    }
}