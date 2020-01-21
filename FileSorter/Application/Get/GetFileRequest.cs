using MediatR;
using System;

namespace FileSorter.Application.Get
{
    public class GetFileRequest : IRequest<GetFileResponse>
    {
        public GetFileRequest(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public string Path { get; }
    }
}
