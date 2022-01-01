using Microsoft.AspNetCore.Mvc;

using MvM.Uploader.Backend.Web.BL;
using MvM.Uploader.Backend.Web.Models;
using System.Security.Authentication;

namespace MvM.Uploader.Backend.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly Authentication _authentication;
        private readonly Database _database;

        public AdminController(Authentication authentication, Database database)
        {
            _authentication = authentication;
            _database = database;
        }

        [HttpGet]
        [Route("changepw")]
        public ActionResponse<string> ChangeUploadCode(string adminpassword, string newCode)
        {
            if (!_authentication.AdminPasswordIsValid(adminpassword)) throw new AuthenticationException("Wrong admin password");
            _database.ChangeUploadCode(newCode);    // TODO: Regex-Validation. 5 Chars, etc.
            return new ActionResponse<string>("Code changed to '{newCode}'");
        }
    }
}