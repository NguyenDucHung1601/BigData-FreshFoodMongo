using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class TrangThaiController : BaseController
    {
        TrangThaiDAO ttDao = new TrangThaiDAO();

        // GET: Admin/TrangThai
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
            ViewBag.Count = ttDao.ListSimple(searching).Count();
            return View(ttDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            TrangThai trangthai = ttDao.GetByID(id);
            return View(trangthai);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string ten)
        {
            TrangThai trangthai = new TrangThai();
            trangthai.IDTrangThai = Guid.NewGuid();
            trangthai.TenTrangThai = ten;
            if (ModelState.IsValid)
            {
                ttDao.Add(trangthai);
                return RedirectToAction("Index");
            }
            else
            {
                return View(trangthai);
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(ttDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string ten)
        {
            TrangThai trangthai = ttDao.GetByID(id);
            trangthai.TenTrangThai = ten;
            if (ModelState.IsValid)
            {
                ttDao.Edit(trangthai);
                return RedirectToAction("Index");
            }
            else
            {
                return View(trangthai);
            }
        }

        public ActionResult Delete(Guid id)
        {
            ttDao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}