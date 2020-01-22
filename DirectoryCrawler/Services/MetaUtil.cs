//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using DirectoryCrawler.Model;
//using Newtonsoft.Json;

//namespace DirectoryCrawler.Services
//{
//    public class MetaUtil
//    {
//        public MetaUtil(string path, params Meta[] metas)
//        {
//            Path = path.Replace("\\", "/");
//            Metas = metas;
//        }

//        public MetaUtil(string path, MetaUtil parent, params Meta[] metas)
//            : this(System.IO.Path.Combine(parent.Path, path), parent.Metas.Concat(metas).ToArray())
//        {

//        }

//        public string Path { get; }
//        public Meta[] Metas { get; }

//        public static Meta Load(string path)
//        {
//            if (File.Exists(path))
//            {
//                return JsonConvert.DeserializeObject<Meta>(File.ReadAllText(path));
//            }
//            return new Meta();
//        }

//        public static MetaUtil Create(string basePath, string path)
//        {
//            var dirName = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(path))!;
//            return new MetaUtil(dirName, Load(path));
//        }
//        public MetaUtil Add(string path)
//        {
//            var dirName = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(path))!;
//            return new MetaUtil(dirName, this, Load(path));
//        }
//        public Meta[] GetDirectoryMetas(string startPath)
//        {
//            foreach (var meta in Metas)
//            {
//            }
//            return null;
//        }
//        public DirectoryMetadata GetProperties(string startPath)
//        {
//            Dictionary<string, FilePropertiesSet> filePropsDictionary = new Dictionary<string, FilePropertiesSet>();
//            DirectoryPropertiesSet directoryProps = null;

//            var directoryMetas = this.GetDirectoryMetas(startPath);

//            foreach (var directoryMeta in directoryMetas)
//            {
//                foreach (var directoryMetaFile in directoryMeta.Files)
//                {
//                    filePropsDictionary[directoryMetaFile.Key] = new FilePropertiesSet(directoryMetaFile.Value);
//                }
//                directoryProps = new DirectoryPropertiesSet(directoryMeta);
//                foreach (var directoryMetaBase in directoryMeta.Bases)
//                {
//                    //if (_path.IsRoot(path, directoryMetaBase.Key, startPath))
//                    //{
//                    //    directoryProps = directoryProps.Merge(directoryMetaBase.Value);

//                    //    foreach (var (fileName, fileOverrides) in directoryMetaBase.Value.Files)
//                    //    {
//                    //        filePropsDictionary[fileName].Merge(fileOverrides);
//                    //    }
//                    //}
//                }
//            }
//            return new DirectoryMetadata(directoryProps, filePropsDictionary);
//        }
//    }
//}