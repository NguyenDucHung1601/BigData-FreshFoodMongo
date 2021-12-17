using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MongoDB.Driver;

namespace FreshFoodMongo.Models.DAO.Client
{
    public class ClientDonHangDAO : BaseDAO
    {
        CommonDAO commonDao = new CommonDAO();
        public ClientDonHangDAO()
        {
            _dbNguoiDung = getDBNguoiDung();
            _dbChiTietGioHang = getDBChiTietGioHang();
            _dbDonHang = getDBDonHang();
            _dbSanPham = getDBSanPham();
        }

        /// <summary>
        ///  Tính tổng tiền đơn hàng sơ bộ
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="dh"></param>
        public void CapNhatTongTienGioHangSoBo(Guid idND)
        {
            var lstctgh = getDataChiTietGioHang().Where(x => x.IDKhachHang == idND);

            // cập nhật thành tiền
            foreach (ChiTietGioHang item in lstctgh)
            {
                var filter = Builders<ChiTietGioHang>.Filter.Eq("_id", item._id);
                var update = Builders<ChiTietGioHang>.Update
                    .Set("ThanhTien", commonDao.getRf_GiaTienSanPham(item.IDSanPham) * item.SoLuong);
                _dbChiTietGioHang.UpdateOne(filter, update);
            }
            // cập nhật tổng tiền
            var lstctghUpdated = getDataChiTietGioHang().Where(x => x.IDKhachHang == idND);
            var khachHang = getDataNguoiDung().FirstOrDefault(x => x.IDNguoiDung == idND);
            var filter2 = Builders<NguoiDung>.Filter.Eq("_id", khachHang._id);
            var update2 = Builders<NguoiDung>.Update
                .Set("TongTienGioHang", lstctghUpdated.Sum(x => x.ThanhTien));
            _dbNguoiDung.UpdateOne(filter2, update2);
        }


        public List<ChiTietDonHang> CapNhatTongTienDonHangSoBo(List<ChiTietGioHang> lstctgh)
        {
            // Chuyển các sản phẩm trong giỏ hàng => đơn hàng
            //Initialize the mapper
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<ChiTietGioHang, ChiTietDonHang>()
                    .ForMember(dest => dest.IDSanPham, act => act.MapFrom(src => src.IDSanPham))
                    .ForMember(dest => dest.SoLuong, act => act.MapFrom(src => src.SoLuong))
                    .ForMember(dest => dest.ThanhTien, act => act.MapFrom(src => src.ThanhTien))
                );
            var mapper = config.CreateMapper();
            var lstctdh = lstctgh.Select
                          (
                            item => mapper.Map<ChiTietGioHang, ChiTietDonHang>(item)
                          ).ToList();
            return lstctdh;
        }

