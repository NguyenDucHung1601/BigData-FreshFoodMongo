using FreshFoodMongo.Models.DTO;
using MongoDB.Driver;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class MaGiamGiaDAO : BaseDAO
    {
        public MaGiamGiaDAO()
        {
            _dbMaGiamGia = getDBMaGiamGia();
        }

        public IEnumerable<MaGiamGia> ListMaGiamGia()
        {
            return getDataMaGiamGia();
        }

        public MaGiamGia GetByID(Guid id)
        {
            return getDataMaGiamGia().Where(x => x.IDMaGiamGia == id).FirstOrDefault();
        }

        public string GetMaSo(Guid id)
        {
            return GetByID(id).CodeGiamGia;
        }

        public void Add(MaGiamGia obj)
        {
            _dbMaGiamGia.InsertOne(obj);
        }

        public void Edit(MaGiamGia obj)
        {
            MaGiamGia magiamgia = GetByID(obj.IDMaGiamGia);
            if (magiamgia != null)
            {
                var filter = Builders<MaGiamGia>.Filter.Eq("_id", obj._id);
                var update = Builders<MaGiamGia>.Update
                    .Set("CodeGiamGia", obj.CodeGiamGia)
                    .Set("TienGiam", obj.TienGiam)
                    .Set("DieuKienApDung", obj.DieuKienApDung)
                    .Set("HanSuDung", obj.HanSuDung);
                _dbMaGiamGia.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            MaGiamGia magiamgia = GetByID(id);
            if (magiamgia != null)
            {
                var filter = Builders<MaGiamGia>.Filter.Eq("_id", magiamgia._id);
                var result = _dbMaGiamGia.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }


        public IEnumerable<MaGiamGia> ListSimple(string searching)
        {
            var list = getDataMaGiamGia()
                .Where(x => x.CodeGiamGia.ToLower().Contains(searching.ToLower())
                        || x.TienGiam.ToString().ToLower().Contains(searching.ToLower())
                        || x.HanSuDung.ToString().ToLower().Contains(searching.ToLower())
                        || x.CreatedDate.ToString().ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.CreatedDate);
            return list;
        }

        public IEnumerable<MaGiamGia> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<MaGiamGia>(PageNum, PageSize);

            return list;
        }
    }
}