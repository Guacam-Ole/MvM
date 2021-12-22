using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using mvmuploader.Models;

namespace mvmuploader.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private const string _code = "dbddhkp";  // TODO: from config/file
        private readonly ILogger<UploadController> _logger;

        public UploadController(ILogger<UploadController> logger)
        {
            this._logger = logger;
        }

        [HttpPost]
        [Route("codeisvalid")]
        public bool CodeIsValid([FromBody]UploadModel upload)
        {
            return upload.Code == _code;
        }

        [HttpPost]
        public bool UploadData(string code, UploadModel uploaddata)
        {
            return false;
        }
    }

 
}

