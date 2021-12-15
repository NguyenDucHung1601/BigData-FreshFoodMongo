using FreshFoodMongo.Models.DTO;
using MongoDB.Driver;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class MaGiamGiaKhachHangDAO : BaseDAO
    {
        public MaGiamGiaKhachHangDAO()
        {
            _dbMaGiamGiaKhachHang = getDBMaGiamGiaKhachHang();
        }

        public IEnumerable<MaGiamGiaKhachHang> ListMaGiamGiaKhachHang()
        {
            return getDataMaGiamGiaKhachHang();
        }

        public MaGiamGiaKhachHang GetByID(Guid idmgg, Guid idkh)
        {
            return getDataMaGiamGiaKhachHang().Where(x => x.IDMaGiamGia == idmgg).Where(x => x.IDKhacHang == idkh).FirstOrDefault();
        }

        public void Add(MaGiamGiaKhachHang obj)
        {
            _dbMaGiamGiaKhachHang.InsertOne(obj);
        }

        public long Delete(Guid idmgg, Guid idkh)
        {
            MaGiamGiaKhachHang obj = GetByID(idmgg, idkh);
            if (obj != null)
            {
                var filter = Builders<MaGiamGiaKhachHang>.Filter.Eq("_id", obj._id);
                var result = _dbMaGiamGiaKhachHang.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }

    }
}