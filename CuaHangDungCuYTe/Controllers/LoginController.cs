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
            return PartialView();
        }

        [HttpPost]
        public ActionResult XuLyLogin(LoginViewModels formData)
        {
            var acc = dbcontext.Accounts.FirstOrDefault(a => a.Email == formData.email && a.Password == formData.passWord);
            if (acc == null)
            {
                return Json(new { success = false, message = "Tài khoản hoặc mật khẩu không đúng!" });
            }
            else
            {
                var ktraRole = acc.Role;
                Session["Login"] = true;
                Session["Username"] = formData.email;
                Session["Role"] = ktraRole.ToString();

                // Lấy giỏ hàng từ database và lưu vào session
                var cartItems = dbcontext.OrdersCarts.Where(c => c.Email == formData.email).ToList();
                var cartSession = new List<CartItem>();
                foreach (var item in cartItems)
                {
                    var product = dbcontext.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                    if (product != null)
                    {
                        cartSession.Add(new CartItem { Product = product, Quantity = item.Soluong });
                    }
                }
                Session["cartSession"] = cartSession;

                if (ktraRole == "User")
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "User") });
                }
                else
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Admin", new { area = "Admin" }) });
                }
            }
        }

        public ActionResult Logout()
        {
            Session["Login"] = false;
            Session["Username"] = null;
            Session.Clear();
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
            var acc = dbcontext.Accounts.FirstOrDefault(a => a.Email == formData.email);
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
                    var item = new Account
                    {
                        Email = formData.email,
                        Password = formData.passWord,
                        Fullname = "",
                        Avatar = "",
                        Sdt = "",
                        Address = "",
                        Role = "User"
                    };

                    dbcontext.Accounts.Add(item);
                    dbcontext.SaveChanges();
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "User") });
                }
            }
        }
    }
}