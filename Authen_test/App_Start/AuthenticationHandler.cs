using Authen_test.Entity;
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
                JWTPayload AccessToken = Authen.DecodeAccessToken(Authorization.Scheme);
                var get_username = Authen.Username_model(AccessToken.username);
                AccessToken.username = get_username.username;
                if (get_username.type_login.Equals("Mainsystem"))
                {
                    var UserVerify = Authen.VerifyAccessToken_main(AccessToken);
                    if (UserVerify != null)
                    {
                        var member_login= new UserLogin(new GenericIdentity(UserVerify.mem_usename), UserVerify);
                        Thread.CurrentPrincipal = member_login;
                        HttpContext.Current.User = member_login;
                        //var UserLogin = new UserLogin(new GenericIdentity(UserVerify.mem_usename));
                    }
                }
                if (get_username.type_login.Equals("Backoffice"))
                {
                    var UserVerify = Authen.VerifyAccessToken_back(AccessToken);
                    if (UserVerify != null)
                    {
                        var ad_login = new AdminLogin(new GenericIdentity(UserVerify.ad_username), UserVerify.roles.ToString());
                        Thread.CurrentPrincipal = ad_login;
                        HttpContext.Current.User = ad_login;
                        ad_login.my_admin = UserVerify;
                        //var UserLogin = new UserLogin(new GenericIdentity(UserVerify.mem_usename));
                    }
                }
                
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
    public class UserLogin : IPrincipal
    {
        public member my_Member { get; set; }
        public UserLogin(IIdentity identity , member member) {
            this.Identity = identity;
            this.my_Member = member;
        }
        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }

    public class AdminLogin : GenericPrincipal
    {
        public AdminLogin(IIdentity identity, string roles)
            : base(identity, new string[] { roles.ToString()})
        {
        }
        public admin my_admin { get; set; }
    }
}