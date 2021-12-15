using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTOplus
{
    public class flatNguoiDungMoi
    {
        public Guid IDUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Confirm { get; set; }
        public string Ten { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public HttpPostedFileBase Avatar { get; set; }
    }
}