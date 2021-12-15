using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTOplus
{
    public class flatDonHang
    {
        public Guid IDDonHang { get; set; }
        public int MaSo { get; set; }
        public string TenKhachHang { get; set; }
        public string GhiChu { get; set; }
        public decimal TienHang { get; set; }
        public decimal TienShip { get; set; }
        public decimal TienGiam { get; set; }
        public decimal TongTien { get; set; }
        public string TenTrangThai { get; set; }
        public string TenPhuongThucThanhToan { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}