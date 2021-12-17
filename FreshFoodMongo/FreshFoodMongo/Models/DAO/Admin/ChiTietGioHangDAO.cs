using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using FreshFoodMongo.Models.DTOplus;
using MongoDB.Driver;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class ChiTietGioHangDAO : BaseDAO
    {
        public ChiTietGioHangDAO()
        {
            _dbChiTietGioHang = getDBChiTietGioHang();
        }

        public IEnumerable<ChiTietGioHang> GetListChiTietGioHang(Guid id)
        {
            return getDataChiTietGioHang().Where(x => x.IDKhachHang == id);
        }

        public ChiTietGioHang GetByID(Guid id)
        {
            return getDataChiTietGioHang().FirstOrDefault(x => x.IDChiTietGioHang == id);
        }

        public void Add(ChiTietGioHang obj)
        {
            _dbChiTietGioHang.InsertOne(obj);
        }

        public void Edit(ChiTietGioHang obj)
        {
            ChiTietGioHang chiTietGioHang = GetByID(obj.IDChiTietGioHang);
            if (chiTietGioHang != null)
            {
                var filter = Builders<ChiTietGioHang>.Filter.Eq("_id", obj._id);
                var update = Builders<ChiTietGioHang>.Update
                    .Set("SoLuong", obj.SoLuong)
                    .Set("ThanhTien", obj.ThanhTien);
                _dbChiTietGioHang.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            ChiTietGioHang chiTietGioHang = GetByID(id);
            if (chiTietGioHang != null)
            {
                var filter = Builders<ChiTietGioHang>.Filter.Eq("_id", chiTietGioHang._id);
                var result = _dbChiTietGioHang.DeleteOne(filter);

                var khachHang = getDataNguoiDung().FirstOrDefault(x => x.IDNguoiDung == chiTietGioHang.IDKhachHang);
                khachHang.TongTienGioHang -= chiTietGioHang.ThanhTien;
                (new NguoiDungDAO()).Edit(khachHang);

                return result.DeletedCount;
            }
            else
            {
                return -1;
            }
        }
    }
}