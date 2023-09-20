using Bionuclear.Core.AzureFiles;
using Bionuclear.Infrastructure.Sql.Commands.LinksResultados;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionuclear.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileShare fileShare;
        private IMediator mediator;

        public FilesController(IFileShare fileShare, IMediator _mediator)
        {
            this.fileShare = fileShare;
            mediator = _mediator;
        }

        /// <summary>
        /// upload file
        /// </summary>
        /// <param name="fileDetail"></param>
        /// <returns></returns>
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFile([FromForm] FilesDetails fileDetail)
        {
            if (fileDetail.FileDetail != null)
            {
                var expediente = await mediator.Send(new LinksCommand(new Core.Dtos.LinksDtos { nombre_documento = fileDetail.FileDetail.FileName }));
                await fileShare.FileUploadAsync(fileDetail);        
                return Ok(expediente);
            }

            return BadRequest();
           
        }

        /// <summary>
        /// download file
        /// </summary>
        /// <param name="fileDetail"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Download")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            string file = "";

            if (fileName != null)
            {
                file = await fileShare.FileDownloadAsync(fileName);
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(file);
            return File(bytes, "application/octet-stream", Path.GetFileName(file));
        }
    }
}
