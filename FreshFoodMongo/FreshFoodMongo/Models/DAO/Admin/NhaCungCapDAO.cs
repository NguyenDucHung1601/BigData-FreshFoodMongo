using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MongoDB.Driver;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class NhaCungCapDAO : BaseDAO
    {
        public NhaCungCapDAO()
        {
            _dbNhaCungCap = getDBNhaCungCap();
        }
        public IEnumerable<NhaCungCap> ListNhaCungCap()
        {
            return getDataNhaCungCap();
        }

        public NhaCungCap GetByID(Guid id)
        {
            return getDataNhaCungCap().FirstOrDefault(x => x.IDNhaCungCap == id);
        }
        public void Add(NhaCungCap obj)
        {
            _dbNhaCungCap.InsertOne(obj);
        }
        public void Edit(NhaCungCap obj)
        {
            NhaCungCap nhacungcap = GetByID(obj.IDNhaCungCap);
            if (nhacungcap!=null)
            {
                var filter = Builders<NhaCungCap>.Filter.Eq("_id", obj._id);
                var update = Builders<NhaCungCap>.Update
                    .Set("Ten", obj.Ten)
                    .Set("DiaChi", obj.DiaChi)
                    .Set("HinhAnh", obj.HinhAnh)
                    .Set("DienThoai", obj.DienThoai)
                    .Set("CreatedDate", obj.CreatedDate);
                _dbNhaCungCap.UpdateOne(filter, update);
            }
        }
        public long Delete(Guid id)
        {
            NhaCungCap nhacungcap = GetByID(id);
            if (nhacungcap != null)
            {
                var filter = Builders<NhaCungCap>.Filter.Eq("_id", nhacungcap._id);
                var result = _dbNhaCungCap.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }
        public IEnumerable<NhaCungCap> ListSimple(string searching)
        {
            var list = getDataNhaCungCap()
                .Where(x => x.Ten.ToLower().Contains(searching.ToLower())
                        || x.DiaChi.ToLower().Contains(searching.ToLower())
                        || x.DienThoai.ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.Ten);
            return list;
        }

        public IEnumerable<NhaCungCap> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<NhaCungCap>(PageNum, PageSize);
            return list;
        }
    }
}