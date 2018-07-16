using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnguyen_Resume.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Interface;

namespace tnguyen_Resume.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private readonly IinformationDAL _iInformationDAL;
        private readonly IProjectDAL _iProjectDAL;

        public HomeController(IinformationDAL iInformationDAL, IProjectDAL iProjectDAL)
        {
            _iInformationDAL = iInformationDAL;
            _iProjectDAL = iProjectDAL;
        }


        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult InformationPartial()
        {
            var model = _iInformationDAL.GetInformation();
            return PartialView(model);
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
            var model = _iProjectDAL.GetProjects(2);
            return PartialView(model);
        }

    }
}
