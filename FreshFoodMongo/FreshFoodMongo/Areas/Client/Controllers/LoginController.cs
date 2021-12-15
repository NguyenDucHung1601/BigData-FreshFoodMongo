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

namespace FreshFoodMongo.Areas.Client.Controllers
{
    public class LoginController : Controller
    {
        BaseDAO baseDao = new BaseDAO();
        NguoiDungDAO ndDao = new NguoiDungDAO();

        // GET: Client/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var dao = new NguoiDungDAO();
                var result = dao.LoginClient(collection["inputUsername"].ToString(), collection["inputPassword"].ToString());
                if (result == 1)
                {
                    var user = dao.GetByUsername(collection["inputUsername"].ToString());
                    var userSession = new UserLogin();

                    userSession.IDNguoiDung = user.IDNguoiDung;
                    userSession.Username = user.Username;
                    userSession.Avatar = user.Avatar;
                    userSession.Ten = user.Ten;
                    userSession.NgayTao = user.CreatedDate;

                    Session.Add(Common.CommonConstants.USER_SESSION, userSession);
                    Session.Add(Common.CommonConstants.IDUSER_SESSION, userSession.IDNguoiDung);
                    Session.Add(Common.CommonConstants.USERNAME_SESSION, userSession.Username);
                    Session.Add(Common.CommonConstants.AVATAR_SESSION, userSession.Avatar);
                    Session.Add(Common.CommonConstants.NAME_SESSION, userSession.Ten);
                    Session.Add(Common.CommonConstants.CREATEDDATE_SESSION, userSession.NgayTao);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username hoặc password không đúng");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            //Session["USER_SESSION"] = null;
            //Session["AVATAR_SESSION"] = null;
            //Session["NAME_SESSION"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password, string confirm, string ten, string dienthoai, string email, string diachi, HttpPostedFileBase avatar)
        {
            NguoiDung nguoidung = new NguoiDung();
            flatNguoiDungMoi newuser = new flatNguoiDungMoi() { Username = username, Ten = ten, DienThoai = dienthoai, DiaChi = diachi, Password = password, Confirm = confirm };

            nguoidung.IDNguoiDung = Guid.NewGuid();
            nguoidung.IsAdmin = false;
            nguoidung.IDLoaiNguoiDung = baseDao.getDataLoaiNguoiDung().Where(x => x.Ten.Equals("Client")).Select(x => x.IDLoaiNguoiDung).ToList().ElementAt(0);
            nguoidung.Username = username;
            nguoidung.Ten = ten;
            nguoidung.DienThoai = dienthoai;
            nguoidung.Email = email;
            nguoidung.DiaChi = diachi;

            nguoidung.IDLoaiKhachHang = baseDao.getDataPhanLoaiKhachHang().Where(x => x.CapDo == 0).Select(x=>x.IDLoaiKhachHang).ToList().ElementAt(0);
            nguoidung.TongTienGioHang = 0;
            nguoidung.SoDonHangDaMua = 0;
            nguoidung.TongTienGioHang = 0;

            nguoidung.CreatedDate = DateTime.Now;
            nguoidung.CreatedBy = (string)Session["USERNAME_SESSION"];
            nguoidung.ModifiedDate = DateTime.Now;
            nguoidung.ModifiedBy = (string)Session["USERNAME_SESSION"];

            if (ModelState.IsValid)
            {
                if (!password.Equals(confirm))
                {
                    ViewBag.ConfirmNewPassword = "** Mật khẩu và xác nhận không khớp";
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
    }
}