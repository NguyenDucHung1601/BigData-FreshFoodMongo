using FreshFoodMongo.Models.DAO;
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
    public class NguoiDungClientController : BaseController
    {
        // GET: Admin/NguoiDungClient
        BaseDAO baseDao = new BaseDAO();
        NguoiDungDAO ndDao = new NguoiDungDAO();

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
            ViewBag.Count = ndDao.ListClientSimple(searching).Count();
            return View(ndDao.ListClientSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            return View(nguoidung);
        }
    }
}