using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tedu.OnlineShop
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}