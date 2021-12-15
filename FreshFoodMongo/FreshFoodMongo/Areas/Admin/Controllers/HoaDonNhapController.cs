using FreshFoodMongo.Models.DAO;
using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class HoaDonNhapController : Controller
    {
        BaseDAO baseDao = new BaseDAO();
        HoaDonNhapDAO hdnDao = new HoaDonNhapDAO();
        ChiTietHoaDonNhapDAO cthdnDao = new ChiTietHoaDonNhapDAO();

        // GET: Admin/HoaDonNhap
        public ActionResult Index(int? page, int? PageSize, string searching = "")
        {
            ViewBag.SearchString = searching;
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" }
            };
            int pageNumber = (page ?? 1);
            int pagesize = (PageSize ?? 10);
            ViewBag.psize = pagesize;
            ViewBag.Count = hdnDao.ListSimple(searching).Count();
            return View(hdnDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            HoaDonNhap hoadonnhap = hdnDao.GetByID(id);
            return View(hoadonnhap);
        }

        public ActionResult Create()
        {
            ViewBag.ListNhaCungCap = new SelectList(baseDao.getDataNhaCungCap(), "IDNhaCungCap", "Ten", "IDNhaCungCap");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Guid idnhacungcap, string ghichu)
        {
            HoaDonNhap hoadonnhap = new HoaDonNhap();
            hoadonnhap.IDHoaDonNhap = Guid.NewGuid();
            hoadonnhap.IDNhaCungCap = idnhacungcap;
            hoadonnhap.GhiChu = ghichu;
            hoadonnhap.TienHang = 0;
            hoadonnhap.TienShip = 0;
            hoadonnhap.TienGiam = 0;
            hoadonnhap.TongTien = 0;

            hoadonnhap.CreatedDate = DateTime.Now;
            hoadonnhap.CreatedBy = (string)Session["USERNAME_SESSION"];
            hoadonnhap.ModifiedDate = DateTime.Now;
            hoadonnhap.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                hdnDao.Add(hoadonnhap);
                return RedirectToAction("Index");
            }
            else
            {
                return View(hoadonnhap);
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(hdnDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string ghichu)
        {
            HoaDonNhap hoadonnhap = hdnDao.GetByID(id);
            hoadonnhap.GhiChu = ghichu;

            hoadonnhap.ModifiedDate = DateTime.Now;
            hoadonnhap.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                hdnDao.Edit(hoadonnhap);
                return RedirectToAction("Index");
            }
            return View(hoadonnhap);
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                hdnDao.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("ErrorConstraint");
            }
        }

        public ActionResult IndexChiTietHoaDonNhap(Guid idhdn, Guid idncc)
        {
            var listChiTietHoaDonNhap = cthdnDao.GetListChiTietHDNByIDHDN(idhdn);

            if (listChiTietHoaDonNhap == null)
                return HttpNotFound();

            ViewBag.IDHoaDonNhap = idhdn;
            ViewBag.IDNhaCungCap = idncc;

            return PartialView(listChiTietHoaDonNhap);
        }
    }
}