using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvM.Uploader.Backend.Web.BL;
using MvM.Uploader.Backend.Web.Models;
using System;
using System.Security.Authentication;

namespace MvM.Uploader.Backend.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EpisodeController : ControllerBase
    {
        private readonly ILogger<EpisodeController> _logger;
        private readonly Database _database;
        private readonly Authentication _authentication;
        private readonly BL.Episode _episode;
        private readonly Auphonic _auphonic;

        public EpisodeController(ILogger<EpisodeController> logger, Database database, Authentication authentication, Auphonic auphonic, BL.Episode episode)
        {
            _logger = logger;
            _database = database;
            _authentication = authentication;
            _episode = episode;
        }

        private void CheckCode(string code)
        {
            if (!_authentication.CodeIsValid(code))  throw new AuthenticationException("Wrong code");
        }

        [HttpPost]
        [Route("codeisvalid")]
        public ActionResponse<object> CodeIsValid([FromBody] EpisodeModel upload)
        {
            try
            {
                CheckCode(upload.Code);
                return new ActionResponse<object>(true);
            }
            catch (AuthenticationException ex)
            {
                _logger.LogDebug(ex, $"wrong input on {nameof(CodeIsValid)}");  // UserInput. No real error
                return new ActionResponse<object>(ActionResponse<object>.ErrorCodes.WrongCode);
            }
            catch (Exception ex)
            {
                return new ActionResponse<object>(ex, $"error on {nameof(CodeIsValid)}");
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Route("start")]
        [RequestSizeLimit(500_000_000)]     // 500MB
        public ActionResponse<string> UploadAndStartEpisode([FromForm]string id,  [FromForm] IFormFile file)
        {
            string auphonicProductionId = null;
            try
            {
                _episode.StartEpisode(id, file);
                return new ActionResponse<string>(auphonicProductionId);
            }
            catch (ArgumentException)
            {
                _logger.LogWarning("Tried to upload to non-existing Episode");
                return new ActionResponse<string>(ActionResponse<string>.ErrorCodes.WrongId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting episode");
                return new ActionResponse<string>(ex);
            }
        }

        [HttpPost]
        public ActionResponse<string> CreateEpisode(EpisodeModel uploaddata)
        {
            try
            {
                CheckCode(uploaddata.Code);
            }
            catch (AuthenticationException ex)
            {
                _logger.LogDebug(ex, $"wrong input on {nameof(CreateEpisode)}");  // wrong UserInput. No real error
                return new ActionResponse<string>(ActionResponse<string>.ErrorCodes.WrongCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error on {nameof(CreateEpisode)}");
                return new ActionResponse<string>(ex);
                throw;
            }

            string episodeId = _episode.InitEpisode(uploaddata);
            _logger.LogInformation($"Episode '{episodeId}' stored in Database");
            return new ActionResponse<string>(episodeId);
        }
    }
}