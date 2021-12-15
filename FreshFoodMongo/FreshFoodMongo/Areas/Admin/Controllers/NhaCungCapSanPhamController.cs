using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class NhaCungCapSanPhamController : BaseController
    {
        NhaCungCapDAO nccDao = new NhaCungCapDAO();
        SanPhamDAO spDao = new SanPhamDAO();
        NhaCungCapSanPhamDAO nccspDao = new NhaCungCapSanPhamDAO();
        static Guid IDcurNhaCungCap;

        // GET: Admin/NhaCungCapSanPham
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(Guid id)
        {
            NhaCungCapSanPham nccsp = nccspDao.GetByID(id);
            return View(nccsp);
        }

        public ActionResult Create(Guid idncc)
        {
            IDcurNhaCungCap = idncc;
            ViewBag.IDNhaCungCap = idncc;
            ViewBag.TenNhaCungCap = (nccDao.GetByID(idncc)).Ten;
            ViewBag.ListSanPham =  new SelectList(spDao.ListSanPham(), "IDSanPham", "Ten", "IDSanPham");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Guid idsanpham, string donvitinh, string giacungung)
        {
            NhaCungCapSanPham nccsp = new NhaCungCapSanPham();
            nccsp.IDNhaCungCapSanPham = Guid.NewGuid();
            nccsp.IDNhaCungCap = IDcurNhaCungCap;
            nccsp.IDSanPham = idsanpham;
            nccsp.DonViTinh = donvitinh;
            nccsp.GiaCungUng = Convert.ToDecimal(giacungung);
            nccsp.NgayCapNhat = DateTime.Now;

            if (ModelState.IsValid)
            {
                nccspDao.Add(nccsp);
                return RedirectToAction("Details", "NhaCungCap", new { id = IDcurNhaCungCap });
            }
            else
            {
                return View(nccsp);
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(nccspDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string donvitinh, string giacungung)
        {
            NhaCungCapSanPham nccsp = nccspDao.GetByID(id);
            nccsp.DonViTinh = donvitinh;
            nccsp.GiaCungUng = Convert.ToDecimal(giacungung);
            nccsp.NgayCapNhat = DateTime.Now;

            if (ModelState.IsValid)
            {
                nccspDao.Edit(nccsp);
                return RedirectToAction("Details", "NhaCungCap", new { id = nccsp.IDNhaCungCap });
            }
            else
            {
                return View(nccsp);
            }
        }

        public ActionResult Delete(Guid id)
        {
            NhaCungCapSanPham nccsp = nccspDao.GetByID(id);
            nccspDao.Delete(id);
            return RedirectToAction("Details", "NhaCungCap", new { id = nccsp.IDNhaCungCap });
        }
    }
}