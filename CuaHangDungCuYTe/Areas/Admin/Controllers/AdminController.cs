using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangDungCuYTe.Models;

namespace CuaHangDungCuYTe.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin

        private YteModels dbcontext = new YteModels();
        public ActionResult Index()
        {
            var login = Session["Login"] as string;
            ViewBag.Login = login;
            return View();
        }

        public ActionResult Profiles()
        {
            var email = Session["Login"] as string;
            var ktra = dbcontext.Accounts.Where(e => e.email == email).FirstOrDefault();
            var fullName = "";
            if(ktra != null)
            {
                fullName = ktra.fullName;
            }
            return PartialView(fullName);
        }
    }
}