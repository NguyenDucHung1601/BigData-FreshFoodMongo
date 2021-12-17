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
    public class ChiTietHoaDonNhapDAO : BaseDAO
    {
        HoaDonNhapDAO hdnDao = new HoaDonNhapDAO();

        public ChiTietHoaDonNhapDAO()
        {
            _dbChiTietHoaDonNhap = getDBChiTietHoaDonNhap();
        }

        public IEnumerable<ChiTietHoaDonNhap> GetListChiTietHDNByIDHDN(Guid id)
        {
            return getDataChiTietHoaDonNhap().Where(x => x.IDHoaDonNhap == id);
        }

        public IEnumerable<ChiTietHoaDonNhap> ListChiTietHoaDonNhap()
        {
            return getDataChiTietHoaDonNhap();
        }

        public ChiTietHoaDonNhap GetByID(Guid id)
        {
            return getDataChiTietHoaDonNhap().Where(x => x.IDChiTietHoaDonNhap == id).FirstOrDefault();
        }

        public void Add(ChiTietHoaDonNhap obj)
        {
            _dbChiTietHoaDonNhap.InsertOne(obj);

            HoaDonNhap hdNhap = hdnDao.GetByID(obj.IDHoaDonNhap);
            hdNhap.TienHang = hdNhap.TienHang + obj.ThanhTien;
            hdNhap.TongTien = hdNhap.TienHang + hdNhap.TienShip + hdNhap.TienGiam;
            hdnDao.Edit(hdNhap);
        }

        public void Edit(ChiTietHoaDonNhap obj, decimal? thanhTienCu)
        {
            ChiTietHoaDonNhap chitietHDN = GetByID(obj.IDChiTietHoaDonNhap);
            if (chitietHDN != null)
            {
                var filter = Builders<ChiTietHoaDonNhap>.Filter.Eq("_id", obj._id);
                var update = Builders<ChiTietHoaDonNhap>.Update
                    .Set("SoLuong", obj.SoLuong)
                    .Set("ThanhTien", obj.ThanhTien);
                _dbChiTietHoaDonNhap.UpdateOne(filter, update);

                HoaDonNhap hdNhap = hdnDao.GetByID(obj.IDHoaDonNhap);
                hdNhap.TienHang = hdNhap.TienHang - (thanhTienCu - chitietHDN.ThanhTien);
                hdNhap.TongTien = hdNhap.TienHang + hdNhap.TienShip + hdNhap.TienGiam;
                hdnDao.Edit(hdNhap);
            }
        }

        public long Delete(Guid id)
        {
            ChiTietHoaDonNhap chitietHDN = GetByID(id);
            if (chitietHDN != null)
            {
                var filter = Builders<ChiTietHoaDonNhap>.Filter.Eq("_id", chitietHDN._id);
                var result = _dbChiTietHoaDonNhap.DeleteOne(filter);

                HoaDonNhap hdNhap = hdnDao.GetByID(chitietHDN.IDHoaDonNhap);
                hdNhap.TienHang = hdNhap.TienHang - chitietHDN.ThanhTien;
                hdNhap.TongTien = hdNhap.TienHang + hdNhap.TienShip + hdNhap.TienGiam;
                hdnDao.Edit(hdNhap);

                return result.DeletedCount;
            }
            else
            {
                return -1;
            }
        }
    }
}