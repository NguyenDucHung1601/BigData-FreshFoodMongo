using FreshFoodMongo.Models.DAO;
using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DAO.Client;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Client.Controllers
{
    public class CheckoutController : Controller
    {
        BaseDAO baseDao = new BaseDAO();
        NguoiDungDAO ndDao = new NguoiDungDAO();
        PhuongThucThanhToanDAO ptttDao = new PhuongThucThanhToanDAO();
        ClientDonHangDAO cdhDao = new ClientDonHangDAO();
        DonHangDAO dhDao = new DonHangDAO();
        TKThanhToanNguoiDungDAO tkttndDao = new TKThanhToanNguoiDungDAO();
        ChiTietDonHangDAO ctdhDao = new ChiTietDonHangDAO();

        static DonHang donHangAdd;
        static List<ChiTietDonHang> lstCTDH;
        static string maGiamGia;
        static Guid idTKTTND;
        static decimal TienTTOL;
        // GET: Client/Checkout
        public ActionResult Index(Guid id, string? idpttt = "af8ad04b-0200-4e0b-ba12-ef94175af1d9")
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            ViewBag.User = nguoidung;
            var route = RouteData.Values["magiamgia"] + Request.Url.Query;
            string mgg = route.Split('&').First().Split('=').Last().ToString();

            var lisPTTT = ptttDao.ListPhuongThucThanhToan();
            ViewBag.PayWayList = new SelectList(lisPTTT, "IDPhuongThucThanhToan", "TenPhuongThucThanhToan", "IDPhuongThucThanhToan");

            decimal TienGiam = 0;
            if (idpttt == "0cea7cb1-b4d5-496c-a2e8-2e0c7386079e")
            {
                mgg = maGiamGia;
            }
            ViewBag.mgg = mgg;
            // Áp dụng giảm giá
            if (mgg != "" && mgg != null)
            {
                if (cdhDao.KiemTraDoiTuongApDung(mgg, nguoidung))
                {
                    TienGiam = baseDao.getDataMaGiamGia().FirstOrDefault(x => x.CodeGiamGia == mgg).TienGiam;
                    maGiamGia = mgg;
                }
            }
            //create donHang
            var lstctgh = baseDao.getDataChiTietGioHang().Where(x => x.IDKhachHang == id).ToList();

            var donHang = new DonHang();
            donHang.TenNhanHang = nguoidung.Ten;
            donHang.DiaChiNhanHang = nguoidung.DiaChi;
            donHang.SdtNhanHang = nguoidung.DienThoai;
            donHang.IDDonHang = Guid.NewGuid();
            donHang.CreatedDate = DateTime.Now;
            donHang.CreatedBy = (string)Session["USERNAME_SESSION"];
            donHang.ModifiedDate = DateTime.Now;
            donHang.ModifiedBy = (string)Session["USERNAME_SESSION"];
            donHang.TienHang = nguoidung.TongTienGioHang;
            if (donHang.TienHang >= 100000)
            {
                donHang.TienShip = 0;
            }
            else
            {
                donHang.TienShip = 30000;
            }
            donHang.TienGiam = TienGiam;
            donHang.TongTien = nguoidung.TongTienGioHang + donHang.TienShip - donHang.TienGiam;
            donHang.IDKhachHang = id;
            donHang.IDTrangThai = new Guid("5404ec28-c908-48b1-a7e5-e5a366b51d5a"); // chờ xác nhận
            donHang.IDPhuongThucThanhToan = new Guid(idpttt);

            if (idpttt == "0cea7cb1-b4d5-496c-a2e8-2e0c7386079e")
            {
                ViewBag.DaThanhToan = donHang.TongTien;
                TienTTOL = (decimal)donHang.TongTien;
            }
            else
            {
                TienTTOL = 0;
                ViewBag.DaThanhToan = 0;
            }
            donHangAdd = donHang;
            ViewBag.donhang = donHang;
            //mapper ChiTietGioHang => ChiTietDonHang
            var lstResult = cdhDao.CapNhatTongTienDonHangSoBo(lstctgh);
            lstCTDH = lstResult;
            return View(lstResult);
        }

        public ActionResult XacNhanThanhToan(Guid id)
        {
            var idnd = (Guid)Session["IDUSER_SESSION"];
            if(idTKTTND != null && idTKTTND != Guid.Empty)
            {
                var TKTTnguoiDung = baseDao.getDataTKThanhToanNguoiDung().FirstOrDefault(x => x.IDTaiKhoan == idTKTTND & x.IDNguoiDung == idnd);
                TKTTnguoiDung.TongTien = TKTTnguoiDung.TongTien - TienTTOL;
                tkttndDao.Edit(TKTTnguoiDung);
            }    
            
            var res = donHangAdd;
            //tạo đơn hàng mới;
            dhDao.Add(res);
            foreach (ChiTietDonHang item in lstCTDH)
            {
                var ctdh = new ChiTietDonHang();
                ctdh.IDChiTietDonHang = Guid.NewGuid();
                ctdh.IDDonHang = donHangAdd.IDDonHang;
                ctdh.IDSanPham = item.IDSanPham;
                ctdh.DonGiaBan = baseDao.getDataSanPham().FirstOrDefault(x => x.IDSanPham == item.IDSanPham).GiaKhuyenMai;
                ctdh.SoLuong = item.SoLuong;
                ctdh.ThanhTien = item.ThanhTien;
                ctdh.CreatedDate = DateTime.Now;
                ctdh.CreatedBy = (string)Session["USERNAME_SESSION"];
                ctdh.ModifiedDate = DateTime.Now;
                ctdh.ModifiedBy = (string)Session["USERNAME_SESSION"];
                ctdhDao.Add(ctdh);
            }
            cdhDao.XacNhanDonHang(id);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginTaiKhoanThanhToan(Guid idtk, Guid idnd)
        {
            TKThanhToanNguoiDung tkthanhtoan = baseDao.getDataTKThanhToanNguoiDung().Where(x => x.IDTaiKhoan == idtk && x.IDNguoiDung == idnd).FirstOrDefault();
            ViewBag.TongTienDonHang = donHangAdd.TongTien;
            return View(tkthanhtoan);
        }

        public ActionResult CheckLoginTaiKhoanThanhToan(Guid idtktt, FormCollection collection)
        {
            // check password
            var idnd = (Guid)Session["IDUSER_SESSION"];
            var taiKhoanTTND = baseDao.getDataTKThanhToanNguoiDung().FirstOrDefault(x => x.IDNguoiDung == idnd & x.IDTaiKhoan == idtktt);
            if (BCrypt.Net.BCrypt.Verify(collection["inputPassword"].ToString(), taiKhoanTTND.Password))
            {
                idTKTTND = idtktt;
                return RedirectToAction("Index", "Checkout", new { id = idnd, idpttt = "0cea7cb1-b4d5-496c-a2e8-2e0c7386079e" });
            }
            return RedirectToAction("LoginTaiKhoanThanhToan", "Checkout", new { idtk = idtktt, idnd = idnd });
        }
    }
}