using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class TheLoaiController : BaseController
    {
        TheLoaiDAO tlDao = new TheLoaiDAO();

        // GET: Admin/TheLoai
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
            ViewBag.Count = tlDao.ListSimple(searching).Count();
            return View(tlDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            TheLoai theloai = tlDao.GetByID(id);
            return View(theloai);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string maso, string ten, string mota)
        {
            TheLoai theloai = new TheLoai();
            theloai.IDTheLoai = Guid.NewGuid();
            theloai.MaSo = maso;
            theloai.Ten = ten;
            theloai.MoTa = mota;

            if (ModelState.IsValid)
            {
                tlDao.Add(theloai);
                return RedirectToAction("Index");
            }
            else
            {
                return View(theloai);
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(tlDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string maso, string ten, string mota)
        {
            TheLoai theloai = tlDao.GetByID(id);
            theloai.MaSo = maso;
            theloai.Ten = ten;
            theloai.MoTa = mota;

            if (ModelState.IsValid)
            {
                tlDao.Edit(theloai);
                return RedirectToAction("Index");
            }
            else
            {
                return View(theloai);
            }
        }

        public ActionResult Delete(Guid id)
        {
            tlDao.Delete(id);
            return RedirectToAction("Index");
        }

    }
}