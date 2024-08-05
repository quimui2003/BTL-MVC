using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangDungCuYTe.Models;
using CuaHangDungCuYTe.UserViewModels;

namespace CuaHangDungCuYTe.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        private YteModels dbcontext = new YteModels();

        [HttpGet]
        public ActionResult Login()
        {
            Session["Login"] = false;
            return PartialView();
        }

        [HttpPost]
        public ActionResult XuLyLogin(LoginViewModels formData)
        {
            var acc = dbcontext.Accounts.FirstOrDefault(a => a.email == formData.email && a.passWord == formData.passWord);
            if (acc == null)
            {
                return Json(new { success = false, message = "Tài khoản hoặc mật khẩu không đúng!" });
            }
            else
            {
                var ktraRole = acc.role;
                Session["Login"] = true;
                Session["Username"] = formData.email;
                if (ktraRole == "User")
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "User") });
                }
                else
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Admin") });
                }
            }
        }

        public ActionResult Logout()
        {
            Session["Login"] = false;
            Session["Username"] = null;
            return RedirectToAction("Index", "User");
        }

        public ActionResult Logged()
        {
            return PartialView();
        }

        public ActionResult Register()
        {
            return PartialView();
        }

        public ActionResult XuLyRegister(LoginViewModels formData)
        {
            var acc = dbcontext.Accounts.FirstOrDefault(a => a.email == formData.email);
            if (acc != null)
            {
                return Json(new { success = false, message = "Tài khoản này đã tồn tại!" });
            }
            else
            {
                if(formData.passWord != formData.passWord2)
                {
                    return Json(new { success = false, message = "Mật khẩu không trùng khớp!" });
                }
                else
                {
                    Session["Login"] = true;
                    Session["Username"] = formData.email;
                    var item = new Account();
                    item.email = formData.email;
                    item.passWord = formData.passWord;
                    item.fullName = "";
                    item.sdt = "";
                    item.role = "User";
                    dbcontext.Accounts.Add(item);
                    dbcontext.SaveChanges();
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "User") });
                }
            }
        }
    }
}