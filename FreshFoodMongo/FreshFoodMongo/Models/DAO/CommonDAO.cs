using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DAO
{
    public class CommonDAO : BaseDAO
    {
        public string getRf_TenTheLoai(Guid id) => (new TheLoaiDAO()).GetByID(id).Ten;
        public string getRf_TenNguoiDung(Guid id) => (new NguoiDungDAO()).GetByID(id).Ten;
        public string getRf_TenPhuongThucThanhToan(Guid id) => (new PhuongThucThanhToanDAO()).GetByID(id).TenPhuongThucThanhToan;
        public string getRf_TenTrangThai(Guid id) => (new TrangThaiDAO()).GetByID(id).TenTrangThai;
        public string getRf_TenNhaCungCap(Guid id) => (new NhaCungCapDAO()).GetByID(id).Ten;
        public string getRf_TenLoaiNguoiDung(Guid id) => (new LoaiNguoiDungDAO()).GetByID(id).Ten;
        public string getRf_TenPhanLoaiKhachHang(Guid id) => (new PhanLoaiKhachHangDAO()).GetByID(id).Ten;
        public string getRf_TenSanPham(Guid id) => (new SanPhamDAO()).GetByID(id).Ten;
        public int? getRf_CapDoPhanLoaiKhachHang(Guid id) => (new PhanLoaiKhachHangDAO()).GetByID(id).CapDo;
        public string getRf_TenCapDoPhanLoaiKhachHang(Guid? id) => (id!=null)? (new PhanLoaiKhachHangDAO()).GetByID((Guid)id).Ten : string.Empty;
        public decimal? getRf_GiaTienSanPham(Guid id) => (new SanPhamDAO()).GetByID(id).GiaTien;
        public decimal? getRf_GiaKhuyenMaiSanPham(Guid id) => (new SanPhamDAO()).GetByID(id).GiaKhuyenMai;
        public string getRf_HinhAnhSanPham(Guid id) => (new SanPhamDAO()).GetByID(id).HinhAnh;
        public string getRf_MaSoTheLoai(Guid id) => (new TheLoaiDAO()).GetByID(id).MaSo;
        public string getRf_VietTatTaiKhoanThanhToan(Guid id) => (new TaiKhoanThanhToanDAO()).GetByID(id).VietTat;
        public string getRf_LogoTaiKhoanThanhToan(Guid id) => (new TaiKhoanThanhToanDAO()).GetByID(id).Logo;
        public int getRf_MaSoHoaDonNhap(Guid id) => (new HoaDonNhapDAO()).GetByID(id).MaSo;
        public decimal? getRf_TongTienGioHangNguoiDung(Guid id) => (new NguoiDungDAO()).GetByID(id).TongTienGioHang;
    }
}