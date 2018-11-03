using Authen_test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
                Authen.VerifyAccessToken(AccessToken);


            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}