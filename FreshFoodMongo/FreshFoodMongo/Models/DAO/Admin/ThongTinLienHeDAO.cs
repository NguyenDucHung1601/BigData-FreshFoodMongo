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
    public class ThongTinLienHeDAO : BaseDAO
    {
        public ThongTinLienHeDAO()
        {
            _dbThongTinLienHe = getDBThongTinLienHe();
        }

        public ThongTinLienHe GetInfoObj()
        {
            return getDataThongTinLienHe().FirstOrDefault();
        }

        public Guid GetInfoID()
        {
            return getDataThongTinLienHe().FirstOrDefault().ID;
        }

        public void Edit(ThongTinLienHe obj)
        {
            ThongTinLienHe info = GetInfoObj();
            var filter = Builders<ThongTinLienHe>.Filter.Eq("_id", info._id);
            var update = Builders<ThongTinLienHe>.Update
                .Set("ID", obj.ID)
                .Set("TenCuaHang", obj.TenCuaHang)
                .Set("DiaChi", obj.DiaChi)
                .Set("DienThoai1", obj.DienThoai1)
                .Set("DienThoai2", obj.DienThoai2)
                .Set("GioMoCua", obj.GioMoCua)
                .Set("Email", obj.Email)
                .Set("LinkFacebook", obj.LinkFacebook)
                .Set("LinkYoutube", obj.LinkYoutube)
                .Set("LinkInstagram", obj.LinkInstagram);
            _dbThongTinLienHe.UpdateOne(filter, update);
        }
    }
}