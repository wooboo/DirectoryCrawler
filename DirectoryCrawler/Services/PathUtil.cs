using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoryCrawler.Services
{
    public class PathUtil
    {
        private readonly string _rootPath;

        public PathUtil(string rootPath)
        {
            _rootPath = Path.GetFullPath(rootPath);
        }
        public PathSet GetPaths(string path)
        {
            var path2 = Path.GetRelativePath(_rootPath, Path.GetFullPath(Path.Combine(_rootPath, path)));
            return new PathSet(
                Path.GetFileName(path2),
                path2.Replace('\\', '/'),
                Path.GetFullPath(Path.Combine(_rootPath, path2)).Replace('\\', '/')
            );
        }

        public IEnumerable<PathSet> GetDirectories(string relativePath)
        {
            return Directory.GetDirectories(Path.Combine(_rootPath, relativePath))
                .Select(o => Path.GetRelativePath(_rootPath, o).Replace('\\', '/')).Select(this.GetPaths);
        }

        public IEnumerable<PathSet> GetFiles(string relativePath)
        {
            return Directory.GetFiles(Path.Combine(_rootPath, relativePath))
                .Select(o => Path.GetRelativePath(_rootPath, o).Replace('\\', '/')).Select(this.GetPaths);
        }
        public bool IsRoot(string part1, string part2, string part3)
        {
            if (part2.StartsWith("/"))
            {
                return Path.GetFullPath(Path.Combine(_rootPath, part2.TrimStart('/'))).Replace('\\', '/') == Path.GetFullPath(Path.Combine(_rootPath, part3)).Replace('\\', '/');
            }
            else
            {
                var full = Path.Combine(_rootPath, part1);
                return Path.GetFullPath(Path.Combine(full, part2)).Replace('\\', '/') == Path.GetFullPath(Path.Combine(_rootPath, part3)).Replace('\\', '/');
            }
        }
    }
}