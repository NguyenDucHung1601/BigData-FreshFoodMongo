using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class PhanLoaiKhachHangController : BaseController
    {
        PhanLoaiKhachHangDAO plkhDao = new PhanLoaiKhachHangDAO();

        // GET: Admin/PhanLoaiKhachHang
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
            ViewBag.Count = plkhDao.ListSimple(searching).Count();
            return View(plkhDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            PhanLoaiKhachHang phanloaikhachhang = plkhDao.GetByID(id);
            return View(phanloaikhachhang);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string capdo, string ten, string sodonhangtoithieu, string tongtienhangtoithieu, string dieukien)
        {
            PhanLoaiKhachHang phanloaikhachhang = new PhanLoaiKhachHang();
            phanloaikhachhang.IDLoaiKhachHang = Guid.NewGuid();
            phanloaikhachhang.CapDo = Convert.ToInt32(capdo);
            phanloaikhachhang.Ten = ten;
            phanloaikhachhang.SoDonHangToiThieu = Convert.ToInt32(sodonhangtoithieu);
            phanloaikhachhang.TongTienHangToiThieu = Convert.ToDecimal(tongtienhangtoithieu);
            phanloaikhachhang.DieuKien = dieukien;
            if (ModelState.IsValid)
            {
                plkhDao.Add(phanloaikhachhang);
                return RedirectToAction("Index");
            }
            else
            {
                return View(phanloaikhachhang);
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(plkhDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string capdo, string ten, string sodonhangtoithieu, string tongtienhangtoithieu, string dieukien)
        {
            PhanLoaiKhachHang phanloaikhachhang = plkhDao.GetByID(id);
            phanloaikhachhang.CapDo = Convert.ToInt32(capdo);
            phanloaikhachhang.Ten = ten;
            phanloaikhachhang.SoDonHangToiThieu = Convert.ToInt32(sodonhangtoithieu);
            phanloaikhachhang.TongTienHangToiThieu = Convert.ToDecimal(tongtienhangtoithieu);
            phanloaikhachhang.DieuKien = dieukien;
            if (ModelState.IsValid)
            {
                plkhDao.Edit(phanloaikhachhang);
                return RedirectToAction("Index");
            }
            else
            {
                return View(phanloaikhachhang);
            }
        }

        public ActionResult Delete(Guid id)
        {
            plkhDao.Delete(id);
            return RedirectToAction("Index");
        }

    }
}