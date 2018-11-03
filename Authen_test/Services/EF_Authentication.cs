using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Authen_test.Interface;
using Authen_test.Models;
using Authen_test.Entity;

namespace Authen_test.Services
{
    public class EF_Authentication : IAuthentication
    {
        private test_db db = new test_db();
        public bool Login_member(m_Login_mem_post val)
        {
            var res = db.members.FirstOrDefault(e => e.mem_usename.Equals(val.username) && e.mem_password.Equals(val.password));
            return res == null ? false : true ;
        }
    }
}