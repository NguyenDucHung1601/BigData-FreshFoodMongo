using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MongoDB.Driver;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class LoaiNguoiDungDAO : BaseDAO
    {
        public LoaiNguoiDungDAO()
        {
            _dbLoaiNguoiDung = getDBLoaiNguoiDung();
        }

        public IEnumerable<LoaiNguoiDung> ListLoaiNguoiDung()
        {
            return getDataLoaiNguoiDung();
        }

        public LoaiNguoiDung GetByID(Guid id)
        {
            return getDataLoaiNguoiDung().Where(x => x.IDLoaiNguoiDung == id).FirstOrDefault();
        }
        public void Add(LoaiNguoiDung obj)
        {
            _dbLoaiNguoiDung.InsertOne(obj);
        }

        public void Edit(LoaiNguoiDung obj)
        {
            LoaiNguoiDung loainguoidung = GetByID(obj.IDLoaiNguoiDung);
            if (loainguoidung != null)
            {
                var filter = Builders<LoaiNguoiDung>.Filter.Eq("_id", obj._id);
                var update = Builders<LoaiNguoiDung>.Update
                    .Set("Ten", obj.Ten);
                _dbLoaiNguoiDung.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            LoaiNguoiDung loainguoidung = GetByID(id);
            if (loainguoidung != null)
            {
                var filter = Builders<DonHang>.Filter.Eq("_id", loainguoidung._id);
                var result = _dbDonHang.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }


        public IEnumerable<LoaiNguoiDung> ListSimple(string searching)
        {
            var list = getDataLoaiNguoiDung()
                .Where(x => x.Ten.ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.Ten);
            return list;
        }

        public IEnumerable<LoaiNguoiDung> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<LoaiNguoiDung>(PageNum, PageSize);
            return list;
        }
    }
}