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
    public class DonHangDAO : BaseDAO
    {
        CommonDAO commonDao = new CommonDAO();
        public DonHangDAO()
        {
            _dbDonHang = getDBDonHang();
        }

        public List<int> ThongKeDonHang()
        {
            var res = getDataDonHang()
                .GroupBy(x => x.CreatedDate.Value.Month)
                .OrderBy(x => x.Key)
                .Select(x => x.Count()).ToList();
            return res;
        }
        public IEnumerable<DonHang> ListDonHang()
        {
            return getDataDonHang();
        }

        public DonHang GetByID(Guid id)
        {
            return getDataDonHang().Where(x => x.IDDonHang == id).FirstOrDefault();
        }

        public void Add(DonHang obj)
        {
            _dbDonHang.InsertOne(obj);
        }

        public void Edit(DonHang obj)
        {
            DonHang donHang = GetByID(obj.IDDonHang);
            if (donHang != null) {
                var filter = Builders<DonHang>.Filter.Eq("_id", obj._id);
                var update = Builders<DonHang>.Update
                    .Set("IDDonHang", obj.IDDonHang)
                    .Set("MaSo", obj.MaSo)
                    .Set("IDKhachHang", obj.IDKhachHang)
                    .Set("GhiChu", obj.GhiChu)
                    .Set("TienHang", obj.TienHang)
                    .Set("TienShip", obj.TienShip)
                    .Set("TienGiam", obj.TienGiam)
                    .Set("TongTien", obj.TongTien)
                    .Set("IDTrangThai", obj.IDTrangThai)
                    .Set("IDPhuongThucThanhToan", obj.IDPhuongThucThanhToan)
                    .Set("CreatedDate", obj.CreatedDate);
                _dbDonHang.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            DonHang donHang = GetByID(id);
            if (donHang != null)
            {
                var filter = Builders<DonHang>.Filter.Eq("_id", donHang._id);
                var result = _dbDonHang.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<DonHang> ListSimple(string searching)
        {
            var list = getDataDonHang()
                .Where(x => x.TienGiam.ToString().ToLower().ToLower().Contains(searching.ToLower())
                        || x.TienShip.ToString().ToLower().ToLower().Contains(searching.ToLower())
                        || x.TienHang.ToString().ToLower().ToLower().Contains(searching.ToLower())
                        || x.TongTien.ToString().ToLower().ToLower().Contains(searching.ToLower())
                        || x.CreatedDate.ToString().ToLower().ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenNguoiDung(x.IDKhachHang).ToLower().ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenTrangThai(x.IDTrangThai).ToLower().ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenPhuongThucThanhToan(x.IDPhuongThucThanhToan).ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.CreatedDate);
            return list;
        }

        public IEnumerable<DonHang> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<DonHang>(PageNum, PageSize);
            return list;
        }

        public IEnumerable<DonHang> ListSimpleSearchClient(int PageNum, int PageSize, string searching)
        {
            var list = getDataDonHang()
                 .Where(x => x.TienGiam.ToString().ToLower().Contains(searching.ToLower())
                        || x.TienShip.ToString().ToLower().Contains(searching.ToLower())
                        || x.TienHang.ToString().ToLower().Contains(searching.ToLower())
                        || x.TongTien.ToString().ToLower().Contains(searching.ToLower())
                        || x.CreatedDate.ToString().ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenNguoiDung(x.IDKhachHang).ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenTrangThai(x.IDTrangThai).ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenPhuongThucThanhToan(x.IDPhuongThucThanhToan).ToLower().Contains(searching.ToLower())
                        || x.IDKhachHang.ToString().ToLower().Contains(searching.ToLower()))
                 .OrderBy(x => x.CreatedDate)
                 .ToPagedList<DonHang>(PageNum, PageSize);
            return list;
        }

        // public int TongDonHang(DateTime startDate, DateTime endDate)
        public int TongDonHang()
        {
            var count = getDataDonHang().Count();
            return count;
        }

        public decimal DoanhThu()
        {
            var totalMoney = getDataDonHang().Sum(x => x.TongTien);
            return Convert.ToDecimal(totalMoney);
        }

        public void AdminXacNhanDonHang(DonHang obj)
        {   // cập nhật trạng thái => đang giao hàng
            var donHang = GetByID(obj.IDDonHang);
            donHang.IDTrangThai = new Guid("3240c2e6-fc6c-4feb-9313-382bd05cf522");

            var filter = Builders<DonHang>.Filter.Eq("_id", obj._id);
            var update = Builders<DonHang>.Update
                .Set("IDTrangThai", obj.IDTrangThai);
            _dbDonHang.UpdateOne(filter, update);
        }
    }
}