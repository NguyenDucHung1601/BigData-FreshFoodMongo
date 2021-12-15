using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTOplus
{
    public class flatSanPham
    {
        public Guid IDSanPham { get; set; }
        public string MaSo { get; set; }
        public string TenSanPham { get; set; }
        public Guid IDTheLoai { get; set; }
        public string TenTheLoai { get; set; }
        public string DonViTinh { get; set; }
        public decimal GiaTien { get; set; }
        public decimal GiaKhuyenMai { get; set; }
        public string HinhAnh { get; set; }
        public string MoTa { get; set; }
        public bool CoSan { get; set; }
        public long SoLuong { get; set; }
        public int SoLuotXem { get; set; }
        public int SoLuotMua { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}