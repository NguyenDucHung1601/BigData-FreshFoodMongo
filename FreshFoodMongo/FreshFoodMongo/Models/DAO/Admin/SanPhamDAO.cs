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
    public class SanPhamDAO : BaseDAO
    {
        CommonDAO commonDao = new CommonDAO();
        public SanPhamDAO()
        {
            _dbSanPham = getDBSanPham();
        }

        public IEnumerable<SanPham> ListSanPham()
        {
            return getDataSanPham();
        }

        public SanPham GetByID(Guid id)
        {
            return getDataSanPham().FirstOrDefault(x => x.IDSanPham == id);
        }

        public string GetIdentity()
        {
            int idOrder = getDataSanPham().Select(x => x.MaOrder).Max();
            return idOrder.ToString("D4");

            //int idOrder = Convert.ToInt32(db.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('SanPham')").FirstOrDefault());
            //return idOrder.ToString("D4");
        }

        public void Add(SanPham obj)
        {
            _dbSanPham.InsertOne(obj);
        }

        public void Edit(SanPham obj)
        {
            SanPham sanpham = GetByID(obj.IDSanPham);
            if (sanpham != null)
            {
                var filter = Builders<SanPham>.Filter.Eq("_id", obj._id);
                var update = Builders<SanPham>.Update
                    .Set("MaSo", obj.MaSo)
                    .Set("Ten", obj.Ten)
                    .Set("DonViTinh", obj.DonViTinh)
                    .Set("GiaTien", obj.GiaTien)
                    .Set("GiaKhuyenMai", obj.GiaKhuyenMai)
                    .Set("HinhAnh", obj.HinhAnh)
                    .Set("MoTa", obj.MoTa)
                    .Set("CoSan", obj.CoSan)
                    .Set("SoLuong", obj.SoLuong)
                    .Set("CreatedDate", obj.CreatedDate)
                    .Set("IDTheLoai", obj.IDTheLoai);
                _dbSanPham.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            SanPham sanpham = GetByID(id);
            if (sanpham != null)
            {
                var filter = Builders<SanPham>.Filter.Eq("_id", sanpham._id);
                var result = _dbSanPham.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }
        public IEnumerable<SanPham> ListSimple(string searching)
        {
            var list = getDataSanPham()
                .Where(x => x.MaSo.ToLower().Contains(searching.ToLower())
                        || x.Ten.ToLower().Contains(searching.ToLower())
                        || x.DonViTinh.ToLower().Contains(searching.ToLower())
                        || x.GiaTien.ToString().ToLower().Contains(searching.ToLower())
                        || x.GiaKhuyenMai.ToString().ToLower().Contains(searching.ToLower())
                        || x.SoLuong.ToString().ToLower().Contains(searching.ToLower())
                        || x.SoLuotXem.ToString().ToLower().Contains(searching.ToLower())
                        || x.SoLuotMua.ToString().ToLower().Contains(searching.ToLower())
                        || x.ModifiedDate.ToString().ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenTheLoai(x.IDTheLoai).ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.ModifiedDate);

            return list;
        }

        public IEnumerable<SanPham> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<SanPham>(PageNum, PageSize);

            return list;
        }
    }
}