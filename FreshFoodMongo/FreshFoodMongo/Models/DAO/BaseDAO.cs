using FreshFoodMongo.Models.DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DAO
{
    public class BaseDAO
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected static IMongoCollection<ChiTietDonHang> _dbChiTietDonHang;
        protected static IMongoCollection<ChiTietGioHang> _dbChiTietGioHang;
        protected static IMongoCollection<ChiTietHoaDonNhap> _dbChiTietHoaDonNhap;
        protected static IMongoCollection<DonHang> _dbDonHang;
        protected static IMongoCollection<HoaDonNhap> _dbHoaDonNhap;
        protected static IMongoCollection<LoaiNguoiDung> _dbLoaiNguoiDung;
        protected static IMongoCollection<MaGiamGia> _dbMaGiamGia;
        protected static IMongoCollection<MaGiamGiaKhachHang> _dbMaGiamGiaKhachHang;
        protected static IMongoCollection<NguoiDung> _dbNguoiDung;
        protected static IMongoCollection<NhaCungCap> _dbNhaCungCap;
        protected static IMongoCollection<NhaCungCapSanPham> _dbNhaCungCapSanPham;
        protected static IMongoCollection<PhanLoaiKhachHang> _dbPhanLoaiKhachHang;
        protected static IMongoCollection<PhuongThucThanhToan> _dbPhuongThucThanhToan;
        protected static IMongoCollection<SanPham> _dbSanPham;
        protected static IMongoCollection<SanPhamKhuyenMai> _dbSanPhamKhuyenMai;
        protected static IMongoCollection<TaiKhoanThanhToan> _dbTaiKhoanThanhToan;
        protected static IMongoCollection<TheLoai> _dbTheLoai;
        protected static IMongoCollection<ThongTinLienHe> _dbThongTinLienHe;
        protected static IMongoCollection<ThongTinNhanHang> _dbThongTinNhanHang;
        protected static IMongoCollection<TKThanhToanNguoiDung> _dbTKThanhToanNguoiDung;
        protected static IMongoCollection<TrangThai> _dbTrangThai;

        public BaseDAO()
        {
            _client = new MongoClient(Common.CommonVariables.g_connectionString);
            _database = _client.GetDatabase(Common.CommonVariables.g_databaseName);
        }

        #region GetCollections
        public IMongoCollection<ChiTietDonHang> getDBChiTietDonHang() => _database.GetCollection<ChiTietDonHang>("ChiTietDonHang");
        public IMongoCollection<ChiTietGioHang> getDBChiTietGioHang() => _database.GetCollection<ChiTietGioHang>("ChiTietGioHang");
        public IMongoCollection<ChiTietHoaDonNhap> getDBChiTietHoaDonNhap() => _database.GetCollection<ChiTietHoaDonNhap>("ChiTietHoaDonNhap");
        public IMongoCollection<DonHang> getDBDonHang() => _database.GetCollection<DonHang>("DonHang");
        public IMongoCollection<HoaDonNhap> getDBHoaDonNhap() => _database.GetCollection<HoaDonNhap>("HoaDonNhap");
        public IMongoCollection<LoaiNguoiDung> getDBLoaiNguoiDung() => _database.GetCollection<LoaiNguoiDung>("LoaiNguoiDung");
        public IMongoCollection<MaGiamGia> getDBMaGiamGia() => _database.GetCollection<MaGiamGia>("MaGiamGia");
        public IMongoCollection<MaGiamGiaKhachHang> getDBMaGiamGiaKhachHang() => _database.GetCollection<MaGiamGiaKhachHang>("MaGiamGiaKhachHang");
        public IMongoCollection<NguoiDung> getDBNguoiDung() => _database.GetCollection<NguoiDung>("NguoiDung");
        public IMongoCollection<NhaCungCap> getDBNhaCungCap() => _database.GetCollection<NhaCungCap>("NhaCungCap");
        public IMongoCollection<NhaCungCapSanPham> getDBNhaCungCapSanPham() => _database.GetCollection<NhaCungCapSanPham>("NhaCungCapSanPham");
        public IMongoCollection<PhanLoaiKhachHang> getDBPhanLoaiKhachHang() => _database.GetCollection<PhanLoaiKhachHang>("PhanLoaiKhachHang");
        public IMongoCollection<PhuongThucThanhToan> getDBPhuongThucThanhToan() => _database.GetCollection<PhuongThucThanhToan>("PhuongThucThanhToan");
        public IMongoCollection<SanPham> getDBSanPham() => _database.GetCollection<SanPham>("SanPham");
        public IMongoCollection<SanPhamKhuyenMai> getDBSanPhamKhuyenMai() => _database.GetCollection<SanPhamKhuyenMai>("SanPhamKhuyenMai");
        public IMongoCollection<TaiKhoanThanhToan> getDBTaiKhoanThanhToan() => _database.GetCollection<TaiKhoanThanhToan>("TaiKhoanThanhToan");
        public IMongoCollection<TheLoai> getDBTheLoai() => _database.GetCollection<TheLoai>("TheLoai");
        public IMongoCollection<ThongTinLienHe> getDBThongTinLienHe() => _database.GetCollection<ThongTinLienHe>("ThongTinLienHe");
        public IMongoCollection<ThongTinNhanHang> getDBThongTinNhanHang() => _database.GetCollection<ThongTinNhanHang>("ThongTinNhanHang");
        public IMongoCollection<TKThanhToanNguoiDung> getDBTKThanhToanNguoiDung() => _database.GetCollection<TKThanhToanNguoiDung>("TKThanhToanNguoiDung");
        public IMongoCollection<TrangThai> getDBTrangThai() => _database.GetCollection<TrangThai>("TrangThai");
        #endregion

        #region GetData
        public IEnumerable<ChiTietDonHang> getDataChiTietDonHang() => getDBChiTietDonHang().Find(FilterDefinition<ChiTietDonHang>.Empty).ToList();
        public IEnumerable<ChiTietGioHang> getDataChiTietGioHang() => getDBChiTietGioHang().Find(FilterDefinition<ChiTietGioHang>.Empty).ToList();
        public IEnumerable<ChiTietHoaDonNhap> getDataChiTietHoaDonNhap() => getDBChiTietHoaDonNhap().Find(FilterDefinition<ChiTietHoaDonNhap>.Empty).ToList();
        public IEnumerable<DonHang> getDataDonHang() => getDBDonHang().Find(FilterDefinition<DonHang>.Empty).ToList();
        public IEnumerable<HoaDonNhap> getDataHoaDonNhap() => getDBHoaDonNhap().Find(FilterDefinition<HoaDonNhap>.Empty).ToList();
        public IEnumerable<LoaiNguoiDung> getDataLoaiNguoiDung() => getDBLoaiNguoiDung().Find(FilterDefinition<LoaiNguoiDung>.Empty).ToList();
        public IEnumerable<MaGiamGia> getDataMaGiamGia() => getDBMaGiamGia().Find(FilterDefinition<MaGiamGia>.Empty).ToList();
        public IEnumerable<MaGiamGiaKhachHang> getDataMaGiamGiaKhachHang() => getDBMaGiamGiaKhachHang().Find(FilterDefinition<MaGiamGiaKhachHang>.Empty).ToList();
        public IEnumerable<NguoiDung> getDataNguoiDung() => getDBNguoiDung().Find(FilterDefinition<NguoiDung>.Empty).ToList();
        public IEnumerable<NhaCungCap> getDataNhaCungCap() => getDBNhaCungCap().Find(FilterDefinition<NhaCungCap>.Empty).ToList();
        public IEnumerable<NhaCungCapSanPham> getDataNhaCungCapSanPham() => getDBNhaCungCapSanPham().Find(FilterDefinition<NhaCungCapSanPham>.Empty).ToList();
        public IEnumerable<PhanLoaiKhachHang> getDataPhanLoaiKhachHang() => getDBPhanLoaiKhachHang().Find(FilterDefinition<PhanLoaiKhachHang>.Empty).ToList();
        public IEnumerable<PhuongThucThanhToan> getDataPhuongThucThanhToan() => getDBPhuongThucThanhToan().Find(FilterDefinition<PhuongThucThanhToan>.Empty).ToList();
        public IEnumerable<SanPham> getDataSanPham() => getDBSanPham().Find(FilterDefinition<SanPham>.Empty).ToList();
        public IEnumerable<SanPhamKhuyenMai> getDataSanPhamKhuyenMai() => getDBSanPhamKhuyenMai().Find(FilterDefinition<SanPhamKhuyenMai>.Empty).ToList();
        public IEnumerable<TaiKhoanThanhToan> getDataTaiKhoanThanhToan() => getDBTaiKhoanThanhToan().Find(FilterDefinition<TaiKhoanThanhToan>.Empty).ToList();
        public IEnumerable<TheLoai> getDataTheLoai() => getDBTheLoai().Find(FilterDefinition<TheLoai>.Empty).ToList();
        public IEnumerable<ThongTinLienHe> getDataThongTinLienHe() => getDBThongTinLienHe().Find(FilterDefinition<ThongTinLienHe>.Empty).ToList();
        public IEnumerable<ThongTinNhanHang> getDataThongTinNhanHang() => getDBThongTinNhanHang().Find(FilterDefinition<ThongTinNhanHang>.Empty).ToList();
        public IEnumerable<TKThanhToanNguoiDung> getDataTKThanhToanNguoiDung() => getDBTKThanhToanNguoiDung().Find(FilterDefinition<TKThanhToanNguoiDung>.Empty).ToList();
        public IEnumerable<TrangThai> getDataTrangThai() => getDBTrangThai().Find(FilterDefinition<TrangThai>.Empty).ToList();
        #endregion
    }
}