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
    public class NhaCungCapController : BaseController
    {
        NhaCungCapDAO nccDao = new NhaCungCapDAO();
        NhaCungCapSanPhamDAO nccspDao = new NhaCungCapSanPhamDAO();
        // GET: Admin/NhaCungCap
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
            ViewBag.Count = nccDao.ListSimple(searching).Count();
            return View(nccDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }
        public ActionResult Details(Guid id)
        {
            NhaCungCap nhacungcap = nccDao.GetByID(id);
            return View(nhacungcap);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string ten, string diachi, HttpPostedFileBase hinhanh,string dienthoai)
        {
            NhaCungCap nhacungcap = new NhaCungCap();
            nhacungcap.IDNhaCungCap = Guid.NewGuid();
            
            nhacungcap.Ten = ten;
            nhacungcap.DiaChi = diachi;
            nhacungcap.DienThoai = dienthoai;
            nhacungcap.CreatedDate = DateTime.Now;

            nhacungcap.CreatedDate = DateTime.Now;
            nhacungcap.CreatedBy = (string)Session["USERNAME_SESSION"];
            nhacungcap.ModifiedDate = DateTime.Now;
            nhacungcap.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                if (hinhanh != null && hinhanh.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(hinhanh.FileName));
                    hinhanh.SaveAs(path);
                    nhacungcap.HinhAnh = hinhanh.FileName;
                }
                nccDao.Add(nhacungcap);
                return RedirectToAction("Index");
            }
            else
            {
                return View(nhacungcap);
            }
        }
        public ActionResult Edit(Guid id)
        {
            return View(nccDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id,string ten, string diachi, HttpPostedFileBase hinhanh, string dienthoai)
        {
            NhaCungCap nhacungcap = nccDao.GetByID(id);
            nhacungcap.Ten = ten;
            nhacungcap.DiaChi = diachi;
            nhacungcap.DienThoai = dienthoai;

            nhacungcap.ModifiedDate = DateTime.Now;
            nhacungcap.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                if (hinhanh != null && hinhanh.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(hinhanh.FileName));
                    hinhanh.SaveAs(path);
                    nhacungcap.HinhAnh = hinhanh.FileName;
                }
                nccDao.Edit(nhacungcap);
                return RedirectToAction("Index");
            }
            else
            {
                return View(nhacungcap);
            }
        }

        public ActionResult Delete(Guid id)
        {
            nccDao.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult IndexNhaCungCapSanPham(Guid id)
        {
            var listSanPhamCungCap = nccspDao.GetListSPCungUngByIDNhaCungCap(id);

            if (listSanPhamCungCap == null)
                return HttpNotFound();

            ViewBag.IDNhaCungCap = id;

            return PartialView(listSanPhamCungCap);
        }
    }
}