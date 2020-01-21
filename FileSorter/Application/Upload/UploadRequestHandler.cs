using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileSorter.Application.Upload
{

    public class UploadRequestHandler : AsyncRequestHandler<UploadRequest>
    {
        private readonly Settings settings;
        private readonly IMediator mediator;

        public UploadRequestHandler(Settings settings, IMediator mediator)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override async Task Handle(UploadRequest request, CancellationToken cancellationToken)
        {
            var location = Path.Combine(this.settings.FilesLocation, request.Path ?? "");
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }

            // Iterate through uploaded files array
            foreach (var file in request.Files)
            {
                // Extract file name from whatever was posted by browser
                var fileName = Path.Combine(location, Path.GetFileName(file.FileName));

                // If file with same name exists delete it
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create new local file and copy contents of uploaded file
                using (var localFile = File.OpenWrite(fileName))
                using (var uploadedFile = file.OpenReadStream())
                {
                    await uploadedFile.CopyToAsync(localFile).ConfigureAwait(false);
                }
                await this.mediator.Publish(new UploadedNotification(fileName)).ConfigureAwait(false);
            }
        }
    }
}
