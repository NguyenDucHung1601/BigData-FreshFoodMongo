using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MongoDB.Driver;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class TrangThaiDAO : BaseDAO
    {
        public TrangThaiDAO()
        {
            _dbTrangThai = getDBTrangThai();
        }

        public IEnumerable<TrangThai> ListLoaiTrangThai()
        {
            return getDataTrangThai();
        }

        public TrangThai GetByID(Guid id)
        {
            return getDataTrangThai().FirstOrDefault(x => x.IDTrangThai == id);
        }
        public void Add(TrangThai obj)
        {
            _dbTrangThai.InsertOne(obj);
        }

        public void Edit(TrangThai obj)
        {
            TrangThai trangthai = GetByID(obj.IDTrangThai);
            if (trangthai != null)
            {
                var filter = Builders<TrangThai>.Filter.Eq("_id", obj._id);
                var update = Builders<TrangThai>.Update
                    .Set("TenTrangThai", obj.TenTrangThai);
                _dbTrangThai.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            TrangThai trangthai = GetByID(id);
            if (trangthai != null)
            {
                var filter = Builders<TrangThai>.Filter.Eq("_id", trangthai._id);
                var result = _dbTrangThai.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }


        public IEnumerable<TrangThai> ListSimple(string searching)
        {
            var list = getDataTrangThai().
                Where(x => x.TenTrangThai.ToLower().Contains(searching.ToLower())).
                OrderBy(x => x.TenTrangThai);
            return list;
        }

        public IEnumerable<TrangThai> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<TrangThai>(PageNum, PageSize);
            return list;
        }
    }
}