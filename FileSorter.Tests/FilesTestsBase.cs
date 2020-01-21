using System;
using System.IO;
using Xunit;

namespace FileSorter.Tests
{
    public class FilesTestsBase : IDisposable
    {
        protected Settings settings;

        public FilesTestsBase()
        {
            var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            this.settings = new Settings
            {
                FilesLocation = path
            };
            Directory.CreateDirectory(path);
        }

        public void Dispose()
        {
            Directory.Delete(this.settings.FilesLocation, true);
        }
        public void CreateDirectory(string path)
        {
            var directoryPath = Path.Combine(this.settings.FilesLocation, path);
            Directory.CreateDirectory(directoryPath);
        }
        public void CreateFile(string path, string content = null)
        {
            var filePath = Path.Combine(this.settings.FilesLocation, path);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, content ?? Guid.NewGuid().ToString());
        }

        public FileSystemAssert AssertPath(string path)
        {
            return new FileSystemAssert(this.settings.FilesLocation, path);
        }
        public class FileSystemAssert
        {
            private readonly string path;
            private readonly bool fileExists;
            private readonly bool directoryExists;

            public FileSystemAssert(string basePath, string path)
            {
                this.path = path;

                this.fileExists = File.Exists(Path.Combine(basePath, path));
                this.directoryExists = Directory.Exists(Path.Combine(basePath, path));
            }
            public void Exists()
            {
                Assert.True(this.fileExists || this.directoryExists, $"{this.path} does not exists");
            }
            public void DirectoryExists()
            {
                Assert.True(this.directoryExists, $"Directory {this.path} does not exists");
            }
            public void FileExists()
            {
                Assert.True(this.fileExists, $"File {this.path} does not exists");
            }
        }
    }
}
