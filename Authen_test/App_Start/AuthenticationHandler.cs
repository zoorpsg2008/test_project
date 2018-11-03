using Authen_test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Authen_test.App_Start
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private JWTAuthentication Authen = new JWTAuthentication();
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var Authorization = request.Headers.Authorization;
            if (Authorization != null)
            {

                //string AccessToken = Authorization.Parameter;
                string AccessToken = Authorization.Scheme;
                var get_username = AccessToken.Split(':');
                if (get_username[0].Equals("Mainsystem"))
                {

                }
                if (get_username[0].Equals("Backoffice"))
                {

                }
                Authen.VerifyAccessToken_main(AccessToken);


            }
            return base.SendAsync(request, cancellationToken);
        }
    }
    public class UserLogin : GenericPrincipal
    {
        public UserLogin(IIdentity identity, string[] roles) : base(identity, roles)
        {
        }
    }
}