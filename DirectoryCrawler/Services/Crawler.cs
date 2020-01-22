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
            var rootMeta = this.LoadMeta(dir);
            var meta = rootMeta;
            DirectoryEx directory = dir;
            foreach (var item in dir.WalkDown(path))
            {
                Meta subMeta = this.LoadMeta(item);
                meta = meta.Merge(subMeta, item);
                directory = item;
            }

            return this.Crawl(meta, directory);
        }
        private Meta LoadMeta(DirectoryEx directory)
        {
            if (directory.TryGetFile(_dirrcFileName, out var dirrc))
            {
                return dirrc!.GetFromJson<Meta>();
            }

            return new Meta();
        }
        public DirectoryStructure Crawl(Meta meta, DirectoryEx directory)
        {
            var files = directory.GetFiles().Where(o => o.Name != _dirrcFileName).Select(f =>
            {
                return new FileStructure(f, meta.GetFileProperties(f.Name));
            }).ToList();

            IEnumerable<DirectoryStructure> directories = null;

            if (meta.Terminate != true)
            {
                directories = directory.GetDirectories().Select(item =>
                {
                    var subMeta = this.LoadMeta(item);
                    return this.Crawl(meta.Merge(subMeta, item), item);
                }).ToList();
            }
            return new DirectoryStructure(directory, files, directories, meta.GetProperties());
        }

        //private (Dictionary<string, FilePropertiesSet> filePropsDictionary, DirectoryPropertiesSet directoryProps)
        //    GetProps(string startPath, string path)
        //{
        //    var dirrc = Path.Combine(startPath, path, _dirrcFileName);
        //    Dictionary<string, FilePropertiesSet> filePropsDictionary = null;
        //    DirectoryPropertiesSet directoryProps = null;
        //    if (File.Exists(dirrc))
        //    {
        //        Meta directoryMeta = JsonConvert.DeserializeObject<Meta>(File.ReadAllText(dirrc));
        //        filePropsDictionary = new Dictionary<string, FilePropertiesSet>();
        //        foreach (var directoryMetaFile in directoryMeta.Files)
        //        {
        //            filePropsDictionary[directoryMetaFile.Key] = new FilePropertiesSet(directoryMetaFile.Value);
        //        }
        //        directoryProps = new DirectoryPropertiesSet(directoryMeta);
        //        foreach (var directoryMetaBase in directoryMeta.Bases)
        //        {
        //            if (_path.IsRoot(path, directoryMetaBase.Key, startPath))
        //            {
        //                directoryProps = directoryProps.Merge(directoryMetaBase.Value);

        //                foreach (var (fileName, fileOverrides) in directoryMetaBase.Value.Files)
        //                {
        //                    filePropsDictionary[fileName].Merge(fileOverrides);
        //                }
        //            }
        //        }
        //    }
        //    return (filePropsDictionary, directoryProps);
        //}
    }
}