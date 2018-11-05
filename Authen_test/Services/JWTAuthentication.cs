using Authen_test.App_Start;
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

        public JWTPayload DecodeAccessToken(string accessToken) => JWT.Decode<JWTPayload>(accessToken, this.secretKey);
        public member VerifyAccessToken_main(JWTPayload payload)
        { 
            try
            {
                test_db db = new test_db();
                if (payload == null) return null;
                if (payload.exp < DateTime.UtcNow) return null;
                var res = db.members.FirstOrDefault(e => e.mem_usename.Equals(payload.username));
                return res;
            }
            catch { return null; }
        }

        public admin VerifyAccessToken_back(JWTPayload payload)
        {
            try
            {
                test_db db = new test_db();
                if (payload == null) return null;
                if (payload.exp < DateTime.UtcNow) return null;
                var res = db.admins.FirstOrDefault(e => e.ad_username.Equals(payload.username));
                return res;
            }
            catch { return null; }
        }

        public Username Username_model(string username)
        {
            Username model = new Username();
            string[] data_username = username.Split(':');
            model.type_login = data_username[0];
            model.username = null;
            for (int i = 0; i < data_username.Count();i++)
            {
                if (i != 0) model.username += data_username[i];
            }
            return model;
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
    public class Username
    {
        public string type_login { get; set; }
        public string username { get; set; }
    }
}
