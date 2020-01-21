using MediatR;
using System;

namespace FileSorter.Application.Get
{
    public class GetDirectoryItemsRequest : IRequest<string[]>
    {
        public GetDirectoryItemsRequest(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public string Path { get; }
    }
}
