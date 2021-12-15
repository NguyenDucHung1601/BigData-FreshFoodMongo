using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MongoDB.Driver;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class PhuongThucThanhToanDAO : BaseDAO
    {
        public PhuongThucThanhToanDAO()
        {
            _dbPhuongThucThanhToan = getDBPhuongThucThanhToan();
        }

        public IEnumerable<PhuongThucThanhToan> ListPhuongThucThanhToan()
        {
            return getDataPhuongThucThanhToan();
        }

        public PhuongThucThanhToan GetByID(Guid id)
        {
            return getDataPhuongThucThanhToan().FirstOrDefault(x => x.IDPhuongThucThanhToan == id);
        }
        public void Add(PhuongThucThanhToan obj)
        {
            _dbPhuongThucThanhToan.InsertOne(obj);
        }

        public void Edit(PhuongThucThanhToan obj)
        {
            PhuongThucThanhToan phuongthucthanhtoan = GetByID(obj.IDPhuongThucThanhToan);
            if (phuongthucthanhtoan != null)
            {
                var filter = Builders<PhuongThucThanhToan>.Filter.Eq("_id", obj._id);
                var update = Builders<PhuongThucThanhToan>.Update
                    .Set("TenPhuongThucThanhToan", obj.TenPhuongThucThanhToan);
                _dbPhuongThucThanhToan.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            PhuongThucThanhToan phuongthucthanhtoan = GetByID(id);
            if (phuongthucthanhtoan != null)
            {
                var filter = Builders<PhuongThucThanhToan>.Filter.Eq("_id", phuongthucthanhtoan._id);
                var result = _dbPhuongThucThanhToan.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }


        public IEnumerable<PhuongThucThanhToan> ListSimple(string searching)
        {
            var list = getDataPhuongThucThanhToan()
                .Where(x => x.TenPhuongThucThanhToan.ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.TenPhuongThucThanhToan);
            return list;
        }

        public IEnumerable<PhuongThucThanhToan> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<PhuongThucThanhToan>(PageNum, PageSize);
            return list;
        }
    }
}