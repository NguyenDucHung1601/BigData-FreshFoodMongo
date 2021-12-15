using FreshFoodMongo.Common;
using FreshFoodMongo.Models.DAO;
using FreshFoodMongo.Models.DAO.Admin;
using FreshFoodMongo.Models.DTO;
using FreshFoodMongo.Models.DTOplus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreshFoodMongo.Areas.Admin.Controllers
{
    public class NguoiDungAdminController : BaseController
    {
        // GET: Admin/NguoiDungAdmin
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
            ViewBag.Count = ndDao.ListAdminSimple(searching).Count();
            return View(ndDao.ListAdminSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(Guid id)
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            return View(nguoidung);
        }

        public ActionResult ChangePassword(Guid id)
        {
            flatChangePassword obj = new flatChangePassword() { IDUser = id, Username = (ndDao.GetByID(id)).Username };
            return View(obj);
        }

        [HttpPost]
        public ActionResult ChangePassword(Guid id, string oldpass, string newpass, string confirm)
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            flatChangePassword userchange = new flatChangePassword() { IDUser = id, Username = (ndDao.GetByID(id)).Username, OldPass = oldpass, NewPass = newpass, Confirm = confirm };

            nguoidung.ModifiedDate = DateTime.Now;
            nguoidung.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                if (BCrypt.Net.BCrypt.Verify(oldpass, nguoidung.Password))
                {
                    ViewBag.OldPassword = string.Empty;
                    if (BCrypt.Net.BCrypt.Verify(newpass, nguoidung.Password))
                        ViewBag.NewPassword = "** Mật khẩu mới phải khác mật khẩu cũ";
                    else
                    {
                        ViewBag.NewPassword = string.Empty;
                        if (!newpass.Equals(confirm))
                            ViewBag.ConfirmPassword = "** Mật khẩu mới và xác nhận mật khẩu không khớp";
                        else
                        {
                            ViewBag.ConfirmPassword = string.Empty;
                            ndDao.ChangePassword(nguoidung, newpass);
                            return RedirectToAction("Details", new { id = nguoidung.IDNguoiDung });
                        }
                    }
                    return View(userchange);
                }
                ViewBag.OldPassword = "** Mật khẩu cũ không đúng";
                return View(userchange);
            }
            else
            {
                return View(userchange);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string username, string password, string confirm, string ten, string dienthoai, string email, string diachi, HttpPostedFileBase avatar)
        {
            NguoiDung nguoidung = new NguoiDung();
            flatNguoiDungMoi newuser = new flatNguoiDungMoi() { Username = username, Ten = ten, DienThoai = dienthoai, DiaChi = diachi, Password = password, Confirm = confirm };

            nguoidung.IDNguoiDung = Guid.NewGuid();
            nguoidung.IsAdmin = true;
            nguoidung.IDLoaiNguoiDung = baseDao.getDataLoaiNguoiDung().FirstOrDefault(x => x.Ten.Equals("Admin")).IDLoaiNguoiDung;
            nguoidung.Username = username;
            nguoidung.Ten = ten;
            nguoidung.DienThoai = dienthoai;
            nguoidung.Email = email;
            nguoidung.DiaChi = diachi;

            nguoidung.CreatedDate = DateTime.Now;
            nguoidung.CreatedBy = (string)Session["USERNAME_SESSION"];
            nguoidung.ModifiedDate = DateTime.Now;
            nguoidung.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                if (!password.Equals(confirm))
                {
                    ViewBag.ConfirmNewPassword = "** Mật khẩu và xác nhận mật khẩu không khớp";
                    return View(newuser);
                }

                nguoidung.Password = BCrypt.Net.BCrypt.HashPassword(password);
                
                if (avatar != null && avatar.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(avatar.FileName));
                    avatar.SaveAs(path);
                    nguoidung.Avatar = avatar.FileName;
                }
                ndDao.Add(nguoidung);
                return RedirectToAction("Index");
            }
            else
            {
                return View(newuser);
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(ndDao.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string ten, string dienthoai, string email, string diachi, HttpPostedFileBase avatar)
        {
            NguoiDung nguoidung = ndDao.GetByID(id);
            nguoidung.Ten = ten;
            nguoidung.DienThoai = dienthoai;
            nguoidung.Email = email;
            nguoidung.DiaChi = diachi;

            nguoidung.ModifiedDate = DateTime.Now;
            nguoidung.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                if (avatar != null && avatar.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(avatar.FileName));
                    avatar.SaveAs(path);
                    nguoidung.Avatar = avatar.FileName;
                }
                ndDao.Edit(nguoidung);
                return RedirectToAction("Index");
            }
            else
            {
                return View(nguoidung);
            }
        }

        public ActionResult Delete(Guid id)
        {
            ndDao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}