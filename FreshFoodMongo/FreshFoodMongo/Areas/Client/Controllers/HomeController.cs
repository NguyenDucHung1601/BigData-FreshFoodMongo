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
    public class HomeController : Controller
    {
        BaseDAO baseDao = new BaseDAO();
        TheLoaiDAO tlDao = new TheLoaiDAO();

        // GET: Client/Home
        public ActionResult Index(string searching)
        {
            IEnumerable<SanPham> list;
            ViewBag.Searching = searching;
            if (!string.IsNullOrEmpty(searching))
                list = baseDao.getDataSanPham().Where(x => x.Ten.ToLower().Contains(searching.ToLower())).ToList();
            else
                list = baseDao.getDataSanPham().ToList();
            ViewBag.SearchList = list;
            return View(list);
        }

        public ActionResult IndexByCategory(Guid idcategory)
        {
            IEnumerable<SanPham> list;
            list = baseDao.getDataSanPham().Where(x => x.IDTheLoai == idcategory).ToList();
            ViewBag.CategoryName = (tlDao.GetByID(idcategory)).Ten;
            return View(list);
        }

        public ActionResult CategoryShow()
        {
            return PartialView(baseDao.getDataTheLoai().ToList());
        }

        public ActionResult CategoryShowImage()
        {
            return PartialView(baseDao.getDataTheLoai().ToList());
        }

        public ActionResult ListCategoryShow()
        {
            return PartialView(baseDao.getDataTheLoai().ToList());
        }

        public ActionResult HeaderCart()
        {
            return PartialView();
        }

        public ActionResult ProductTopNew()
        {
            return PartialView(baseDao.getDataSanPham().OrderByDescending(p => p.ModifiedDate).Take(9));
        }

        public ActionResult ProductTopSale()
        {
            return PartialView(baseDao.getDataSanPham().OrderByDescending(p => p.SoLuotMua).Take(9));
        }

        public ActionResult ProductTopReview()
        {
            return PartialView(baseDao.getDataSanPham().OrderByDescending(p => p.SoLuotXem).Take(9));
        }

        public ActionResult SaleOff()
        {
            return PartialView(baseDao.getDataSanPhamKhuyenMai().Where(x => x.ThoiGianKetThuc < DateTime.Now).ToList());
        }
    }
}