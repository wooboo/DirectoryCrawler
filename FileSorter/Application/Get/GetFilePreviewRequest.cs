using MediatR;
using System;

namespace FileSorter.Application.Get
{
    public class GetFilePreviewRequest : IRequest<GetFileResponse>
    {
        public GetFilePreviewRequest(string path, int? width, int? height)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Width = width;
            Height = height;
        }

        public string Path { get; }
        public int? Width { get; }
        public int? Height { get; }
    }

}
