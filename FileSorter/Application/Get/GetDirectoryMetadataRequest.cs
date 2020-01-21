using System;
using DirectoryCrawler.Model;
using MediatR;

namespace FileSorter.Application.Get
{
    public class GetDirectoryMetadataRequest : IRequest<DirectoryStructure>
    {
        public GetDirectoryMetadataRequest(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public string Path { get; }
    }
}