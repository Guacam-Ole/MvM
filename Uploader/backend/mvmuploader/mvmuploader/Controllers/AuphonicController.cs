using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvM.Uploader.Backend.Web.BL;
using MvM.Uploader.Backend.Web.Models;
using System;
using System.Threading.Tasks;

namespace MvM.Uploader.Backend.Web.Controllers
{
    public class AuphonicController:Controller
    {
        private readonly Auphonic _auphonic;

        public AuphonicController(Auphonic auphonic)
        {
            _auphonic = auphonic;
        }

        [HttpGet]
        [Route("credits")]
        public async Task<ActionResponse<TimeSpan>> GetRemainingTimeAsync()
        {
            var account = await _auphonic.GetAccountAsync();
            var remainingDuration = TimeSpan.FromHours(account.Credits);
            return new ActionResponse<TimeSpan>(remainingDuration);
        }
    }
}
