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
using Authen_test.App_Start;

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
        [Authorize]
        [HttpGet]
        [Route("api/Authentication")]
        public IHttpActionResult Get_Authentication()
        {
            var data = User as UserLogin;
            return Json(data.my_Member);
        }

        [HttpPost]
        [Route("api/admin_login")]
        public IHttpActionResult Post_Login_admin([FromBody] m_Login_mem_post val)
        {
            if (Authen.Login_admin(val))
            {
                return Ok(JWTAuthen.GenerateAccessToken(val.username, type_login.Backoffice));
            }
            return BadRequest("failed");
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        [Route("api/admin_Authentication")]
        public IHttpActionResult Get_Authentication_admin()
        {
            var data = User as AdminLogin;
            return Json(data.my_admin);
        }
    }
}
