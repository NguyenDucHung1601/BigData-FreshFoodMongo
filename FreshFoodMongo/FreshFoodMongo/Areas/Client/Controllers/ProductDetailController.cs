using FreshFoodMongo.Models.DAO;
using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Client.Controllers
{
    public class ProductDetailController : Controller
    {
        BaseDAO baseDao = new BaseDAO();
        SanPhamDAO spDao = new SanPhamDAO();
        static List<SanPham> recommendProduct = new List<SanPham>();

        // GET: Client/ProductDetail
        public async Task<ActionResult> Index(Guid id)
        {
            SanPham sanpham = spDao.GetByID(id);
            sanpham.SoLuotXem = sanpham.SoLuotXem + 1;
            spDao.Edit(sanpham);

            ViewBag.Product = sanpham;

            ChiTietGioHang obj = new ChiTietGioHang();
            obj.IDChiTietGioHang = Guid.NewGuid();
            obj.IDSanPham = id;
            obj.SoLuong = 1;

            // recommend product
            var idsRecommend = new List<Guid>();
            if (Session["IDUSER_SESSION"] != null)
            {
                using (var client = new HttpClient())
                {
                    var path = string.Format(Common.CommonVariables.g_apiGetRecommendProdct, (Guid)Session["IDUSER_SESSION"]);
                    var response = await client.GetAsync(path);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        data = data.Replace("[", "").Replace("]", "").Replace("\"", "");
                        var ids = data.Split(',');

                        foreach (var item in ids)
                            idsRecommend.Add(new Guid(item));
                    }
                }
                IList<SanPham> list = new List<SanPham>();
                foreach (var item in idsRecommend)
                    list.Add(spDao.GetByID(item));
                recommendProduct = list.ToList();
            }

            return View(obj);
        }

        public ActionResult RecommendProduct(Guid idtheloai, string ten)
        {
            IList<SanPham> list = spDao.ListSanPham().Where(x => x.IDTheLoai == idtheloai).ToList();

            return PartialView(recommendProduct);
        }

        public ActionResult RelatedProduct(Guid idtheloai, string ten)
        {
            IList<SanPham> list = spDao.ListSanPham().Where(x => x.IDTheLoai == idtheloai).ToList();

            return PartialView(list);
        }
    }
}