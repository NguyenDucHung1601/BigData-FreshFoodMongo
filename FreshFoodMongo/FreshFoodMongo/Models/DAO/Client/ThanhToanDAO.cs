using FreshFoodMongo.Models.DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DAO.Client
{
    public class ThanhToanDAO : BaseDAO
    {
        //TKThanhToanNguoiDungDAO tkttndDao = new TKThanhToanNguoiDungDAO();

        public ThanhToanDAO()
        {
            _dbTKThanhToanNguoiDung = getDBTKThanhToanNguoiDung();
        }

        public IEnumerable<TKThanhToanNguoiDung> ListTKThanhToanNguoiDung()
        {
            return getDataTKThanhToanNguoiDung();
        }

        public TKThanhToanNguoiDung GetByID(Guid idnd, Guid idtk)
        {
            return getDataTKThanhToanNguoiDung().Where(x => x.IDNguoiDung == idnd).Where(x => x.IDTaiKhoan == idtk).FirstOrDefault();
        }

        public void Add(TKThanhToanNguoiDung obj)
        {
            _dbTKThanhToanNguoiDung.InsertOne(obj);
        }
        public int KTTKHOPLE(TKThanhToanNguoiDung obj, string pass)
        {
            TKThanhToanNguoiDung taikhoanthanhtoannguoidung = GetByID(obj.IDNguoiDung, obj.IDTaiKhoan);
            DonHang donhang = getDataDonHang().Where(x => x.IDKhachHang == obj.IDNguoiDung).FirstOrDefault();
            if (BCrypt.Net.BCrypt.Verify(pass, taikhoanthanhtoannguoidung.Password))
            {
                if (taikhoanthanhtoannguoidung.TongTien >= donhang.TongTien)

                    // view thanh toan thanh cong// cap nhat lai tien trong gio hang
                    taikhoanthanhtoannguoidung.TongTien = taikhoanthanhtoannguoidung.TongTien - donhang.TongTien;
                return 1;
                //else return tai khoan k du tien
            }
            return -1;// view sai mat khau
        }
        public long Delete(Guid idnd, Guid idtk)
        {
            TKThanhToanNguoiDung tkthanhtoannguoidung = getDataTKThanhToanNguoiDung().Where(x => x.IDNguoiDung == idnd).Where(x => x.IDTaiKhoan == idtk).ToList().FirstOrDefault();
            if (tkthanhtoannguoidung != null)
            {
                var filter = Builders<TKThanhToanNguoiDung>.Filter.Eq("_id", tkthanhtoannguoidung._id);
                var result = _dbTKThanhToanNguoiDung.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }
    }
}