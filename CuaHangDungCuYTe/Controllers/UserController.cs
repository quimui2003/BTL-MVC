using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using CuaHangDungCuYTe.Models;
using CuaHangDungCuYTe.UserViewModels;

namespace CuaHangDungCuYTe.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private YteModels dbcontext = new YteModels();
        public ActionResult Index()
        {
            Session["cartSession"] = "";
            var loadsp = dbcontext.Products.ToList();
            return View(loadsp);
        }

        public ActionResult Cart(string productId)
        {
            var sp = dbcontext.Products.FirstOrDefault(pid => pid.ProductId == productId);
            if (sp == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm này!" }, JsonRequestBehavior.AllowGet);
            }

            List<CartItem> cartSession = Session["cartSession"] as List<CartItem>;
            if (cartSession == null)
            {
                cartSession = new List<CartItem>();
            }

            var existingItem = cartSession.FirstOrDefault(c => c.Product.ProductId == productId);
            if (existingItem != null)
            {
                if (Session["Login"] != null && (bool)Session["Login"] == true)
                {
                    string email = Session["Username"] as string;
                    var cartItem = dbcontext.OrdersCarts.FirstOrDefault(c => c.Email == email && c.ProductId == productId);
                    if (cartItem != null)
                    {
                        cartItem.Soluong += 1;
                        dbcontext.SaveChanges();
                    }
                }
                existingItem.Quantity += 1;
            }
            else
            {
                if (Session["Login"] != null && (bool)Session["Login"] == true)
                {
                    string email = Session["Username"] as string;
                    int ocid = 0;
                    do
                    {
                        ocid++;
                    }
                    while (dbcontext.OrdersCarts.FirstOrDefault(a => a.CartId == ocid) != null);

                    var newCartItem = new OrdersCart
                    {
                        CartId = ocid,
                        ProductId = productId,
                        Email = email,
                        Soluong = 1
                    };
                    dbcontext.OrdersCarts.Add(newCartItem);
                    dbcontext.SaveChanges();
                }
                cartSession.Add(new CartItem { Product = sp, Quantity = 1 });
            }

            Session["cartSession"] = cartSession;

            return Json(new { success = true, message = "Đã thêm vào giỏ hàng!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayCart()
        {
            var cart = Session["cartSession"] as List<CartItem>;
            if (Session["Login"] != null && (bool)Session["Login"] == true)
            {
                string email = Session["Username"] as string;
                var acc = dbcontext.OrdersCarts.Where(a => a.Email == email).ToList();

                cart = new List<CartItem>(); // Khởi tạo lại cart để đảm bảo không bị trùng lặp

                foreach (var itemCart in acc)
                {
                    var product = dbcontext.Products.FirstOrDefault(p => p.ProductId == itemCart.ProductId);
                    if (product != null)
                    {
                        var cartItem = new CartItem
                        {
                            Product = product,
                            Quantity = itemCart.Soluong
                        };
                        cart.Add(cartItem);
                    }
                }

                Session["cartSession"] = cart;
            }

            return PartialView(cart);
        }
<<<<<<< HEAD


        public ActionResult RemoveCartItem(string productId)
        {
            List<CartItem> cartSession = Session["cartSession"] as List<CartItem>;
            if (cartSession != null)
            {
                var itemToRemove = cartSession.FirstOrDefault(c => c.Product.ProductId == productId);
                if (itemToRemove != null)
                {
                    cartSession.Remove(itemToRemove);

                    if (Session["Login"] != null && (bool)Session["Login"] == true)
                    {
                        string email = Session["Username"] as string;
                        var cartItem = dbcontext.OrdersCarts.FirstOrDefault(c => c.ProductId == productId && c.Email == email);
                        if (cartItem != null)
                        {
                            dbcontext.OrdersCarts.Remove(cartItem);
                            dbcontext.SaveChanges();
                        }
                    }
                }
            }

            Session["cartSession"] = cartSession;
            return Json(new { success = true, message = "Đã xóa sản phẩm khỏi giỏ hàng!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckOrders()
        {
            if (Session["Login"] != null && (bool)Session["Login"] == true)
            {   
                return Json(new { success = true, message = "Đã đăng nhập!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Bạn chưa đăng nhập! Vui lòng đăng nhập để đặt hàng!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DonHang()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PayOrders(InfoOrderViewModels formData)
        {
            var cart = Session["cartSession"] as List<CartItem>;
            if (cart != null)
            {
                string email = Session["Username"] as string;
                var acc = dbcontext.Accounts.Where(a => a.Email == email).FirstOrDefault();
                acc.Fullname = formData.name;
                acc.Address = formData.address;
                acc.Sdt = formData.phone;
                dbcontext.SaveChanges();

                var newOrderId = "";
                int oid = 0;
                do
                {
                    oid++;
                    newOrderId = oid.ToString();
                }
                while (dbcontext.Orders.FirstOrDefault(a => a.OrderId == newOrderId) != null);

                var order = new Order
                {
                    OrderId = newOrderId,
                    Email = email,
                    OrderDate = DateTime.Now,
                    TotalPrice = cart.Sum(c => c.Quantity * c.Product.Price),
                    status = "Pending"
                };
                dbcontext.Orders.Add(order);

                foreach (var item in cart)
                {
                    var newODId = "";
                    int odid = 0;
                    do
                    {
                        odid++;
                        newODId = odid.ToString();
                    }
                    while (dbcontext.OrdersDetails.FirstOrDefault(a => a.OrderDetailId == newODId) != null);

                    var itemDetail = new OrdersDetail
                    {
                        OrderDetailId = newODId,
                        OrderId = newOrderId,
                        ProductId = item.Product.ProductId,
                        Quantity = item.Quantity,
                        Giaban = item.Product.Price
                    };
                    dbcontext.OrdersDetails.Add(itemDetail);
                    dbcontext.SaveChanges();
                }

                dbcontext.SaveChanges();
                ViewBag.Mess = "Oke!";
                Session["cartSession"] = null; // Clear the cart session after order is placed
                var cus = dbcontext.OrdersCarts.Where(c => c.Email == email).ToList();
                if (cus.Any())
                {
                    dbcontext.OrdersCarts.RemoveRange(cus);
                    dbcontext.SaveChanges();
                }
                return RedirectToAction("DonHang", "User");
            }
            else
            {
                return RedirectToAction("DonHang", "User");
            }
        }

        public ActionResult ProductsDetails(string id)
        {
            var productDetail = dbcontext.Products.FirstOrDefault(pd => pd.ProductId == id);
            if (productDetail == null)
            {
                return RedirectToAction("Index", "User");
            }

            var tenloai = dbcontext.Loais.FirstOrDefault(p => p.Maloai == productDetail.Maloai);
            if (tenloai == null)
            {
                return RedirectToAction("Index", "User");
            }
            ViewBag.TenLoai = tenloai.Tenloai;
            return View(productDetail);
        }

        public ActionResult SPTuongTu(int mloai)
        {
            var productTT = dbcontext.Products.Where(p => p.Maloai == mloai).Take(4).ToList();
            return PartialView(productTT);
        }
=======
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
    }
}