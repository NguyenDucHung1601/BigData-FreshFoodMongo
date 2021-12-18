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
    public class HoaDonNhapDAO : BaseDAO
    {
        CommonDAO commonDao = new CommonDAO();
        public HoaDonNhapDAO()
        {
            _dbHoaDonNhap = getDBHoaDonNhap();
        }

        public IEnumerable<int> ThongKeHoaDonNhap()
        {
            var res = getDataHoaDonNhap()
                .GroupBy(x => x.CreatedDate.Value.Month)
                .OrderBy(x => x.Key)
                .Select(x => x.Count()).ToList();
            return res;
        }
        public IEnumerable<HoaDonNhap> ListHoaDonNhap()
        {
            return getDataHoaDonNhap();
        }

        public HoaDonNhap GetByID(Guid id)
        {
            return getDataHoaDonNhap().Where(x => x.IDHoaDonNhap == id).FirstOrDefault();
        }

        public void Add(HoaDonNhap obj)
        {
            _dbHoaDonNhap.InsertOne(obj);
        }

        public void Edit(HoaDonNhap obj)
        {
            HoaDonNhap hoaDonNhap = GetByID(obj.IDHoaDonNhap);
            if (hoaDonNhap != null)
            {
                var filter = Builders<HoaDonNhap>.Filter.Eq("_id", obj._id);
                var update = Builders<HoaDonNhap>.Update
                    .Set("TienHang", obj.TienHang)
                    .Set("TienShip", obj.TienShip)
                    .Set("TienGiam", obj.TienGiam)
                    .Set("TongTien", obj.TongTien)
                    .Set("GhiChu", obj.GhiChu);
                _dbHoaDonNhap.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            HoaDonNhap hoadonnhap = GetByID(id);
            if (hoadonnhap != null)
            {
                var filter = Builders<HoaDonNhap>.Filter.Eq("_id", hoadonnhap._id);
                var result = _dbHoaDonNhap.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }

        public IEnumerable<HoaDonNhap> ListSimple(string searching)
        {
            var list = getDataHoaDonNhap()
                .Where(x => x.IDHoaDonNhap.ToString().ToLower().Contains(searching.ToLower())
                        || x.TongTien.ToString().ToLower().Contains(searching.ToLower())
                        || x.CreatedDate.ToString().ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenNhaCungCap(x.IDNhaCungCap).ToString().ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.CreatedDate);
            return list;
        }

        public IEnumerable<HoaDonNhap> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<HoaDonNhap>(PageNum, PageSize);
            return list;
        }

        public int TongHoaDonNhap()
        {
            var count = getDataHoaDonNhap().Count();
            return count;
        }
    }
}