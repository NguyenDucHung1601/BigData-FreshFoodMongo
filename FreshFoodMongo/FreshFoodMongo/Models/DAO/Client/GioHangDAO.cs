using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DAO.Client
{
    public class GioHangDAO : BaseDAO
    {
        NguoiDungDAO ndDao = new NguoiDungDAO();
        public GioHangDAO()
        {
            _dbChiTietGioHang = getDBChiTietGioHang();
        }
        public ChiTietGioHang GetChiTietGioHangByID(Guid id)
        {
            return getDataChiTietGioHang().FirstOrDefault(x => x.IDChiTietGioHang == id);
        }
        public IEnumerable<ChiTietGioHang> GetListChiTietGioHangByIDKhachHang(Guid id)
        {
            return getDataChiTietGioHang().Where(x => x.IDKhachHang == id);
        }
        public bool KTGIOHANG(ChiTietGioHang obj)
        {
            if (obj.SoLuong <= ((new SanPhamDAO()).GetByID(obj.IDSanPham).SoLuong))
                return true;
            return false;
        }
        public void Add(ChiTietGioHang obj)
        {
            _dbChiTietGioHang.InsertOne(obj);

            NguoiDung nguoidung = ndDao.GetByID(obj.IDKhachHang);
            var filter = Builders<NguoiDung>.Filter.Eq("_id", nguoidung._id);
            var update = Builders<NguoiDung>.Update
                .Set("TongTienGioHang", nguoidung.TongTienGioHang + obj.ThanhTien);
            _dbNguoiDung.UpdateOne(filter, update);
        }
        public void Edit(ChiTietGioHang obj)
        {
            ChiTietGioHang ctGioHang = GetChiTietGioHangByID(obj.IDChiTietGioHang);
            if (ctGioHang != null)
            {
                NguoiDung nguoidung = ndDao.GetByID(obj.IDKhachHang);
                nguoidung.TongTienGioHang -= ctGioHang.ThanhTien;
                ctGioHang.SoLuong = obj.SoLuong;
                ctGioHang.ThanhTien = obj.ThanhTien;
                nguoidung.TongTienGioHang += ctGioHang.ThanhTien;
                ctGioHang.DuocChon = obj.DuocChon;

                var filter1 = Builders<NguoiDung>.Filter.Eq("_id", nguoidung._id);
                var update1 = Builders<NguoiDung>.Update
                    .Set("TongTienGioHang", nguoidung.TongTienGioHang);
                _dbNguoiDung.UpdateOne(filter1, update1);


                var filter2 = Builders<ChiTietGioHang>.Filter.Eq("_id", ctGioHang._id);
                var update2 = Builders<ChiTietGioHang>.Update
                    .Set("SoLuong", ctGioHang.SoLuong)
                    .Set("DuocChon", ctGioHang.DuocChon)
                    .Set("ThanhTien", ctGioHang.ThanhTien);
                _dbChiTietGioHang.UpdateOne(filter2, update2);
            }
        }
        public bool CNGioHang(Guid id)
        {
            var listChitietgiohang = getDataChiTietGioHang().Where(x => x.IDKhachHang == id);
            NguoiDung user = ndDao.GetByID(id);
            ChiTietGioHang cartDetail = new ChiTietGioHang();
            foreach (var item in listChitietgiohang)
            {
                if (!KTGIOHANG(item))
                    return false;
                cartDetail = getDataChiTietGioHang().Where(x => x.IDKhachHang == id && x.IDSanPham == item.IDSanPham).FirstOrDefault();
                user.TongTienGioHang -= cartDetail.ThanhTien;
                cartDetail.ThanhTien = item.ThanhTien;
                user.TongTienGioHang += cartDetail.ThanhTien;

                var filter1 = Builders<NguoiDung>.Filter.Eq("_id", user._id);
                var update1 = Builders<NguoiDung>.Update
                    .Set("TongTienGioHang", user.TongTienGioHang);
                _dbNguoiDung.UpdateOne(filter1, update1);

                var filter2 = Builders<ChiTietGioHang>.Filter.Eq("_id", cartDetail._id);
                var update2 = Builders<ChiTietGioHang>.Update
                    .Set("ThanhTien", cartDetail.ThanhTien);
                _dbChiTietGioHang.UpdateOne(filter2, update2);
            }
            return true;
        }
    }

}