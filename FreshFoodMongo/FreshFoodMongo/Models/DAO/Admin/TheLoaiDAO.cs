using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MongoDB.Driver;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class TheLoaiDAO : BaseDAO
    { 
        public TheLoaiDAO()
        {
            _dbTheLoai = getDBTheLoai();
        }

        public IEnumerable<TheLoai> ListTheLoai()
        {
            return getDataTheLoai();
        }

        public TheLoai GetByID(Guid id)
        {
            return getDataTheLoai().FirstOrDefault(x => x.IDTheLoai == id);
        }

        public string GetMaSo(Guid id)
        {
            return getDataTheLoai().FirstOrDefault(x => x.IDTheLoai == id).MaSo;
        }

        public void Add(TheLoai obj)
        {
            _dbTheLoai.InsertOne(obj);
        }

        public void Edit(TheLoai obj)
        {
            TheLoai theloai = GetByID(obj.IDTheLoai);
            if (theloai != null)
            {
                var filter = Builders<TheLoai>.Filter.Eq("_id", obj._id);
                var update = Builders<TheLoai>.Update
                    .Set("MaSo", obj.MaSo)
                    .Set("Ten", obj.Ten)
                    .Set("MoTa", obj.MoTa);
                _dbTheLoai.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            TheLoai theloai = GetByID(id);
            if (theloai != null)
            {
                var filter = Builders<TheLoai>.Filter.Eq("_id", theloai._id);
                var result = _dbTheLoai.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }


        public IEnumerable<TheLoai> ListSimple(string searching)
        {
            var list = getDataTheLoai()
                .Where(x => x.Ten.ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.Ten);
            return list;
        }

        public IEnumerable<TheLoai> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<TheLoai>(PageNum, PageSize);
            return list;
        }
    }
}