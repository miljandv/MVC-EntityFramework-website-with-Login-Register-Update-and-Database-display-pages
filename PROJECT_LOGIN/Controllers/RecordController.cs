using System.Web.Mvc;
using MVC_test.WebUI;
using MVC_test.WebUI.Models;
using MVC_test.BL.Records.Manager;
using MVC_test.BL.Records.Model;
using MVC_test.Controllers;
using System.Globalization;

namespace MVC_test.Controllers
{
    public class RecordController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            var result = RecordManager.NewRecord();
            if (result.isSuccess)
            {
                return View(result.value);
            }
            return View("_Error", new ErrorModel
            {
                ErrorMessage = result.Message
            });
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(RecordEntityModel model)
        {
                var result = RecordManager.GetRecordFromDB(model.Phone);
                if (result.isSuccess) return RedirectToAction("LoggedIn",result.value);
                else return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult LoggedIn(RecordEntityModel mod)
        {; ;
            HttpContext.Response.Cookies.Add(new System.Web.HttpCookie("LoginCookie",mod.Fname+" "+" "+mod.Lname));
            HttpContext.Response.Cookies.Get("LoginCookie").Expires.AddDays(3);
            return View(mod);
        }
        [HttpGet]
        public ActionResult Users()
        {
            var result = RecordManager.LoadRecords();
            return View(result);
            /*}
            return View("_Error", new ErrorModel{
                ErrorMessage = result.Message
            });*/
        }
        [HttpGet]
        public ActionResult Update(string id)
        {
            var result = RecordManager.GetRecordFromDB(id);
            if (result.isSuccess)
            {
                return View(result.value);
            }
            return View("_Error", new ErrorModel
            {
                ErrorMessage = result.Message
            });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RecordEntityModel model)
        {
            if (ModelState.IsValid)
            {
                RecordManager.CreateDBEntry(model.Fname, model.Lname, model.Phone);
                return RedirectToAction("Users");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Remove(string id)
        {
            var result = RecordManager.RemoveRecordFromDB(id);
            if (result.isSuccess)
            {
                return RedirectToRoute(RouteNames.RecordUsers);
            }
            return View("_Error", new ErrorModel
            {
                ErrorMessage = result.Message
            });
        }
        [HttpPost]
        public ActionResult Update(RecordEntityModel model)
        {
            var saveResult = RecordManager.UpdateRecordInDB(model);

            return RedirectToRoute(RouteNames.RecordUsers);
            /* ViewBag.ErrorMessage = saveResult.Message;
             return View(model);*/
        }
    }
}