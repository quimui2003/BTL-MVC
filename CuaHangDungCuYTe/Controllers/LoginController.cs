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
<<<<<<< HEAD
=======
            Session["Login"] = false;
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
            return PartialView();
        }

        [HttpPost]
        public ActionResult XuLyLogin(LoginViewModels formData)
        {
<<<<<<< HEAD
            var acc = dbcontext.Accounts.FirstOrDefault(a => a.Email == formData.email && a.Password == formData.passWord);
=======
            var acc = dbcontext.Accounts.FirstOrDefault(a => a.email == formData.email && a.passWord == formData.passWord);
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
            if (acc == null)
            {
                return Json(new { success = false, message = "Tài khoản hoặc mật khẩu không đúng!" });
            }
            else
            {
<<<<<<< HEAD
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

=======
                var ktraRole = acc.role;
                Session["Login"] = true;
                Session["Username"] = formData.email;
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
                if (ktraRole == "User")
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "User") });
                }
                else
                {
<<<<<<< HEAD
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Admin", new { area = "Admin" }) });
=======
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Admin") });
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
                }
            }
        }

        public ActionResult Logout()
        {
            Session["Login"] = false;
            Session["Username"] = null;
<<<<<<< HEAD
            Session.Clear();
=======
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
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
<<<<<<< HEAD
            var acc = dbcontext.Accounts.FirstOrDefault(a => a.Email == formData.email);
=======
            var acc = dbcontext.Accounts.FirstOrDefault(a => a.email == formData.email);
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
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
<<<<<<< HEAD
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
=======
                    var item = new Account();
                    item.email = formData.email;
                    item.passWord = formData.passWord;
                    item.fullName = "";
                    item.sdt = "";
                    item.role = "User";
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
                    dbcontext.Accounts.Add(item);
                    dbcontext.SaveChanges();
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "User") });
                }
            }
        }
    }
}