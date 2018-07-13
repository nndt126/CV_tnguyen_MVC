using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnguyen_Resume.Models;

namespace tnguyen_Resume.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/

        tnguyenResumeEntities db = new tnguyenResumeEntities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProjectPartial()
        {
            //Lấy ra Ma User đầu tiên trong csdl
            int id_User = int.Parse(db.Projects.ToList().ElementAt(0).ID_User.ToString());
            //int id_User = 1;
            var pj = db.Projects.Where(t => t.ID_User == id_User).OrderByDescending(t=>t.ProjectTime).ToList();
            return PartialView(pj);
        }

        public ActionResult AdminProject()
        {
            return View();
        }

        public JsonResult GetProjects()
        {
            List<Project> listWork = db.Projects.ToList();
            var projectModel = listWork.Select(x => new
            {
                ID = x.ID,
                ProjectTitle = x.ProjectTitle,
                ProjectInfo = x.ProjectInfo,
                ProjectDetail = x.ProjectDetail.Substring(0, x.ProjectDetail.Length >= 50 ? 50 : x.ProjectDetail.Length) + "...",
                ProjectImage = x.ProjectImage,
                ProjectJob= x.ProjectJob,
                ProjectURL = x.ProjectURL.Substring(0, x.ProjectURL.Length >= 50 ? 25 : x.ProjectURL.Length) + "...",
                ProjectTime = JsonConvert.SerializeObject(x.ProjectTime, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" }).Trim(new Char[] { '"' }),
            });

            return Json(projectModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProjectsByID(int Id)
        {
            db = new tnguyenResumeEntities();
            var jSonWork = db.Projects.Where(n => n.ID == Id).ToList();
            var infor = jSonWork.Select(x => new {
                ID = x.ID,
                ProjectImage = x.ProjectImage,
                ProjectTitle = x.ProjectTitle,
                ProjectInfo = x.ProjectInfo,
                ProjectDetail = x.ProjectDetail,
                ProjectJob = x.ProjectJob,
                ProjectURL = x.ProjectURL,
                ProjectTime = JsonConvert.SerializeObject(x.ProjectTime, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" }).Trim(new Char[] { '"' }),
            }).FirstOrDefault();
            return Json(infor, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditProject(int sID, string sProjectImage, string sProjectTitle, string sProjectInfo, string sProjectDetail, string sProjectJob, string sProjectURL, string sProjectTime)
        {
            bool status = false;
            db = new tnguyenResumeEntities();
            string sDate = sProjectTime.Trim(new Char[] { '"' });
            DateTime dtProjectTime = Convert.ToDateTime(sDate,
    System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            Project pj = db.Projects.SingleOrDefault(n => n.ID == sID);
            if (pj == null)
            {
                //Add new item
                db.Projects.Add(new Project() { ProjectTitle = sProjectTitle, ProjectInfo = sProjectInfo, ProjectDetail = sProjectDetail, ProjectJob = sProjectJob, ProjectTime = dtProjectTime, ProjectURL = sProjectURL, ProjectImage = sProjectImage, ID_User = 1 });
                db.SaveChanges();
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
                    db.SaveChanges();
                    status = true;
                }
            }

            return Json(new { success = status });
        }
    }
}
