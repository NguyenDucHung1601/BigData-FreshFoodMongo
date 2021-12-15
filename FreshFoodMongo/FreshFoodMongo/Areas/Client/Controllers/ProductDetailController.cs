using FreshFoodMongo.Models.DAO;
using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Client.Controllers
{
    public class ProductDetailController : Controller
    {
        BaseDAO baseDao = new BaseDAO();
        SanPhamDAO spDao = new SanPhamDAO();
        // GET: Client/ProductDetail
        public ActionResult Index(Guid id)
        {
            SanPham sanpham = spDao.GetByID(id);
            sanpham.SoLuotXem = sanpham.SoLuotXem + 1;
            spDao.Edit(sanpham);

            ViewBag.Product = sanpham;

            ChiTietGioHang obj = new ChiTietGioHang();
            obj.IDChiTietGioHang = Guid.NewGuid();
            obj.IDSanPham = id;
            obj.SoLuong = 1;

            return View(obj);
        }

        public ActionResult RelatedProduct(Guid idtheloai, string ten)
        {
            IList<SanPham> list = spDao.ListSanPham().Where(x => x.IDTheLoai == idtheloai).ToList();

            return PartialView(list);
        }
    }
}