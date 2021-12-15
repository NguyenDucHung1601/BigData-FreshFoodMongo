using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MongoDB.Driver;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class TaiKhoanThanhToanDAO : BaseDAO
    {
        public TaiKhoanThanhToanDAO()
        {
            _dbTaiKhoanThanhToan = getDBTaiKhoanThanhToan();
        }
        public IEnumerable<TaiKhoanThanhToan> ListTaiKhoanThanhToan()
        {
            return getDataTaiKhoanThanhToan();
        }

        public TaiKhoanThanhToan GetByID(Guid id)
        {
            return getDataTaiKhoanThanhToan().FirstOrDefault(x => x.IDTaiKhoan == id);
        }
        public void Add(TaiKhoanThanhToan obj)
        {
            _dbTaiKhoanThanhToan.InsertOne(obj);
        }
        public void Edit(TaiKhoanThanhToan obj)
        {
            TaiKhoanThanhToan taiKhoanThanhToan = GetByID(obj.IDTaiKhoan);
            if (taiKhoanThanhToan!=null)
            {
                var filter = Builders<TaiKhoanThanhToan>.Filter.Eq("_id", obj._id);
                var update = Builders<TaiKhoanThanhToan>.Update
                    .Set("Ten", obj.Ten)
                    .Set("VietTat", obj.VietTat)
                    .Set("Logo", obj.Logo)
                    .Set("LoaiTaiKhoan", obj.LoaiTaiKhoan);
                _dbTaiKhoanThanhToan.UpdateOne(filter, update);
            }
        }
        public long Delete(Guid id)
        {
            TaiKhoanThanhToan taiKhoanThanhToan = GetByID(id);
            if (taiKhoanThanhToan != null)
            {
                var filter = Builders<TaiKhoanThanhToan>.Filter.Eq("_id", taiKhoanThanhToan._id);
                var result = _dbTaiKhoanThanhToan.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }
        public IEnumerable<TaiKhoanThanhToan> ListSimple(string searching)
        {
            var list = getDataTaiKhoanThanhToan()
                .Where(x => x.Ten.ToLower().Contains(searching.ToLower())
                        || x.VietTat.ToLower().Contains(searching.ToLower())
                        || x.LoaiTaiKhoan.ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.Ten);
            return list;
        }

        public IEnumerable<TaiKhoanThanhToan> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<TaiKhoanThanhToan>(PageNum, PageSize);
            return list;
        }
    }
}