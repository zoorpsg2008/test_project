using Authen_test.Entity;
using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace Authen_test.Services
{
    public class JWTAuthentication
    {
        private byte[] secretKey = Encoding.UTF8.GetBytes("JWTAuthentication");
        public string GenerateAccessToken(string username , type_login type, int minute = 60)
        {
            try
            { 
                //string type_str = type == type_login.mainsystem ? "mainsystem" : throw new Exception("Type Error");
                JWTPayload payload = new JWTPayload
                {
                    username = $"{type.ToString()}:"+username,
                    exp = DateTime.UtcNow.AddMinutes(minute)
                };
                return JWT.Encode(payload, this.secretKey, JwsAlgorithm.HS256);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public member VerifyAccessToken(string accessToken)
        {
            test_db db = new test_db();
            try
            {
                JWTPayload payload  = JWT.Decode<JWTPayload>(accessToken, this.secretKey);
                if (payload == null) return null;
                if (payload.exp < DateTime.UtcNow) return null;
                var get_username = payload.username.Split(':');
                var res = db.members.FirstOrDefault(e => e.mem_usename.Equals(get_username[1]));
                return res;
            }
            catch { return null; }
        }


    }

    public class JWTPayload
    {
        public string username { get; set; }
        public DateTime exp { get; set; }
    }
    public class Authentication_model<T>
    {
        public T Get_model {get;set;}
    }
    public enum type_login 
    {
        Mainsystem,
        Backoffice
    }
}