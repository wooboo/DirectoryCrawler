using MediatR;
using Microsoft.Extensions.FileProviders;
using System;

namespace FileSorter.Application.Get
{
    public class IsModifiedSinceRequest : IRequest<bool>
    {
        public IsModifiedSinceRequest(string path, DateTimeOffset dateTime)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            LastModifiedSince = dateTime;
        }

        public string Path { get; }
        public DateTimeOffset LastModifiedSince { get; }
    }
    public class IsModifiedSinceRequestHandler : RequestHandler<IsModifiedSinceRequest, bool>
    {
        private readonly Settings settings;
        private readonly IFileProvider fileProvider;

        public IsModifiedSinceRequestHandler(Settings settings, IFileProvider fileProvider)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
        }
        protected override bool Handle(IsModifiedSinceRequest request)
        {
            var fileInfo = fileProvider.GetFileInfo(request.Path);
            var lastModified = DateTimeOffset.Parse(fileInfo.LastModified.ToString("R"));
            lastModified.AddMilliseconds(-lastModified.Millisecond);
            return request.LastModifiedSince < lastModified;
        }
    }
}
