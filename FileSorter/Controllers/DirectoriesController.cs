using System.Threading.Tasks;
using FileSorter.Application.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileSorter.Controllers
{
    [ApiController]
    [Route("directories/{**path}")]
    public class DirectoriesController : ControllerBase
    {
        private readonly ILogger<DirectoriesController> logger;
        private readonly IMediator mediator;

        public DirectoriesController(ILogger<DirectoriesController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string path = "")
        {
            var pathInfo = await this.mediator.Send(new GetPathInfoRequest(path));

            if (pathInfo.Exists)
            {
                if (pathInfo.IsDirectory)
                {
                    var directoryEntries = await this.mediator.Send(new GetDirectoryMetadataRequest(path));
                    return Ok(directoryEntries);
                }
            }

            return NotFound();
        }

    }
}