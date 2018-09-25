using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tedu.OnlineShop.Areas.Admin.Models;

namespace Tedu.OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var userDao = new UserDao();
                var user = userDao.Login(model.UserName, model.Password);
                if(user != null)
                {
                    var userSession = new UserLogin()
                    {
                        UserID = user.ID,
                        UserName = user.UserName,
                    };

                    Session.Add(Common.Constants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "HomeController");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }

            return View("Index");
        }
    }
}