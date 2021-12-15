using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class BaoCaoController : BaseController
    {
        // GET: Admin/BaoCao
        public ActionResult KhachHangTiemNang()
        {
            return View();
        }
        public ActionResult SanPhamBanChay()
        {
            return View();
        }
        public ActionResult SanPhamBanRa()
        {
            return View();
        }
        public ActionResult DoanhThuSanPham()
        {
            return View();
        }
        public ActionResult SanPhamNhapVao()
        {
            return View();
        }
    }
}