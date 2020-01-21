using FileSorter.Controllers;
using MediatR;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace FileSorter.Application.Get
{
    public class GetFilePreviewRequestHandler : RequestHandler<GetFilePreviewRequest, GetFileResponse>
    {
        private readonly Settings settings;
        private readonly IFileProvider fileProvider;

        public GetFilePreviewRequestHandler(Settings settings, IFileProvider fileProvider)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
        }
        protected override GetFileResponse Handle(GetFilePreviewRequest request)
        {
            var fileInfo = this.fileProvider.GetFileInfo(request.Path);
            var stream = new MemoryStream();
            new ThumbnailGenerator().Resize(fileInfo, request.Width, request.Height, stream);
            stream.Seek(0, SeekOrigin.Begin);

            return new GetFileResponse(fileInfo.CreateReadStream(), fileInfo.LastModified, "application/octet-stream");
        }
    }

}
