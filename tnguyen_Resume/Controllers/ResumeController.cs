using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnguyen_Resume.Models;
using tnguyenResume.Bussiness.Interface;

namespace tnguyen_Resume.Controllers
{
    public class ResumeController : Controller
    {
        //
        // GET: /Resume/
        tnguyenResumeEntities db = new tnguyenResumeEntities();

        private readonly IWorkDAL _iWorkDAL;

        public ResumeController(IWorkDAL iWorkDAL)
        {
            _iWorkDAL = iWorkDAL;
        }

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult EducationPartial()
        {
            ////Lấy ra Ma User đầu tiên trong csdl
            //Guid id_User = db.Educations.ToList().ElementAt(0).ID_User ?? Guid.Empty;
            ////int id_User = 1;
            //var edu = db.Educations.Where(t => t.ID_User == id_User).OrderBy(t => t.EducationDate.Value.Year).ToList();
            return PartialView();
        }

        public PartialViewResult WorkPartial()
        {
            var model = _iWorkDAL.GetAllWorks().ToList();
            return PartialView(model);
        }

        //public PartialViewResult WorkPartial()
        //{
        //    //Lấy ra Ma User đầu tiên trong csdl
        //    int id_User = int.Parse(db.Works.ToList().ElementAt(0).ID_User.ToString());
        //    //int id_User = 1;
        //    var wk = db.Works.Where(t => t.ID_User == id_User).OrderBy(t => t.WorksDate.Value.Year).ToList();
        //    return PartialView(wk);
        //}

        public PartialViewResult SkillPartial()
        {
            ////Lấy ra Ma User đầu tiên trong csdl
            //Guid id_User = db.Skills.ToList().ElementAt(0).ID_User ?? Guid.Empty;
            ////int id_User = 1;
            //var skill = db.Skills.Where(t => t.ID_User == id_User).ToList();
            return PartialView();
        }

        public PartialViewResult TestimonialPartial()
        {
            ////Lấy ra Ma User đầu tiên trong csdl
            //Guid id_User = db.Testimonials.ToList().ElementAt(0).ID_User ?? Guid.Empty;
            ////int id_User = 1;
            //var tm = db.Testimonials.Where(t => t.ID_User == id_User).Take(3).ToList();
            return PartialView();
        }

    }
}
