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
    public class ChiTietDonHangDAO : BaseDAO
    {
        public ChiTietDonHangDAO()
        {
            _dbChiTietDonHang = getDBChiTietDonHang();
        }

        public IEnumerable<ChiTietDonHang> GetListChiTietDonHang(Guid id)
        {
            return getDataChiTietDonHang().Where(x => x.IDDonHang == id);
        }

        public void Add(ChiTietDonHang obj)
        {
            _dbChiTietDonHang.InsertOne(obj);
        }
    }
}