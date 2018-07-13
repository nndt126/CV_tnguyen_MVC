using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnguyen_Resume.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace tnguyen_Resume.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        tnguyenResumeEntities db = new tnguyenResumeEntities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult InformationPartial()
        {
            var info = db.Information.FirstOrDefault();
            return PartialView(info);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                await MessageService.SendEmailAsync(model.FirstName, model.Phone, model.Subject, model.Message);
                return View("EmailSent");
                //string url = Request.UrlReferrer.AbsolutePath + "#contact";
                //return RedirectToAction(url);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult EmailSent()
        {
            return View();
        }

        public PartialViewResult ProjectPartialFooter()
        {
            //Lấy ra Ma User đầu tiên trong csdl
            int id_User = int.Parse(db.Projects.ToList().ElementAt(0).ID_User.ToString());
            //int id_User = 1;
            var pj = db.Projects.Where(t => t.ID_User == id_User).OrderByDescending(t => t.ProjectTime).Take(2).ToList();
            return PartialView(pj);
        }

    }
}
