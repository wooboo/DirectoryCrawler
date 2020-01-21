using MediatR;

namespace FileSorter.Application.Get
{
    public class GetPathInfoRequest : IRequest<PathInfoResponse>
    {
        public GetPathInfoRequest(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
