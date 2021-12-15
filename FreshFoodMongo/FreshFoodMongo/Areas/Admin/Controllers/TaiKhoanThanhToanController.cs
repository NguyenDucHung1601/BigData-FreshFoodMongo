using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class TaiKhoanThanhToanController : BaseController
    {
        TaiKhoanThanhToanDAO tkttDao = new TaiKhoanThanhToanDAO();
        // GET: Admin/TaiKhoanThanhToan
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
            ViewBag.Count = tkttDao.ListSimple(searching).Count();
            return View(tkttDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }
        public ActionResult Details(Guid id)
        {
            TaiKhoanThanhToan taikhoanthanhtoan = tkttDao.GetByID(id);
            return View(taikhoanthanhtoan);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string viettat, string ten, HttpPostedFileBase logo,string loaitaikhoan)
        {
            TaiKhoanThanhToan taikhoanthanhtoan = new TaiKhoanThanhToan();
            taikhoanthanhtoan.IDTaiKhoan = Guid.NewGuid();
            taikhoanthanhtoan.VietTat = viettat;
            taikhoanthanhtoan.Ten = ten;
            taikhoanthanhtoan.LoaiTaiKhoan = loaitaikhoan;
            if (ModelState.IsValid)
            {
                if (logo != null && logo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(logo.FileName));
                    logo.SaveAs(path);
                    taikhoanthanhtoan.Logo = logo.FileName;
                }
                tkttDao.Add(taikhoanthanhtoan);
                return RedirectToAction("Index");
            }
            else
            {
                return View(taikhoanthanhtoan);
            }
        }
        public ActionResult Edit(Guid id)
        {
            return View(tkttDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id,string viettat,string ten, HttpPostedFileBase logo, string loaitaikhoan)
        {
            TaiKhoanThanhToan taikhoanthanhtoan = tkttDao.GetByID(id);
            taikhoanthanhtoan.VietTat = viettat;
            taikhoanthanhtoan.Ten = ten;
            taikhoanthanhtoan.LoaiTaiKhoan = loaitaikhoan;

            if (ModelState.IsValid)
            {
                if (logo != null && logo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(logo.FileName));
                    logo.SaveAs(path);
                    taikhoanthanhtoan.Logo = logo.FileName;
                }
                tkttDao.Edit(taikhoanthanhtoan);
                return RedirectToAction("Index");
            }
            else
            {
                return View(taikhoanthanhtoan);
            }
        }

        public ActionResult Delete(Guid id)
        {
            tkttDao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}