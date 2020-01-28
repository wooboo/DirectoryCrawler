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
        private readonly string metaFileName;
        private readonly PathUtil _path;

        public Crawler(string rootPath, string dirrcFileName = ".dirrc.json", string metaFileName = ".meta.json")
        {
            _rootPath = rootPath;
            _dirrcFileName = dirrcFileName;
            this.metaFileName = metaFileName;
            _path = new PathUtil(_rootPath);
        }
        public DirectoryStructure Build(string path)
        {
            var dir = new DirectoryEx(_rootPath);
            var rootMeta = this.LoadMeta(dir, this.metaFileName);
            var meta = rootMeta;
            DirectoryEx directory = dir;
            foreach (var item in dir.WalkDown(path))
            {
                Meta subMeta = this.LoadMeta(item, this.metaFileName);
                meta = meta.Merge(subMeta, item);
                directory = item;
            }
            var dirrc = this.LoadMeta(directory, this._dirrcFileName);

            return this.Crawl(meta, dirrc, directory);
        }
        private Meta LoadMeta(DirectoryEx directory, string fileName)
        {
            if (directory.TryGetFile(fileName, out var dirrc))
            {
                return dirrc!.GetFromJson<Meta>();
            }

            return new Meta();
        }
        public DirectoryStructure Crawl(Meta meta, Meta dirrc, DirectoryEx directory)
        {
            var merged = meta.Clone();
            merged.Merge(dirrc);

            var files = directory.GetFiles()
                .Select(f => new FileStructure(f, merged.GetFileProperties(f.Name)
                .Merge(dirrc.GetFileProperties(f.Name))))
                .ToList();


            var directories = directory.GetDirectories()
                .Select(item => new
                {
                    item,
                    subMeta = merged.Merge(this.LoadMeta(item, this.metaFileName), item),
                    deeprc = dirrc.Merge(item)
                })
                .Where(o => o.deeprc != null)
                .Select((x) => this.Crawl(x.subMeta, x.deeprc!, x.item))
                .ToList();

            directories.AddRange(merged.Virtuals
                .Select(kvp =>
                {
                    var item = directory.GetNestedDirectory(kvp.Key);
                    return new
                    {
                        kvp.Key,
                        deeprc = kvp.Value ?? new Meta(),
                        item,
                        subMeta = merged.Merge(this.LoadMeta(item, this.metaFileName), item),

                    };
                })
                .Select((x) => this.Crawl(x.subMeta, x.deeprc!, x.item))
            );

            return new DirectoryStructure(directory, files, directories, merged.GetProperties());
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