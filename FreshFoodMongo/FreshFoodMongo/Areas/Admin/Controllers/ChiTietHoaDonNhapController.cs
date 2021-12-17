using FreshFoodMongo.Models.DAO;
using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using FreshFoodMongo.Models.DTOplus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class ChiTietHoaDonNhapController : Controller
    {
        CommonDAO commonDao = new CommonDAO();
        ChiTietHoaDonNhapDAO cthdnDao = new ChiTietHoaDonNhapDAO();
        HoaDonNhapDAO hdnDao = new HoaDonNhapDAO();
        SanPhamDAO spDao = new SanPhamDAO();
        NhaCungCapSanPhamDAO nccspDao = new NhaCungCapSanPhamDAO();
        static Guid IDcurHoaDonNhap;


        // GET: Admin/ChiTietHoaDonNhap
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(Guid id)
        {
            ChiTietHoaDonNhap chitietHDN = cthdnDao.GetByID(id);
            return View(chitietHDN);
        }

        public ActionResult Create(Guid idhdn, Guid idncc)
        {
            IEnumerable<NhaCungCapSanPham> listSPCungUng = nccspDao.GetListSPCungUngByIDNhaCungCap(idncc);
            var selectList = new List<flatSanPham>();
            foreach (var item in listSPCungUng)
                selectList.Add(new flatSanPham { IDSanPham = item.IDSanPham, TenSanPham = commonDao.getRf_TenSanPham(item.IDSanPham) });
            ViewBag.ListSPCungUng = new SelectList(selectList, "IDSanPham", "TenSanPham", "IDSanPham");
            IDcurHoaDonNhap = idhdn;
            ViewBag.IDHoaDonNhap = idhdn;
            ViewBag.MaSoHoaDonNhap = (hdnDao.GetByID(idhdn)).MaSo;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Guid idsanpham, string soluong)
        {
            ChiTietHoaDonNhap chitietHDN = new ChiTietHoaDonNhap();
            chitietHDN.IDChiTietHoaDonNhap = Guid.NewGuid();
            chitietHDN.IDHoaDonNhap = IDcurHoaDonNhap;
            chitietHDN.IDSanPham = idsanpham;
            chitietHDN.DonViTinh = nccspDao.SPCungUng_GetDonViTinh((hdnDao.GetByID(IDcurHoaDonNhap)).IDNhaCungCap, idsanpham);
            chitietHDN.DonGiaNhap = nccspDao.SPCungUng_GetGiaCungUng((hdnDao.GetByID(IDcurHoaDonNhap)).IDNhaCungCap, idsanpham);
            chitietHDN.SoLuong = Convert.ToInt32(soluong);
            chitietHDN.ThanhTien = chitietHDN.DonGiaNhap * chitietHDN.SoLuong;

            if (ModelState.IsValid)
            {
                cthdnDao.Add(chitietHDN);
                return RedirectToAction("Details", "HoaDonNhap", new { id = IDcurHoaDonNhap });
            }
            else
            {
                return View(chitietHDN);
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(cthdnDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string soluong)
        {
            ChiTietHoaDonNhap chitietHDN = cthdnDao.GetByID(id);
            var thanhTienCu = chitietHDN.ThanhTien;
            chitietHDN.SoLuong = Convert.ToInt32(soluong);
            chitietHDN.ThanhTien = chitietHDN.DonGiaNhap * chitietHDN.SoLuong;

            if (ModelState.IsValid)
            {
                cthdnDao.Edit(chitietHDN, thanhTienCu);
                return RedirectToAction("Details", "HoaDonNhap", new { id = chitietHDN.IDHoaDonNhap });
            }
            else
            {
                return View(chitietHDN);
            }
        }

        public ActionResult Delete(Guid id)
        {
            ChiTietHoaDonNhap chitietHDN = cthdnDao.GetByID(id);
            cthdnDao.Delete(id);
            return RedirectToAction("Details", "HoaDonNhap", new { id = chitietHDN.IDHoaDonNhap });
        }
    }
}