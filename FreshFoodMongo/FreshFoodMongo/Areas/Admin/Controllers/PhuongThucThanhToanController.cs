using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class PhuongThucThanhToanController : BaseController
    {
        PhuongThucThanhToanDAO ptttDao = new PhuongThucThanhToanDAO();

        // GET: Admin/PhuongThucThanhToan
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
            ViewBag.Count = ptttDao.ListSimple(searching).Count();
            return View(ptttDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            PhuongThucThanhToan phuongthucthanhtoan = ptttDao.GetByID(id);
            return View(phuongthucthanhtoan);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string ten)
        {
            PhuongThucThanhToan phuongthucthanhtoan = new PhuongThucThanhToan();
            phuongthucthanhtoan.IDPhuongThucThanhToan = Guid.NewGuid();
            phuongthucthanhtoan.TenPhuongThucThanhToan = ten;
            if (ModelState.IsValid)
            {
                ptttDao.Add(phuongthucthanhtoan);
                return RedirectToAction("Index");
            }
            else
            {
                return View(phuongthucthanhtoan);
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(ptttDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string ten)
        {
            PhuongThucThanhToan phuongthucthanhtoan = ptttDao.GetByID(id);
            phuongthucthanhtoan.TenPhuongThucThanhToan = ten;
            if (ModelState.IsValid)
            {
                ptttDao.Edit(phuongthucthanhtoan);
                return RedirectToAction("Index");
            }
            else
            {
                return View(phuongthucthanhtoan);
            }
        }

        public ActionResult Delete(Guid id)
        {
            ptttDao.Delete(id);
            return RedirectToAction("Index");
        }

    }
}