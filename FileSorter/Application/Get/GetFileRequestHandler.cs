using MediatR;
using Microsoft.Extensions.FileProviders;
using System;

namespace FileSorter.Application.Get
{
    public class GetFileRequestHandler : RequestHandler<GetFileRequest, GetFileResponse>
    {
        private readonly Settings settings;
        private readonly IFileProvider fileProvider;

        public GetFileRequestHandler(Settings settings, IFileProvider fileProvider)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
        }
        protected override GetFileResponse Handle(GetFileRequest request)
        {
            var fileInfo = this.fileProvider.GetFileInfo(request.Path);
            return new GetFileResponse(fileInfo.CreateReadStream(), fileInfo.LastModified, "application/octet-stream");
        }
    }

}
