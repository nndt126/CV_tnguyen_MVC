using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnguyen_Resume.Models;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;

namespace tnguyen_Resume.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/

        private readonly IProjectDAL _iProjectDAL;

        public ProjectController(IProjectDAL iProjectDAL)
        {
            _iProjectDAL = iProjectDAL;
        }


        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProjectPartial()
        {
            var model = _iProjectDAL.GetProjects(0);
            return PartialView(model);
        }

        public ActionResult AdminProject()
        {
            return View();
        }

        public JsonResult GetProjects()
        {
            List<Project> listWork = _iProjectDAL.GetProjects(0).ToList();
            var projectModel = listWork.Select(x => new
            {
                ID = x.ID,
                ProjectTitle = x.ProjectTitle,
                ProjectInfo = x.ProjectInfo,
                ProjectDetail = x.ProjectDetail.Substring(0, x.ProjectDetail.Length >= 50 ? 50 : x.ProjectDetail.Length) + "...",
                ProjectImage = x.ProjectImage,
                ProjectJob = x.ProjectJob,
                ProjectURL = x.ProjectURL.Substring(0, x.ProjectURL.Length >= 50 ? 25 : x.ProjectURL.Length) + "...",
                ProjectTime = JsonConvert.SerializeObject(x.ProjectTime, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" }).Trim(new Char[] { '"' }),
            }).ToList();

            return Json(projectModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProjectsByID(Guid Id)
        {
            var jSonWork = _iProjectDAL.GetProjectById(Id);
            string sProjectTime = jSonWork.ProjectTime.Value.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            var data = new { jSonWork, sProjectTime };
            //var infor = jSonWork.Select(x => new
            //{
            //    ID = x.ID,
            //    ProjectImage = x.ProjectImage,
            //    ProjectTitle = x.ProjectTitle,
            //    ProjectInfo = x.ProjectInfo,
            //    ProjectDetail = x.ProjectDetail,
            //    ProjectJob = x.ProjectJob,
            //    ProjectURL = x.ProjectURL,
            //    ProjectTime = JsonConvert.SerializeObject(x.ProjectTime, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" }).Trim(new Char[] { '"' }),
            //}).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditProject(Guid sID, string sProjectImage, string sProjectTitle, string sProjectInfo, string sProjectDetail, string sProjectJob, string sProjectURL, string sProjectTime)
        {
            bool status = false;
            string sDate = sProjectTime.Trim(new Char[] { '"' });
            DateTime dtProjectTime = Convert.ToDateTime(sDate,
    System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            Project pj = _iProjectDAL.GetProjectById(sID);
            if (pj == null)
            {
                var prj = new Project() {
                    ProjectTitle = sProjectTitle,
                    ProjectInfo = sProjectInfo,
                    ProjectDetail = sProjectDetail,
                    ProjectJob = sProjectJob,
                    ProjectTime = dtProjectTime,
                    ProjectURL = sProjectURL,
                    ProjectImage = sProjectImage,
                    ID_User = null };
                //Add new item
                Guid prjID = _iProjectDAL.Insert(prj);
                if (prjID != Guid.Empty)
                    status = true;
            }
            else
            {
                //Update project with ID
                if (ModelState.IsValid)
                {
                    pj.ProjectTitle = sProjectTitle;
                    pj.ProjectInfo = sProjectInfo;
                    pj.ProjectDetail = sProjectDetail;
                    pj.ProjectJob = sProjectJob;
                    pj.ProjectTime = dtProjectTime;
                    pj.ProjectURL = sProjectURL;
                    pj.ProjectImage = sProjectImage;
                    _iProjectDAL.Update(pj);
                    status = true;
                }
            }

            return Json(status);
        }
    }
}
