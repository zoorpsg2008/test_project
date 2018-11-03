using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Authen_test.Models;
using Authen_test.Interface;
using Authen_test.Services;
using Jose.jwe;

namespace Authen_test.Controllers
{
    public class LoginController : ApiController
    {
        private EF_Authentication Authen = new EF_Authentication();
        private JWTAuthentication JWTAuthen = new JWTAuthentication();
        [HttpPost]
        [Route("api/member_login")]
        public IHttpActionResult Post_Login_member([FromBody] m_Login_mem_post val)
        {
            if (Authen.Login_member(val))
            {
                return Ok(JWTAuthen.GenerateAccessToken(val.username,type_login.Mainsystem));
            }
            return BadRequest("failed");
        }

        [HttpGet]
        [Route("api/Authentication")]
        public IHttpActionResult Get_Authentication()
        {
            return Json(new {
                isLogin = User.Identity.IsAuthenticated,
                username = User.Identity.Name
            });
        }
    }
}
