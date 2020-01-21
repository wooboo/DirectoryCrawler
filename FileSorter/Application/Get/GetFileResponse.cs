using System;
using System.IO;

namespace FileSorter.Application.Get
{
    public class GetFileResponse
    {
        public GetFileResponse(Stream stream, DateTimeOffset lastModified, string contentType)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            LastModified = lastModified;
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        }

        public Stream Stream { get; }
        public DateTimeOffset LastModified { get; }
        public string ContentType { get; }
    }

}
