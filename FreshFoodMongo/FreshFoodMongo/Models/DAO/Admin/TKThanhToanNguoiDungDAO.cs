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
    public class TKThanhToanNguoiDungDAO : BaseDAO
    {
        public TKThanhToanNguoiDungDAO()
        {
            _dbTKThanhToanNguoiDung = getDBTKThanhToanNguoiDung();
        }

        public IEnumerable<TKThanhToanNguoiDung> ListTKThanhToanNguoiDung()
        {
            return getDataTKThanhToanNguoiDung();
        }

        public TKThanhToanNguoiDung GetByID(Guid idnd,Guid idtk)
        {
            return getDataTKThanhToanNguoiDung().Where(x => x.IDNguoiDung == idnd).Where(x => x.IDTaiKhoan == idtk).FirstOrDefault();
        }

        public void Add(TKThanhToanNguoiDung obj)
        {
            _dbTKThanhToanNguoiDung.InsertOne(obj);
        }

        public void Edit(TKThanhToanNguoiDung obj)
        {
            TKThanhToanNguoiDung tkthanhtoannguoidung = GetByID(obj.IDNguoiDung,obj.IDTaiKhoan);
            if (tkthanhtoannguoidung != null)
            {
                var filter = Builders<TKThanhToanNguoiDung>.Filter.Eq("_id", obj._id);
                var update = Builders<TKThanhToanNguoiDung>.Update
                    .Set("Username", obj.Username)
                    .Set("Password", obj.Password)
                    .Set("TongTien", obj.TongTien);
                _dbTKThanhToanNguoiDung.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid idnd,Guid idtk )
        {
            TKThanhToanNguoiDung tkthanhtoannguoidung = GetByID(idnd, idtk);
            if (tkthanhtoannguoidung != null)
            {
                var filter = Builders<TKThanhToanNguoiDung>.Filter.Eq("_id", tkthanhtoannguoidung._id);
                var result = _dbTKThanhToanNguoiDung.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }

        public IEnumerable<TKThanhToanNguoiDung> ListSimple(string searching)
        {
            var list = getDataTKThanhToanNguoiDung()
                .Where(x => x.Username.ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.Username);
            return list;
        }

        public IEnumerable<TKThanhToanNguoiDung> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<TKThanhToanNguoiDung>(PageNum, PageSize);
            return list;
        }
    }
}