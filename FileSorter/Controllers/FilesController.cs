using FileSorter.Application.Get;
using FileSorter.Application.Move;
using FileSorter.Application.Upload;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FileSorter.Controllers
{
    [ApiController]
    [Route("files/{**path}")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> logger;
        private readonly IMediator mediator;

        public FilesController(ILogger<FilesController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string path = "", int? width = null, int? height = null)
        {

            var pathInfo = await this.mediator.Send(new GetPathInfoRequest(path));

            if (pathInfo.Exists)
            {
                if (pathInfo.IsDirectory)
                {
                    var directoryEntries = await this.mediator.Send(new GetDirectoryItemsRequest(path));
                    return Ok(directoryEntries);
                }
                else
                {
                    if (this.Request.Headers.TryGetIfModifiedSince(out var lastModifiedSince))
                    {
                        if (!await this.mediator.Send(new IsModifiedSinceRequest(path, lastModifiedSince)))
                        {
                            return this.StatusCode(304, "Page has not been modified");
                        }
                    }

                    GetFileResponse fileResponse = null;
                    if (width.HasValue || height.HasValue)
                    {
                        fileResponse = await this.mediator.Send(new GetFilePreviewRequest(path, width, height));
                    }
                    else
                    {
                        fileResponse = await this.mediator.Send(new GetFileRequest(path));
                    }

                    this.Response.Headers["Last-Modified"] = fileResponse.LastModified.ToString("R");
                    this.Response.Headers["Cache-Control"] = "no-cache, must-revalidate";
                    return File(fileResponse.Stream, fileResponse.ContentType);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string path, IFormFile[] files)
        {
            await mediator.Send(new UploadRequest(path, files));

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Move(string path, [FromBody]string[] files)
        {
            await mediator.Send(new MoveRequest(path, files));

            return Ok();
        }

    }

}
