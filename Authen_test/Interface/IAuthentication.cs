using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authen_test.Models;
using Authen_test.Entity;

namespace Authen_test.Interface
{
    interface IAuthentication
    {
        bool Login_member(m_Login_mem_post val);
    }
}
