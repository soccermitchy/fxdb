using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fxdb.Controllers {
    [Route("api/userinfo")]
    [Authorize(Policy="AllowedDomain")]
    public class UserInfoController : ControllerBase {
        [HttpGet("claims")]
        public IActionResult GetClaims() {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGetAttribute("name")]
        public string GetName(){
            return User.Identity.Name;
        }

        [HttpGetAttribute("guid")]
        public string GetGuid() {
            return User.Claims.First(c => c.Type == "sub").Value;
        }

        [HttpGetAttribute("authtype")]
        public string GetAuthType() {
            return User.Identity.AuthenticationType;
        }
    }
}