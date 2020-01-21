using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoryCrawler.Services
{
    public class DirectoryEx
    {
        private readonly string fullPath;
        private readonly string path;
        private readonly DirectoryEx root;

        public DirectoryEx(string path)
        {
            Parent = null;
            Name = $"{Path.DirectorySeparatorChar}";
            this.fullPath = path;
            this.path = this.Name;
            this.root = this;
        }
        public DirectoryEx(DirectoryEx parent, string name)
        {
            Parent = parent;
            Name = name;
            this.root = parent.root;
            this.fullPath = Path.Combine(parent.FullPath, name);
            this.path = Path.Combine(parent.path, name);
        }

        public DirectoryEx Parent { get; }
        public string Name { get; }
        public string FullPath => this.fullPath;

        public IEnumerable<DirectoryEx> GetDirectories()
        {
            return Directory.EnumerateDirectories(this.FullPath).Select(o => new DirectoryEx(this, Path.GetFileName((string)o)));
        }

        public IEnumerable<FileEx> GetFiles()
        {
            return Directory.EnumerateDirectories(this.FullPath).Select(o => new FileEx(this, Path.GetFileName(o)));
        }

        public bool TryGetFile(string filePath, out FileEx? file)
        {
            var parts = filePath.Split(Path.DirectorySeparatorChar);
            var dirs = parts[..^1];
            var fileName = parts[^1];
            DirectoryEx dir = this;
            if (dirs.Length > 0 && this.TryGetDirectory(Path.Combine(dirs), out var tmpDir))
            {
                dir = tmpDir!;
            }
            if (File.Exists(Path.Combine(this.fullPath, filePath)))
            {
                file = new FileEx(dir, fileName);
                return true;
            }
            file = null;
            return false;
        }

        public IEnumerable<DirectoryEx> WalkDown(string path)
        {
            var parts = path.Split(Path.DirectorySeparatorChar);
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
            if (Path.DirectorySeparatorChar.ToString().Equals(name) ||
                "".Equals(name))
            {
                directory = root;
                return true;
            }
            if (Directory.Exists(Path.Combine(this.FullPath, name)))
            {
                directory = new DirectoryEx(this, name);
                return true;
            }
            directory = null;
            return false;
        }
    }
}