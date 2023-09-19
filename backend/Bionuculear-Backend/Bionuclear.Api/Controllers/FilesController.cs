using Bionuclear.Core.AzureFiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bionuclear.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileShare fileShare;

        public FilesController(IFileShare fileShare)
        {
            this.fileShare = fileShare;
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
                await fileShare.FileUploadAsync(fileDetail);
            }
            return Ok();
        }

        /// <summary>
        /// download file
        /// </summary>
        /// <param name="fileDetail"></param>
        /// <returns></returns>
        [HttpPost("Download")]
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
