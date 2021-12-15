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
    public class NhaCungCapSanPhamDAO : BaseDAO
    {
        CommonDAO commonDao = new CommonDAO();
        public NhaCungCapSanPhamDAO()
        {
            _dbNhaCungCapSanPham = getDBNhaCungCapSanPham();
        }

        public IEnumerable<NhaCungCapSanPham> ListNhaCungCap()
        {
            return getDataNhaCungCapSanPham();
        }
        public IEnumerable<NhaCungCapSanPham> GetListSPCungUngByIDNhaCungCap(Guid idncc)
        {
            return getDataNhaCungCapSanPham().Where(x => x.IDNhaCungCap == idncc);
        }

        public string SPCungUng_GetDonViTinh(Guid? idncc, Guid idsp)
        {
            return getDataNhaCungCapSanPham()
                .Where(x => x.IDNhaCungCap == idncc)
                .Where(x => x.IDSanPham == idsp)
                .Select(x => x.DonViTinh)
                .FirstOrDefault();
        }

        public decimal SPCungUng_GetGiaCungUng(Guid? idncc, Guid idsp)
        {
            return Convert.ToDecimal(getDataNhaCungCapSanPham()
                .Where(x => x.IDNhaCungCap == idncc)
                .Where(x => x.IDSanPham == idsp)
                .Select(x => x.GiaCungUng)
                .FirstOrDefault());
        }

        public NhaCungCapSanPham GetByID(Guid id)
        {
            return getDataNhaCungCapSanPham().FirstOrDefault(x => x.IDNhaCungCapSanPham == id);
        }

        public void Add(NhaCungCapSanPham obj)
        {
            _dbNhaCungCapSanPham.InsertOne(obj);
        }

        public void Edit(NhaCungCapSanPham obj)
        {
            NhaCungCapSanPham nhacungcapsanpham = GetByID(obj.IDNhaCungCapSanPham);
            if (nhacungcapsanpham != null)
            {
                var filter = Builders<NhaCungCapSanPham>.Filter.Eq("_id", obj._id);
                var update = Builders<NhaCungCapSanPham>.Update
                    .Set("DonViTinh", obj.DonViTinh)
                    .Set("GiaCungUng", obj.GiaCungUng);
                _dbNhaCungCapSanPham.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            NhaCungCapSanPham nhacungcapsanpham = GetByID(id);
            if (nhacungcapsanpham != null)
            {
                var filter = Builders<NhaCungCapSanPham>.Filter.Eq("_id", nhacungcapsanpham._id);
                var result = _dbNhaCungCapSanPham.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<NhaCungCapSanPham> ListSimple(string searching)
        {
            var list = getDataNhaCungCapSanPham()
                .Where(x => x.DonViTinh.ToLower().Contains(searching.ToLower())
                        || x.GiaCungUng.ToString().ToLower().Contains(searching.ToLower())
                        || x.NgayCapNhat.ToString().ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenNhaCungCap(x.IDNhaCungCap).ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenSanPham(x.IDSanPham).ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.NgayCapNhat);
            return list;
        }

        public IEnumerable<NhaCungCapSanPham> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListSimple(searching).ToPagedList<NhaCungCapSanPham>(PageNum, PageSize);
            return list;
        }
    }
}