        /// <summary>
        /// Kiểm tra mã giảm giá áp dụng
        /// </summary>
        /// <param name="mgg"> mã giảm giá</param>
        /// <returns></returns>
        public bool KiemTraMaGiamGia(string mgg)
        {
            var result = getDataMaGiamGia().Any(x => x.CodeGiamGia == mgg);
            if (result)
            {
                var res = getDataMaGiamGia().FirstOrDefault(x => x.CodeGiamGia == mgg);
                if (res.HanSuDung >= DateTime.Now)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool KiemTraDoiTuongApDung(string mgg, NguoiDung kh)
        {
            if (KiemTraMaGiamGia(mgg))
            {
                var mmgObj = getDataMaGiamGia().FirstOrDefault(x => x.CodeGiamGia == mgg);
                var res = getDataPhanLoaiKhachHang().FirstOrDefault(x => x.IDLoaiKhachHang == kh.IDLoaiKhachHang);
                if (commonDao.getRf_CapDoPhanLoaiKhachHang((Guid)mmgObj.IDLoaiKhachHang) == res.CapDo)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// Xác nhận thanh toán đơn hàng
        /// </summary>
        /// <param name="dh"></param>
        public void XacNhanThanhToan(DonHang dh)
        {
            var donHang = getDataDonHang().FirstOrDefault(x => x.IDDonHang == dh.IDDonHang);
            // trạng thái chờ xác nhận
            var filter = Builders<DonHang>.Filter.Eq("_id", donHang._id);
            var update = Builders<DonHang>.Update
                .Set("IDTrangThai", new Guid("5404ec28-c908-48b1-a7e5-e5a366b51d5a"));
            _dbDonHang.UpdateOne(filter, update);
        }


        // Xác nhận đã nhận hàng
        public void XacNhanDaNhanHang(DonHang dh)
        {
            var donHang = getDataDonHang().FirstOrDefault(x => x.IDDonHang == dh.IDDonHang);
            // trạng thái đã giao hàng
            var filter = Builders<DonHang>.Filter.Eq("_id", donHang._id);
            var update = Builders<DonHang>.Update
                .Set("IDTrangThai", new Guid("e970c371-b124-49a4-ae54-781f8f848797"));
            _dbDonHang.UpdateOne(filter, update);
        }

        public void XacNhanDonHang(Guid id)
        {
            var donHang = getDataDonHang().FirstOrDefault(x => x.IDDonHang == id);
            // cập nhật số lượng & số lượt mua
            var lstctDonHang = getDataChiTietDonHang().Where(x => x.IDDonHang == donHang.IDDonHang);

            foreach (ChiTietDonHang item in lstctDonHang)
            {
                var sanpham = getDataSanPham().FirstOrDefault(x => x.IDSanPham == item.IDSanPham);
                var filter = Builders<SanPham>.Filter.Eq("_id", sanpham._id);
                var update = Builders<SanPham>.Update
                    .Set("SoLuong", sanpham.SoLuong * item.SoLuong)
                    .Set("SoLuongMua", sanpham.SoLuong + 1);
                _dbSanPham.UpdateOne(filter, update);

                var donHangUpdated = getDataDonHang().FirstOrDefault(x => x.IDDonHang == id);
                CapNhatGioHang(donHangUpdated.IDKhachHang);
            }
            // cật nhật số đơn hàng và tổng tiền hàng đã mua
            var khachHang = getDataNguoiDung().FirstOrDefault(x => x.IDNguoiDung == donHang.IDKhachHang);
            var filter2 = Builders<NguoiDung>.Filter.Eq("_id", khachHang._id);
            var update2 = Builders<NguoiDung>.Update
                .Set("SoDonHangDaMua", khachHang.SoDonHangDaMua + 1)
                .Set("TongTienHangDaMua", khachHang.TongTienHangDaMua + donHang.TienGiam - donHang.TienGiam + donHang.TienShip);
            _dbNguoiDung.UpdateOne(filter2, update2);

            var khachHangUpdated = getDataNguoiDung().FirstOrDefault(x => x.IDNguoiDung == donHang.IDKhachHang);
            CapNhatTongTienGioHang(khachHangUpdated);
        }

        // Xóa các sản phẩm đã mua trong giỏ hàng và cập nhật tổng tiền giỏ hàng
        public void CapNhatGioHang(Guid id)
        {
            var res = getDataChiTietGioHang().Where(x => x.IDKhachHang == id);
            var filter = Builders<ChiTietGioHang>.Filter.Eq("IDKhachHang", id);
            var result = _dbChiTietGioHang.DeleteMany(filter);
        }

        public void CapNhatTongTienGioHang(NguoiDung kh)
        {
            var lstctgh = getDataChiTietGioHang().Where(x => x.IDKhachHang == kh.IDNguoiDung);
            // cập nhật thành tiền
            foreach (ChiTietGioHang item in lstctgh)
            {
                var filter = Builders<ChiTietGioHang>.Filter.Eq("_id", item._id);
                var update = Builders<ChiTietGioHang>.Update
                    .Set("ThanhTien", commonDao.getRf_GiaTienSanPham(item.IDSanPham) * item.SoLuong);
                _dbChiTietGioHang.UpdateOne(filter, update);
            }
            // cập nhật tổng tiền
            var lstctghUpdated = getDataChiTietGioHang().Where(x => x.IDKhachHang == kh.IDNguoiDung);
            var khachHang = getDataNguoiDung().FirstOrDefault(x => x.IDNguoiDung == kh.IDNguoiDung);
            var filter2 = Builders<NguoiDung>.Filter.Eq("_id", khachHang._id);
            var update2 = Builders<NguoiDung>.Update
                .Set("TongTienGioHang", lstctghUpdated.Sum(x => x.ThanhTien));
            _dbNguoiDung.UpdateOne(filter2, update2);
        }
    }
}