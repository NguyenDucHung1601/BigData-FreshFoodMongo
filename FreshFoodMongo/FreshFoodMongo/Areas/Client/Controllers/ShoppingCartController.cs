using FreshFoodMongo.Models.DAO.Client;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreshFoodMongo.Common;
using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DAO;

namespace FreshFoodMongo.Areas.Client.Controllers
{
    public class ShoppingCartController : BaseController
    {
        BaseDAO baseDao = new BaseDAO();
        CommonDAO commonDao = new CommonDAO();
        GioHangDAO ghDao = new GioHangDAO();
        SanPhamDAO spDao = new SanPhamDAO();
        NguoiDungDAO ndDao = new NguoiDungDAO();
        ChiTietGioHangDAO ctghDao = new ChiTietGioHangDAO();
        // GET: Client/ShoppingCart
        public ActionResult Index(Guid id)
        {
            var list = baseDao.getDataChiTietGioHang().Where(x => x.IDKhachHang == id).OrderByDescending(x => x.ModifiedDate);
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(FormCollection c)
        {
            Guid idkhachhang = Guid.NewGuid();
            int i = 0;
            if (ModelState.IsValid)
            {
                var userIDCTGioHang = c.GetValues("item.IDChiTietGioHang");
                var userSoLuong = c.GetValues("item.SoLuong");

                for (i = 0; i < userIDCTGioHang.Count(); i++)
                {
                    string idCTGioHang = userIDCTGioHang[i];
                    ChiTietGioHang obj = baseDao.getDataChiTietGioHang().FirstOrDefault(x => x.IDChiTietGioHang.ToString() == idCTGioHang);

                    if (obj.SoLuong != Convert.ToInt32(userSoLuong[i]))
                    {
                        obj.ModifiedDate = DateTime.Now;
                        obj.ModifiedBy = (string)Session["USERNAME_SESSION"];
                    }

                    obj.SoLuong = Convert.ToInt32(userSoLuong[i]);
                    obj.ThanhTien = obj.SoLuong * commonDao.getRf_GiaKhuyenMaiSanPham(obj.IDSanPham);

                    idkhachhang = obj.IDKhachHang;

                    ghDao.Edit(obj);


                    if (obj.SoLuong == 0)
                    {
                        NguoiDung nguoidung = ndDao.GetByID(obj.IDKhachHang);
                        nguoidung.TongTienGioHang -= obj.ThanhTien;
                        ctghDao.Delete(obj.IDChiTietGioHang);
                    }
                ctghDao.Edit(obj);
                }
            }
            var list = baseDao.getDataChiTietGioHang().Where(x => x.IDKhachHang == idkhachhang).OrderByDescending(x => x.ModifiedDate).ToList();
            return View(list);
        }

        public ActionResult AddItem(Guid productId, int quantity)
        {
            ChiTietGioHang obj = new ChiTietGioHang();
            obj.IDChiTietGioHang = Guid.NewGuid();
            obj.IDSanPham = productId;
            obj.IDKhachHang = (Guid)Session["IDUSER_SESSION"];
            obj.SoLuong = quantity;
            obj.DuocChon = false;
            obj.ThanhTien = obj.SoLuong * (spDao.GetByID(productId)).GiaKhuyenMai;
            obj.CreatedDate = DateTime.Now;
            obj.CreatedBy = (string)Session["USERNAME_SESSION"];
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = (string)Session["USERNAME_SESSION"];

            IEnumerable<ChiTietGioHang> listCTGioHang = ghDao.GetListChiTietGioHangByIDKhachHang((Guid)Session["IDUSER_SESSION"]);
            foreach (var item in listCTGioHang)
            {
                if (obj.IDSanPham == item.IDSanPham)
                {
                    obj.IDChiTietGioHang = item.IDChiTietGioHang;
                    obj.SoLuong = item.SoLuong + obj.SoLuong;
                    obj.ThanhTien = obj.SoLuong * (spDao.GetByID(productId)).GiaKhuyenMai;
                    if (ghDao.KTGIOHANG(obj))
                    {
                        ghDao.Edit(obj);
                        return RedirectToAction("Index", "ShoppingCart", new { id = Session["IDUSER_SESSION"] });
                    }
                    else
                    {
                        // fail ...
                    }
                }
            }

            if (ghDao.KTGIOHANG(obj))
            {
                ghDao.Add(obj);
            }
            else
            {
                // fail ...
            }
            return RedirectToAction("Index", "ShoppingCart", new { id = Session["IDUSER_SESSION"] });
        }
        public ActionResult DeleteItem(Guid id)
        {
            ChiTietGioHang obj = ctghDao.GetByID(id);
            NguoiDung nguoidung = ndDao.GetByID(obj.IDKhachHang);
            nguoidung.TongTienGioHang -= obj.ThanhTien;
            ctghDao.Delete(id);
            ndDao.Edit(nguoidung);
            return RedirectToAction("Index", new { id = Session["IDUSER_SESSION"] });
        }
    }
}