using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangDungCuYTe.Areas.Admin.AdminViewModels;
using CuaHangDungCuYTe.Models;
using Microsoft.SqlServer.Server;
=======
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangDungCuYTe.Models;
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf

namespace CuaHangDungCuYTe.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin

        private YteModels dbcontext = new YteModels();
        public ActionResult Index()
        {
<<<<<<< HEAD
            if (Session["Login"] == null || (bool)Session["Login"] == false || (string)Session["Role"] != "Admin")
            {
                return RedirectToAction("Index", "User", new { area = "" });
            }
            var totalPrice = dbcontext.Orders.Where(s => s.status == "Approved").Sum(t => t.TotalPrice);
            var totalProduct = dbcontext.Products.Count();
            var totalOrder = dbcontext.Orders.Where(s => s.status == "Approved").Count();
            var totalUser = dbcontext.Accounts.Where(r => r.Role == "User").Count();
            ViewBag.TotalPrice = totalPrice;
            ViewBag.TotalProduct = totalProduct;
            ViewBag.TotalOrder = totalOrder;
            ViewBag.TotalUser = totalUser;
=======
            var login = Session["Login"] as string;
            ViewBag.Login = login;
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
            return View();
        }

        public ActionResult Profiles()
        {
<<<<<<< HEAD
            var email = Session["Username"] as string;
            var ktra = dbcontext.Accounts.Where(e => e.Email == email).FirstOrDefault();
            if(ktra != null)
            {
                ViewBag.Fullname = ktra.Fullname;
                ViewBag.Avatar = ktra.Avatar;
            }
            return PartialView();
        }

        public ActionResult OrderTB()
        {
            var orderList = (from od in dbcontext.Orders
                            join acc in dbcontext.Accounts on od.Email equals acc.Email
                            select new HoaDonDisplayVM()
                            {
                                OrderId = od.OrderId,
                                Fullname = acc.Fullname,
                                OrderDate = od.OrderDate,
                                TotalPrice = od.TotalPrice,
                                status = od.status
                            }).ToList();
            ViewBag.Message = orderList.Count();
            return PartialView(orderList);
        }

        public ActionResult ProductTB()
        {
            var productList = dbcontext.Products.ToList();
            ViewBag.Message = productList.Count();
            return PartialView(productList);
        }

        public ActionResult CustomerTB()
        {
            var accountList = dbcontext.Accounts.Where(a => a.Role == "User").ToList();
            ViewBag.Message = accountList.Count();
            return PartialView(accountList);
        }

        public ActionResult OrdersPage()
        {
            var orders = (from o in dbcontext.Orders
                            join a in dbcontext.Accounts on o.Email equals a.Email
                            //from nxbf in nxbtemp.DefaultIfEmpty()
                            orderby o.OrderDate descending
                            select new HoaDonDisplayVM()
                            {
                                OrderId = o.OrderId,
                                Fullname = a.Fullname,
                                OrderDate = o.OrderDate,
                                TotalPrice = o.TotalPrice,
                                status = o.status
                            }).ToList();
            ViewBag.Message = orders.Count();
            return View(orders);
        }

        public ActionResult OrderDetailPage(string id)
        {
            var details = (from ct in dbcontext.OrdersDetails
                           join p in dbcontext.Products on ct.ProductId equals p.ProductId
                           where ct.OrderId == id
                           select new ChitietHoaDonDisplayVM()
                           {
                               OrderId = id,
                               ProductId = p.ProductId,
                               ProductName = p.ProductName,
                               ProductImage = p.ProductImage,
                               Quantity = ct.Quantity,
                               GiaBan = ct.Giaban
                           }).ToList();
            ViewBag.Message = details.Count();
            return View(details);
        }

        [HttpPost]
        public ActionResult EditOrders(string id, string status)
        {
            var item = dbcontext.Orders.FirstOrDefault(m => m.OrderId == id);
            if (item == null)
            {
                return Json(new { success = false, message = "Order not found" });
            }
            item.status = status == "Approved" ? "Approved" : "Reject";
            dbcontext.SaveChanges();
            return Json(new { success = true, message = "Order status updated" });
        }


        public ActionResult ProductsPage()
        {
            var products = dbcontext.Products.ToList();
            ViewBag.Message = products.Count();
            return View(products);
        }

        [HttpPost]
        public ActionResult AddProducts(HangHoaVM formData, HttpPostedFileBase fileUpload)
        {
            var item = new Product();
            item.ProductId = (dbcontext.Products.Count() + 1).ToString();
            item.ProductName = formData.ProductName;
            item.Soluongton = formData.Soluongton;
            item.Maloai = formData.Maloai;
            item.Price = formData.Price;
            item.Mota = formData.Mota;

            //get file
            var fileName = System.IO.Path.GetFileName(fileUpload.FileName);

            //get path
            var path = Path.Combine(Server.MapPath("~/Images/"), fileName);

            //ktra path
            if (System.IO.File.Exists(path))
            {
                ViewBag.Message = "Ảnh này đã tồn tại!";
            }
            else
            {
                fileUpload.SaveAs(path);
            }
            item.ProductImage = fileName;
            dbcontext.Products.Add(item);
            dbcontext.SaveChanges();
            return RedirectToAction("ProductsPage", "Admin");
        }

        [HttpPost]
        public ActionResult EditProducts(HangHoaVM formData, string ImageOld, HttpPostedFileBase fileUpload)
        {
            var item = dbcontext.Products.Where(i => i.ProductId == formData.ProductId).FirstOrDefault();
            if(item == null)
            {
                return RedirectToAction("ProductsPage", "Admin");
            }

            item.ProductName = formData.ProductName;
            item.Soluongton = formData.Soluongton;
            item.Maloai = formData.Maloai;
            item.Price = formData.Price;
            item.Mota = formData.Mota;

            if(fileUpload == null)
            {
                item.ProductImage = ImageOld;
            }
            else
            {
                //get file
                var fileName = System.IO.Path.GetFileName(fileUpload.FileName);

                //get path
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);

                //ktra path
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Message = "Ảnh này đã tồn tại!";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                item.ProductImage = fileName;
            }
            dbcontext.SaveChanges();
            return RedirectToAction("ProductsPage", "Admin");
        }

        public ActionResult DeleteProducts(string id)
        {
            var item = dbcontext.Products.Where(i => i.ProductId == id).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("ProductsPage", "Admin");
            }
            dbcontext.Products.Remove(item);
            dbcontext.SaveChanges();
            return RedirectToAction("ProductsPage", "Admin");
        }

        public ActionResult AccountsPage()
        {
            var accounts = dbcontext.Accounts.ToList();
            ViewBag.Mess = accounts.Count();
            return View(accounts);
        }

        [HttpPost]
        public ActionResult EditAccounts(string email, string role)
        {
            var item = dbcontext.Accounts.FirstOrDefault(e => e.Email == email);
            if (item == null)
            {
                return Json(new { success = false, message = "Not found account" });
            }
            item.Role = role == "Appoint" ? "Admin" : "User";
            dbcontext.SaveChanges();
            return Json(new { success = true, message = "Account role updated" });
        }

        public ActionResult SettingsPage()
        {
            var email = Session["Username"] as string;
            var accounts = dbcontext.Accounts.FirstOrDefault(a => a.Email == email);
            return View(accounts);
        }

        [HttpPost]
        public ActionResult EditSettings(TaiKhoanVM formData, string ImageOld, HttpPostedFileBase fileUpload)
        {
            var email = Session["Username"] as string;
            var item = dbcontext.Accounts.FirstOrDefault(a => a.Email == email);
            if (item == null)
            {
                return RedirectToAction("SettingsPage", "Admin");
            }
            item.Fullname = formData.name;
            item.Password = formData.password;
            item.Address = formData.address;
            item.Sdt = formData.sdt;
            if (fileUpload == null)
            {
                item.Avatar = ImageOld;
            }
            else
            {
                //get file
                var fileName = System.IO.Path.GetFileName(fileUpload.FileName);

                //get path
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);

                //ktra path
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Message = "Ảnh này đã tồn tại!";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                item.Avatar = fileName;
            }
            dbcontext.SaveChanges();
            return RedirectToAction("SettingsPage", "Admin");
=======
            var email = Session["Login"] as string;
            var ktra = dbcontext.Accounts.Where(e => e.email == email).FirstOrDefault();
            var fullName = "";
            if(ktra != null)
            {
                fullName = ktra.fullName;
            }
            return PartialView(fullName);
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
        }
    }
}