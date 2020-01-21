using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace FileSorter.Application.Upload
{
    public class UploadRequest : IRequest
    {
        public UploadRequest(string path, IFormFile[] files)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Files = files ?? throw new ArgumentNullException(nameof(files));
        }

        public string Path { get; }
        public IFormFile[] Files { get; }
    }
}
