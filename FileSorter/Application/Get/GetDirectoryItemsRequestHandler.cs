using MediatR;
using System.IO;
using System.Linq;

namespace FileSorter.Application.Get
{

    public class GetDirectoryItemsRequestHandler : RequestHandler<GetDirectoryItemsRequest, string[]>
    {
        private readonly Settings settings;

        public GetDirectoryItemsRequestHandler(Settings settings)
        {
            this.settings = settings;
        }
        protected override string[] Handle(GetDirectoryItemsRequest request)
        {
            var location = this.settings.GetPhisicalPath(request.Path);
            return Directory.GetFileSystemEntries(location).Select(o => Path.GetRelativePath(location, o)).ToArray();
        }
    }
}
