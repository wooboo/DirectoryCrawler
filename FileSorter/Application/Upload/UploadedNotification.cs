using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FileSorter.Application.Upload
{
    public class UploadedNotification : INotification
    {
        public UploadedNotification(string fileName)
        {
            this.FileName = fileName;
        }

        public string FileName { get; }
    }

    public class GenerateMetadata : INotificationHandler<UploadedNotification>
    {
        public Task Handle(UploadedNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}