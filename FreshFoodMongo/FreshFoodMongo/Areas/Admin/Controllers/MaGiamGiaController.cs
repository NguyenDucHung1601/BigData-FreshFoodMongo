using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class MaGiamGiaController : BaseController
    {
        MaGiamGiaDAO mggDao = new MaGiamGiaDAO();
        PhanLoaiKhachHangDAO plkhDao = new PhanLoaiKhachHangDAO();

        // GET: Admin/MaGiamGia
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
            ViewBag.Count = mggDao.ListSimple(searching).Count();
            return View(mggDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            MaGiamGia magiamgia = mggDao.GetByID(id);
            return View(magiamgia);
        }

        public ActionResult Create()
        {
            IEnumerable<PhanLoaiKhachHang> loaiKH = plkhDao.ListPhanLoaiKhachHang();
            SelectList loaiKHList = new SelectList(loaiKH, "IDLoaiKhachHang", "Ten", "IDLoaiKhachHang");
            ViewBag.CustomerCategoryList = loaiKHList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(string magg, Guid idloaikhachhang, string tiengiam, string hansudung)
        {
            MaGiamGia magiamgia = new MaGiamGia();
            magiamgia.IDMaGiamGia = Guid.NewGuid();
            magiamgia.CodeGiamGia = magg;
            magiamgia.IDLoaiKhachHang = idloaikhachhang;
            magiamgia.DieuKienApDung = (plkhDao.GetByID(idloaikhachhang)).Ten;
            magiamgia.TienGiam = Convert.ToDecimal(tiengiam);
            magiamgia.HanSuDung = Convert.ToDateTime(hansudung);

            magiamgia.CreatedDate = DateTime.Now;
            magiamgia.CreatedBy = (string)Session["USERNAME_SESSION"];
            magiamgia.ModifiedDate = DateTime.Now;
            magiamgia.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                mggDao.Add(magiamgia);

                // Insert MaGiamGiaKhachHang
                // TODO

                return RedirectToAction("Index");
            }
            else
            {
                return View(magiamgia);
            }
        }

        public ActionResult Edit(Guid id)
        {
            IEnumerable<PhanLoaiKhachHang> loaiKH = plkhDao.ListPhanLoaiKhachHang();
            SelectList loaiKHList = new SelectList(loaiKH, "IDLoaiKhachHang", "Ten", "IDLoaiKhachHang");
            ViewBag.CustomerCategoryList = loaiKHList;

            return View(mggDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string magg, Guid idloaikhachhang, string tiengiam, string hansudung)
        {
            MaGiamGia magiamgia = mggDao.GetByID(id);
            magiamgia.CodeGiamGia = magg;
            magiamgia.CodeGiamGia = magg;
            magiamgia.IDLoaiKhachHang = idloaikhachhang;
            magiamgia.DieuKienApDung = (plkhDao.GetByID(idloaikhachhang)).Ten;
            magiamgia.TienGiam = Convert.ToDecimal(tiengiam);
            magiamgia.HanSuDung = Convert.ToDateTime(hansudung);

            magiamgia.ModifiedDate = DateTime.Now;
            magiamgia.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                mggDao.Edit(magiamgia);

                // Edit MaGiamGiaKhachHang
                // TODO

                return RedirectToAction("Index");
            }
            else
            {
                return View(magiamgia);
            }
        }

        public ActionResult Delete(Guid id)
        {
            mggDao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}