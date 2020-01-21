using MediatR;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace FileSorter.Application.Get
{
    public class GetPathInfoRequestHandler : RequestHandler<GetPathInfoRequest, PathInfoResponse>
    {
        private readonly Settings settings;
        private readonly IFileProvider fileProvider;

        public GetPathInfoRequestHandler(Settings settings, IFileProvider fileProvider)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
        }
        protected override PathInfoResponse Handle(GetPathInfoRequest request)
        {
            var phisicalPath = this.settings.GetPhisicalPath(request.Path);
            if (Directory.Exists(phisicalPath))
            {
                return new PathInfoResponse(phisicalPath, true, true);
            }
            if (File.Exists(phisicalPath))
            {
                return new PathInfoResponse(phisicalPath, true, false);
            }

            return new PathInfoResponse(phisicalPath, false, false);
        }
    }
}
