using FreshFoodMongo.Models.DAO;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Client.Controllers
{
    public class ShopController : BaseController
    {
        BaseDAO baseDao = new BaseDAO();
        // GET: Client/Shop
        public ActionResult Index(string name, string idcategory, string priceFrom, string priceTo)
        {
            ViewBag.ProductName = name;
            ViewBag.IdCategory = idcategory;
            ViewBag.PriceFrom = priceFrom;
            ViewBag.PriceTo = priceTo;

            SelectList theloaiList = new SelectList(baseDao.getDataTheLoai(), "IDTheLoai", "Ten", "IDTheLoai");
            ViewBag.CategoryList = theloaiList;

            IEnumerable<SanPham> list = baseDao.getDataSanPham(); ;
            if (!string.IsNullOrEmpty(name))
                list = list.Where(x => x.Ten.ToLower().Contains(name.ToLower())).ToList();
            if (!string.IsNullOrEmpty(idcategory))
                list = list.Where(x => x.IDTheLoai.ToString().Equals(idcategory)).ToList();
            if (!string.IsNullOrEmpty(priceFrom) && !string.IsNullOrEmpty(priceTo))
                if (Convert.ToDecimal(priceFrom) < Convert.ToDecimal(priceTo))
                    list = list.Where(x => x.GiaKhuyenMai >= Convert.ToDecimal(priceFrom) && x.GiaKhuyenMai <= Convert.ToDecimal(priceTo)).ToList();
            return View(list);
        }

        public ActionResult ListCategoryShow()
        {
            return PartialView(baseDao.getDataTheLoai());
        }

        public ActionResult ProductTopNew()
        {
            return PartialView(baseDao.getDataSanPham().OrderByDescending(p => p.ModifiedDate).Take(9));
        }

        public ActionResult ProductTopSale()
        {
            return PartialView(baseDao.getDataSanPham().OrderByDescending(p => p.SoLuotMua).Take(9));
        }

        public ActionResult SaleOff()
        {
            return PartialView(baseDao.getDataSanPhamKhuyenMai().Where(x => x.ThoiGianKetThuc < DateTime.Now).ToList());
        }
    }
}