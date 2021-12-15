using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTOplus
{
    public class flatTKThanhToanNguoiDung
    {
        public Guid IDTaiKhoan { get; set; }
        public Guid IDNguoiDung { get; set; }
        public string TenNguoiDung { get; set; }    
        public string TenTaiKhoan { get; set; }
        public string TaiKhoan { get; set; }
        public string Logo { get; set; }
        public string LoaiTaiKhoan { get; set; }
    }
}