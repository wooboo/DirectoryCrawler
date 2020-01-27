using DirectoryCrawler.Model;
using DirectoryCrawler.Services;
using MediatR;
using System.IO;

namespace FileSorter.Application.Get
{
    public class GetDirectoryMetadataRequestHandler : RequestHandler<GetDirectoryMetadataRequest, DirectoryStructure>
    {
        private readonly Crawler crawler;

        public GetDirectoryMetadataRequestHandler(Settings settings)
        {
            this.crawler = new Crawler(settings.FilesLocation);
        }
        protected override DirectoryStructure Handle(GetDirectoryMetadataRequest request)
        {
            return this.crawler.Build(request.Path.Replace('/', Path.DirectorySeparatorChar));
        }
    }
}