using FreshFoodMongo.Models.DTO;
using FreshFoodMongo.Models.DTOplus;
using MongoDB.Driver;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshFoodMongo.Models.DAO.Admin
{
    public class NguoiDungDAO : BaseDAO
    {
        CommonDAO commonDao = new CommonDAO();
        public NguoiDungDAO()
        {
            _dbNguoiDung = getDBNguoiDung();
        }
        public List<NguoiDung> DSKhachHangTiemNang()
        {
            return getDataNguoiDung().Where(x => x.IsAdmin.Equals(false))
                .Where(x => x.TongTienHangDaMua > 500000)
                .OrderByDescending(x => x.TongTienHangDaMua)
                .ToList();
        }
        public NguoiDung GetByUsername(string username)
        {
            return getDataNguoiDung().FirstOrDefault(x => x.Username == username);
        }

        public NguoiDung GetByID(Guid id)
        {
            return getDataNguoiDung().FirstOrDefault(x => x.IDNguoiDung == id);
        }

        public int LoginAdmin(string username, string password)
        {
            var user = GetByUsername(username);
            if (user == null || !user.IsAdmin)
            {
                return 0;
            }
            else
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    var filter = Builders<NguoiDung>.Filter.Eq("_id", user._id);
                    var update = Builders<NguoiDung>.Update
                        .Set("LanHoatDongGanNhat", DateTime.Now);
                    _dbNguoiDung.UpdateOne(filter, update);
                    return 1;
                }
                else
                    return -1;
            }
        }

        public int LoginClient(string username, string password)
        {
            var user = GetByUsername(username);
            if (user == null || user.IsAdmin)
            {
                return 0;
            }
            else
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    var filter = Builders<NguoiDung>.Filter.Eq("_id", user._id);
                    var update = Builders<NguoiDung>.Update
                        .Set("LanHoatDongGanNhat", DateTime.Now);
                    _dbNguoiDung.UpdateOne(filter, update);
                    return 1;
                }
                else
                    return -1;
            }
        }

        public void ChangePassword(NguoiDung obj, string newpass)
        {
            NguoiDung user = GetByID(obj.IDNguoiDung);
            if (user != null)
            {
                var filter = Builders<NguoiDung>.Filter.Eq("_id", user._id);
                var update = Builders<NguoiDung>.Update
                    .Set("Password", BCrypt.Net.BCrypt.HashPassword(newpass));
                _dbNguoiDung.UpdateOne(filter, update);
            }
        }

        public IEnumerable<NguoiDung> ListAdmin()
        {
            return getDataNguoiDung().Where(x => x.IsAdmin == true);
        }

        public IEnumerable<NguoiDung> ListClient()
        {
            return getDataNguoiDung().Where(x => x.IsAdmin == false);
        }

        public IEnumerable<NguoiDung> ListClientCapDoGreaterThan(int capdo)
        {
            var dataClient = getDataNguoiDung().Where(x => x.IsAdmin == false);
            return dataClient.Where(x => commonDao.getRf_CapDoPhanLoaiKhachHang((Guid)x.IDLoaiKhachHang) >= capdo);
        }

        public void UpdatePhanLoaiKhachHang(Guid id)
        {
            NguoiDung kh = GetByID(id);
            var listPhanLoai = (new PhanLoaiKhachHangDAO()).ListPhanLoaiKhachHang();

            if (kh != null)
            {
                foreach (PhanLoaiKhachHang pl in listPhanLoai)
                {
                    if (kh.SoDonHangDaMua >= pl.SoDonHangToiThieu && kh.TongTienHangDaMua >= pl.TongTienHangToiThieu)
                    {
                        kh.IDLoaiKhachHang = pl.IDLoaiKhachHang;
                    }
                }
            }
        }

        public void Add(NguoiDung obj)
        {
            _dbNguoiDung.InsertOne(obj);
        }

        public void Edit(NguoiDung obj)
        {
            NguoiDung nguoidung = GetByID(obj.IDNguoiDung);
            if (nguoidung != null)
            {
                var filter = Builders<NguoiDung>.Filter.Eq("_id", obj._id);
                var update = Builders<NguoiDung>.Update
                    .Set("Ten", obj.Ten)
                    .Set("DienThoai", obj.DienThoai)
                    .Set("Email", obj.Email)
                    .Set("DiaChi", obj.DiaChi)
                    .Set("TongTienGioHang", obj.TongTienGioHang)
                    .Set("Avatar", obj.Avatar);
                _dbNguoiDung.UpdateOne(filter, update);
            }
        }

        public long Delete(Guid id)
        {
            NguoiDung nguoidung = GetByID(id);
            if (nguoidung != null)
            {
                var filter = Builders<NguoiDung>.Filter.Eq("_id", nguoidung._id);
                var result = _dbNguoiDung.DeleteOne(filter);
                return result.DeletedCount;
            }
            else
                return -1;
        }

        public IEnumerable<NguoiDung> ListAccountSimple(string searching)
        {
            var list = getDataNguoiDung()
                .Where(x => x.Username.ToLower().Contains(searching.ToLower())
                        || x.ModifiedDate.ToString().ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenLoaiNguoiDung(x.IDLoaiNguoiDung).ToString().ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.ModifiedBy);
            return list;
        }

        public IEnumerable<NguoiDung> ListAccountSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListAccountSimple(searching).ToPagedList<NguoiDung>(PageNum, PageSize);
            return list;
        }

        public IEnumerable<NguoiDung> ListAdminSimple(string searching)
        {
            var list = getDataNguoiDung()
                .Where(x => x.IsAdmin == true)
                .Where(x => x.Username.ToLower().Contains(searching.ToLower())
                        || x.ModifiedBy.ToLower().Contains(searching.ToLower())
                        || x.ModifiedDate.ToString().ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.ModifiedDate);
            return list;
        }

        public IEnumerable<NguoiDung> ListAdminSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListAdminSimple(searching).ToPagedList<NguoiDung>(PageNum, PageSize);
            return list;
        }

        public int TongSoThanhVien()
        {
            var countUser = getDataNguoiDung().Where(x => x.IsAdmin == false).Count();
            return countUser;
        }
        public IEnumerable<NguoiDung> ListClientSimple(string searching)
        {
            var dataClient = getDataNguoiDung().Where(x => x.IsAdmin == false);
            var list = getDataNguoiDung()
                .Where(x => x.Username.ToLower().Contains(searching.ToLower())
                        || x.Ten.ToLower().Contains(searching.ToLower())
                        || x.SoDonHangDaMua.ToString().ToLower().Contains(searching.ToLower())
                        || x.TongTienHangDaMua.ToString().ToLower().Contains(searching.ToLower())
                        || x.LanHoatDongGanNhat.ToString().ToLower().Contains(searching.ToLower())
                        || x.ModifiedDate.ToString().ToLower().Contains(searching.ToLower())
                        || commonDao.getRf_TenPhanLoaiKhachHang((Guid)x.IDLoaiKhachHang).ToLower().Contains(searching.ToLower()))
                .OrderBy(x => x.ModifiedDate);
            return list;
        }

        public IEnumerable<NguoiDung> ListClientSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = ListClientSimple(searching).ToPagedList<NguoiDung>(PageNum, PageSize);
            return list;
        }

    }
}