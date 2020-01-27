using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoryCrawler.Services
{
    public class DirectoryEx
    {

        public DirectoryEx(string path)
        {
            this.Parent = null;
            this.Name = $"{System.IO.Path.DirectorySeparatorChar}";
            this.FullPath = path;
            this.Path = this.Name;
            this.Root = this;
            this.UrlPath = this.Path.Replace(System.IO.Path.DirectorySeparatorChar, '/');
        }
        public DirectoryEx(DirectoryEx parent, string name)
        {
            Parent = parent;
            Name = name;
            this.Root = parent.Root;
            this.FullPath = System.IO.Path.Combine(parent.FullPath, name);
            this.Path = System.IO.Path.Combine(parent.Path, name);
            this.UrlPath = this.Path.Replace(System.IO.Path.DirectorySeparatorChar, '/');
        }
        public DirectoryEx Root { get; }
        public DirectoryEx Parent { get; }
        public string Name { get; }
        public string FullPath { get; }
        public string Path { get; }
        public string UrlPath { get; }

        public IEnumerable<DirectoryEx> GetDirectories()
        {
            return Directory.EnumerateDirectories(this.FullPath).Select(o => new DirectoryEx(this, System.IO.Path.GetFileName((string)o)));
        }

        public IEnumerable<FileEx> GetFiles()
        {
            return Directory.EnumerateFiles(this.FullPath).Select(o => new FileEx(this, System.IO.Path.GetFileName(o)));
        }

        public bool TryGetFile(string filePath, out FileEx? file)
        {
            var parts = filePath.Split(System.IO.Path.DirectorySeparatorChar);
            var dirs = parts[..^1];
            var fileName = parts[^1];
            DirectoryEx dir = this;
            if (dirs.Length > 0 && this.TryGetDirectory(System.IO.Path.Combine(dirs), out var tmpDir))
            {
                dir = tmpDir!;
            }
            if (File.Exists(System.IO.Path.Combine(this.FullPath, filePath)))
            {
                file = new FileEx(dir, fileName);
                return true;
            }
            file = null;
            return false;
        }

        public IEnumerable<DirectoryEx> WalkDown(string path)
        {
            var parts = path.Split(System.IO.Path.DirectorySeparatorChar);
            var parent = this;
            foreach (var part in parts)
            {
                parent = parent.GetDirectory(part);
                yield return parent;
            }
        }
        public IEnumerable<DirectoryEx> WalkUp()
        {
            var parent = this;
            while (parent != null)
            {
                yield return parent;
                parent = parent.Parent;
            }
        }
        public DirectoryEx GetNestedDirectory(string relativePath)
        {
            return this.WalkDown(relativePath).Last();
        }

        public bool TryGetNestedDirectory(string relativePath, out DirectoryEx? directory)
        {
            try
            {
                directory = this.WalkDown(relativePath).Last();
                return true;
            }
            catch (NotFoundException)
            {
                directory = null;
                return false;
            }
        }

        public DirectoryEx GetDirectory(string name)
        {
            if (this.TryGetDirectory(name, out var directory))
            {
                return directory!;
            }

            throw new NotFoundException($"{name} not found");
        }

        public bool TryGetDirectory(string name, out DirectoryEx? directory)
        {
            if (".".Equals(name))
            {
                directory = this;
                return true;
            }
            if ("..".Equals(name))
            {
                directory = this.Parent;
                return directory != null;
            }
            if (System.IO.Path.DirectorySeparatorChar.ToString().Equals(name) ||
                "".Equals(name))
            {
                directory = Root;
                return true;
            }
            if (Directory.Exists(System.IO.Path.Combine(this.FullPath, name)))
            {
                directory = new DirectoryEx(this, name);
                return true;
            }
            directory = null;
            return false;
        }

        public override string ToString()
        {
            return this.Path;
        }
    }
}