using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangDungCuYTe.Models;

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

        public ActionResult Cart(int productId)
        {
            var sp = dbcontext.Products.FirstOrDefault(pid => pid.productId == productId);
            if (sp == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm này!" }, JsonRequestBehavior.AllowGet);
            }

            List<Product> cartSession = Session["cartSession"] as List<Product>;
            if (cartSession == null)
            {
                cartSession = new List<Product>();
            }

            cartSession.Add(sp);
            Session["cartSession"] = cartSession;

            return Json(new { success = true, message = "Đã thêm vào giỏ hàng!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayCart()
        {
            var cart = Session["cartSession"] as List<Product>;
            return PartialView(cart);
        }
    }
}