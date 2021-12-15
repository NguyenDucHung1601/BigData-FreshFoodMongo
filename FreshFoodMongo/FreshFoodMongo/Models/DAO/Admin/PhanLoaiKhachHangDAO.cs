using FreshFoodMongo.Models.DTO;
using MongoDB.Driver;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class PhanLoaiKhachHangDAO : BaseDAO
    {
        public PhanLoaiKhachHangDAO()
        {
            _dbPhanLoaiKhachHang = getDBPhanLoaiKhachHang();
        }

        public IEnumerable<PhanLoaiKhachHang> ListPhanLoaiKhachHang()
        {
            return getDataPhanLoaiKhachHang().OrderBy(x => x.CapDo);
        }

        public PhanLoaiKhachHang GetByID(Guid id)
        {
            return getDataPhanLoaiKhachHang().FirstOrDefault(x => x.IDLoaiKhachHang == id);
        }

        public void Add(PhanLoaiKhachHang obj)
        {
            _dbPhanLoaiKhachHang.InsertOne(obj);
        }

        public void Edit(PhanLoaiKhachHang obj)
        {
            PhanLoaiKhachHang phanloaiKH = GetByID(obj.IDLoaiKhachHang);
            if (phanloaiKH != null)
            {
                var filter = Builders<PhanLoaiKhachHang>.Filter.Eq("_id", obj._id);
                var update = Builders<PhanLoaiKhachHang>.Update
                    .Set("CapDo", obj.CapDo)
                    .Set("Ten", obj.Ten)
                    .Set("SoDonHangToiThieu", obj.SoDonHangToiThieu)
                    .Set("TongTienHangToiThieu", obj.TongTienHangToiThieu)
                    .Set("DieuKien", obj.DieuKien);
                _dbPhanLoaiKhachHang.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            PhanLoaiKhachHang phanloaiKH = GetByID(id);
            if (phanloaiKH != null)
            {
                var filter = Builders<PhanLoaiKhachHang>.Filter.Eq("_id", phanloaiKH._id);
                var result = _dbPhanLoaiKhachHang.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }


        public IEnumerable<PhanLoaiKhachHang> ListSimple(string searching)
        {
            var list = getDataPhanLoaiKhachHang()
                .Where(x => x.CapDo.ToString().ToLower().Contains(searching.ToLower())
                        || x.Ten.ToLower().Contains(searching.ToLower()))
                .OrderBy(x=>x.CapDo);
            return list;
        }

        public IEnumerable<PhanLoaiKhachHang> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<PhanLoaiKhachHang>(PageNum, PageSize);
            return list;
        }
    }
}