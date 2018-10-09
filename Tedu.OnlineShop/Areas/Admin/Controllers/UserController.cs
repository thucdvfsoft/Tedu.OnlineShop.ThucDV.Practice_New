using Model.Dao;
using Model.EF;
using System.Web.Mvc;
using Tedu.OnlineShop.Common;

namespace Tedu.OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(long id)
        {
            var dao = new UserDao();
            var user = dao.ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var userEncryptMD5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = userEncryptMD5Pas;

                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if(!string.IsNullOrEmpty(user.Password))
                {
                    var userEncryptMD5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = userEncryptMD5Pas;
                }
                bool result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user không thành công");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var dao = new UserDao();
            bool result = dao.Delete(id);

            return RedirectToAction("Index");
        }
    }
}