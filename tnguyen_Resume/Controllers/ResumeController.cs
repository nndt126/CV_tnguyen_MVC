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

        private readonly IWorkDAL _iWorkDAL;
        private readonly IEducationDAL _iEducationDAL;
        private readonly ISkillDAL _iSkillDAL;
        private readonly ITestimonialDAL _iTestimonialDAL;

        public ResumeController(IWorkDAL iWorkDAL, IEducationDAL iEducationDAL, ISkillDAL iSkillDAL, 
            ITestimonialDAL iTestimonialDAL)
        {
            _iWorkDAL = iWorkDAL;
            _iEducationDAL = iEducationDAL;
            _iSkillDAL = iSkillDAL;
            _iTestimonialDAL = iTestimonialDAL;
        }

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult EducationPartial()
        {
            var model = _iEducationDAL.GetAllEducation().ToList();
            return PartialView(model);
        }

        public PartialViewResult WorkPartial()
        {
            var model = _iWorkDAL.GetAllWorks().ToList();
            return PartialView(model);
        }

        public PartialViewResult SkillPartial()
        {
            var model = _iSkillDAL.GetAllSkill();
            return PartialView(model);
        }

        public PartialViewResult TestimonialPartial()
        {
            var model = _iTestimonialDAL.GetTestimonial(2);
            return PartialView(model);
        }

    }
}
