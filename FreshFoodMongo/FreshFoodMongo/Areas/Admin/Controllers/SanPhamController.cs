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
    public class SanPhamController : BaseController
    {
        SanPhamDAO spDao = new SanPhamDAO();
        TheLoaiDAO tlDao = new TheLoaiDAO();

        // GET: Admin/SanPham
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
            ViewBag.Count = spDao.ListSimple(searching).Count();
            return View(spDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult CleanDataProduct(int? page, int? PageSize, string searching = "")
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
            ViewBag.Count = spDao.ListSimple(searching).Count();
            var lstSanPham = spDao.getDataSanPham();
            var lstSanPhamClean = new List<SanPham>();
            //Bloom filter : Loại bỏ sản phẩm tên trùng nhau
            var bloomFilter = new Filter<string>(lstSanPham.Count());
            foreach (var item in lstSanPham)
            {
                if (!bloomFilter.Contains(item.Ten))
                {
                    bloomFilter.Add(item.Ten);
                    lstSanPhamClean.Add(item);
                }
                else
                {
                    // Loại bỏ sản phẩm bị trùng
                    spDao.Delete(item.IDSanPham);
                }
            }
            return View(spDao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            SanPham sanpham = spDao.GetByID(id);
            return View(sanpham);
        }

        public ActionResult Create()
        {
            SelectList theloaiList = new SelectList(tlDao.ListTheLoai(), "IDTheLoai", "Ten", "IDTheLoai");
            ViewBag.CategoryList = theloaiList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(string ten, string donvitinh, string giatien, string giakhuyenmai, HttpPostedFileBase hinhanh, string mota, string soluong, Guid idtheloai)
        {
            SanPham sanpham = new SanPham();
            sanpham.IDSanPham = Guid.NewGuid();
            sanpham.MaSo = $"{tlDao.GetMaSo(idtheloai)}.{spDao.GetIdentity()}";
            sanpham.Ten = ten;
            sanpham.DonViTinh = donvitinh;
            sanpham.GiaTien = Convert.ToDecimal(giatien);
            sanpham.GiaKhuyenMai = Convert.ToDecimal(giakhuyenmai);
            sanpham.MoTa = mota;
            sanpham.SoLuong = Convert.ToInt64(soluong);
            sanpham.CoSan = sanpham.SoLuong > 0 ? true : false;
            sanpham.IDTheLoai = idtheloai;
            sanpham.SoLuotXem = 0;
            sanpham.SoLuotMua = 0;

            sanpham.CreatedDate = DateTime.Now;
            sanpham.CreatedBy = (string)Session["USERNAME_SESSION"];
            sanpham.ModifiedDate = DateTime.Now;
            sanpham.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                if (hinhanh != null && hinhanh.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(hinhanh.FileName));
                    hinhanh.SaveAs(path);
                    sanpham.HinhAnh = hinhanh.FileName;
                }
                spDao.Add(sanpham);
                return RedirectToAction("Index");
            }
            else
            {
                return View(sanpham);
            }
        }

        public ActionResult Edit(Guid id)
        {
            SelectList theloaiList = new SelectList(tlDao.ListTheLoai(), "IDTheLoai", "Ten", "IDTheLoai");
            ViewBag.CategoryList = theloaiList;

            return View(spDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string maso, string ten, string donvitinh, string giatien, string giakhuyenmai, HttpPostedFileBase hinhanh, string mota, string soluong, Guid idtheloai)
        {
            SanPham sanpham = spDao.GetByID(id);
            sanpham.MaSo = maso;
            sanpham.Ten = ten;
            sanpham.DonViTinh = donvitinh;
            sanpham.GiaTien = Convert.ToDecimal(giatien);
            sanpham.GiaKhuyenMai = Convert.ToDecimal(giakhuyenmai);
            sanpham.MoTa = mota;
            sanpham.SoLuong = Convert.ToInt64(soluong);
            sanpham.CoSan = sanpham.SoLuong > 0 ? true : false;
            sanpham.IDTheLoai = idtheloai;

            sanpham.ModifiedDate = DateTime.Now;
            sanpham.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                if (hinhanh != null && hinhanh.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(hinhanh.FileName));
                    hinhanh.SaveAs(path);
                    sanpham.HinhAnh = hinhanh.FileName;
                }
                spDao.Edit(sanpham);
                return RedirectToAction("Index");
            }
            else
            {
                return View(sanpham);
            }
        }

        public ActionResult Delete(Guid id)
        {
            spDao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}