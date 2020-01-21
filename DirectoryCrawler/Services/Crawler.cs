using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DirectoryCrawler.Model;
using Newtonsoft.Json;

namespace DirectoryCrawler.Services
{
    public class Crawler
    {
        private readonly string _rootPath;
        private readonly string _dirrcFileName;
        private readonly PathUtil _path;

        public Crawler(string rootPath, string dirrcFileName = ".dirrc.json")
        {
            _rootPath = rootPath;
            _dirrcFileName = dirrcFileName;
            _path = new PathUtil(_rootPath);
        }
        public DirectoryStructure Build(string path)
        {
            var dir = new DirectoryEx(_rootPath);
            var rootMeta = new Meta();
            if(dir.TryGetFile(_dirrcFileName, out var rootdirrc))
            {
                rootMeta = rootdirrc!.GetFromJson<Meta>();
            }
            var meta = rootMeta;
            foreach (var item in dir.WalkDown(path))
            {
                if (item.TryGetFile(_dirrcFileName, out var dirrc))
                {
                    var subMeta = dirrc!.GetFromJson<Meta>();
                    meta = meta.Merge(subMeta, item);
                }
            }

            return null;// this.Crawl(null, startPath, path);
        }
        public DirectoryStructure Crawl(MetaUtil metaUtil, string startPath, string path)
        {
            var pathSet = _path.GetPaths(path);

            var (filePropsDictionary, directoryProps) = GetProps(startPath, path);
            IEnumerable<FileStructure> files = _path.GetFiles(pathSet.RelativePath)
                .Where(o => o.Name != _dirrcFileName)
                .Select(o => new FileStructure(o, filePropsDictionary.GetValueOrDefault(o.Name)));
            IEnumerable<DirectoryStructure> directories = null;

            if (!directoryProps.Terminate)
            {
                directories = _path.GetDirectories(pathSet.RelativePath).Select(s => this.Crawl(null, startPath, s.RelativePath));
            }
            return new DirectoryStructure(pathSet, files, directories, directoryProps);
        }

        private (Dictionary<string, FilePropertiesSet> filePropsDictionary, DirectoryPropertiesSet directoryProps)
            GetProps(string startPath, string path)
        {
            var dirrc = Path.Combine(startPath, path, _dirrcFileName);
            Dictionary<string, FilePropertiesSet> filePropsDictionary = null;
            DirectoryPropertiesSet directoryProps = null;
            if (File.Exists(dirrc))
            {
                Meta directoryMeta = JsonConvert.DeserializeObject<Meta>(File.ReadAllText(dirrc));
                filePropsDictionary = new Dictionary<string, FilePropertiesSet>();
                foreach (var directoryMetaFile in directoryMeta.Files)
                {
                    filePropsDictionary[directoryMetaFile.Key] = new FilePropertiesSet(directoryMetaFile.Value);
                }
                directoryProps = new DirectoryPropertiesSet(directoryMeta);
                foreach (var directoryMetaBase in directoryMeta.Bases)
                {
                    if (_path.IsRoot(path, directoryMetaBase.Key, startPath))
                    {
                        directoryProps = directoryProps.Merge(directoryMetaBase.Value);

                        foreach (var (fileName, fileOverrides) in directoryMetaBase.Value.Files)
                        {
                            filePropsDictionary[fileName].Merge(fileOverrides);
                        }
                    }
                }
            }
            return (filePropsDictionary, directoryProps);
        }
    }
}