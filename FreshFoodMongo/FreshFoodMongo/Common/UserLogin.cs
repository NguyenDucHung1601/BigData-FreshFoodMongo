using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Common
{
    [Serializable]
    public class UserLogin
    {
        public Guid IDNguoiDung { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string Ten { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}