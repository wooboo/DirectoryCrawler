using MediatR;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileSorter.Application.Move
{
    public class MoveRequestHandler : RequestHandler<MoveRequest>
    {
        private readonly Settings settings;
        private readonly IFileProvider fileProvider;

        public MoveRequestHandler(Settings settings, IFileProvider fileProvider)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
        }
        protected override void Handle(MoveRequest request)
        {
            var fileInfos = request.Paths
                .Select(o => this.settings.GetPhisicalPath(o))
                .Select(o => new
                {
                    FileExists = File.Exists(o),
                    DirectoryExists = Directory.Exists(o),
                    Path = o,
                    FileName = Path.GetFileName(o)
                });
            var fullDestinationPath = this.settings.GetPhisicalPath(request.Destination);
            if (!File.Exists(fullDestinationPath) && !Directory.Exists(fullDestinationPath))
            {
                Directory.CreateDirectory(fullDestinationPath);
            }
            foreach (var item in fileInfos)
            {
                var destPath = Path.Combine(fullDestinationPath, item.FileName);
                if (File.Exists(fullDestinationPath))
                {
                    destPath = fullDestinationPath;
                }
                this.Move(item.Path, destPath);
            }
        }

        private void Move(string path, string dest)
        {
            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(dest);

                foreach (var item in Directory.GetFileSystemEntries(path))
                {
                    var destPath = Path.Combine(dest, Path.GetFileName(item));
                    if (File.Exists(dest))
                    {
                        destPath = dest;
                    }
                    this.Move(item, destPath);
                }
            }
            else if (File.Exists(path))
            {
                if (File.Exists(dest) || Directory.Exists(dest))
                {
                    dest = IncrementFile(dest);
                }
                File.Move(path, dest);
            }
        }
        Regex incrementPart = new Regex("\\s\\(\\d+\\)$");
        private string IncrementFile(string path)
        {
            var directory = Path.GetDirectoryName(path);
            var fileName = Path.GetFileNameWithoutExtension(path);
            var extension = Path.GetExtension(path);

            fileName = this.incrementPart.Replace(fileName, "");
            var index = 1;
            string result = null;
            do
            {
                result = Path.Combine(directory, $"{fileName} ({index++})" + extension);
            } while (File.Exists(result) || Directory.Exists(result));

            return result;

        }
    }
}
