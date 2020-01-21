using Newtonsoft.Json;
using System.IO;

namespace DirectoryCrawler.Services
{
    public class FileEx
    {
        private readonly DirectoryEx parent;

        public FileEx(DirectoryEx parent, string name)
        {
            this.parent = parent;
            Name = name;
            this.path = System.IO.Path.Combine(parent.FullPath, name);
        }

        public string Name { get; }

        public string Path => this.path;

        private readonly string path;

        public string GetAllText()
        {
            return File.ReadAllText(this.path);
        }

        public T GetFromJson<T>()
        {
            return JsonConvert.DeserializeObject<T>(this.GetAllText());
        }
    }
}