using DirectoryCrawler.Model;
using DirectoryCrawler.Services;
using MediatR;

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
            return this.crawler.Crawl(null, request.Path, request.Path);
        }
    }
}