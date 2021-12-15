using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DTOplus
{
    public class flatKhachHang
    {
        public Guid IDNguoiDung { get; set; }
        public Guid IDLoaiNguoiDung { get; set; }
        public Guid IDPhanLoaiKhachHang { get; set; }
        public string TenLoaiKhachHang { get; set; }
        public string Ten { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public decimal TongTienGioHang { get; set; }
        public int SoDonHangDaMua { get; set; }
        public decimal TongTienHangDaMua { get; set; }
        public bool TrangThai { get; set; }
        public DateTime LanHoatDongGanNhat { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}