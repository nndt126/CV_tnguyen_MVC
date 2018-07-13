using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using tnguyen_Resume.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace tnguyen_Resume.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        tnguyenResumeEntities db = new tnguyenResumeEntities();

        public ActionResult Index()
        {
            return View();
        }

        #region Get/Edit Information Angular
        public ActionResult GetLayOutInformation()
        {
            return View();
        }
        
        public JsonResult GetInfors()
        {

            List<Information> listInfor = db.Information.ToList();
            var viewModel = listInfor.Select(x => new
            {
                ID = x.ID,
                FullName = x.FullName,
                Name = x.Name,
                Image = x.Image,
                Phone = x.Phone,
                Email = x.Email,
                Address = x.Address,
                City = x.City,
                About = x.About.Substring(0, x.About.Length >= 50 ? 50 : x.About.Length) + "...",
            });
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetInforByID(int Id)
        {
            db = new tnguyenResumeEntities();
            var jSonWork = db.Information.Where(n => n.ID == Id).ToList();
            var infor = jSonWork.Select(x => new {
                ID = x.ID,
                FullName = x.FullName,
                Name = x.Name,
                Image = x.Image,
                Phone = x.Phone,
                Email = x.Email,
                Address = x.Address,
                City = x.City,
                About = x.About,
            }).FirstOrDefault();
            return Json(infor, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditInfor(int sID, string sFullName, string sName, string sPhone, string sAddress, string sCity, string sEmail, string sAbout, string sImage )
        {
            bool status = false;
            db = new tnguyenResumeEntities();
           
            Information wk = db.Information.SingleOrDefault(n => n.ID == sID);
         
            //Update item
            if (ModelState.IsValid)
            {
                wk.FullName = sFullName;
                wk.Name = sName;
                wk.Image = sImage;
                wk.Phone = sPhone;
                wk.Address = sAddress;
                wk.City = sCity;
                wk.Email = sEmail;
                wk.About = sAbout;                
                db.SaveChanges();
                status = true;
            }

            return Json(new { success = status });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;
            var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
            var sPath = System.Web.HttpContext.Current.Request.Form["sPath"];
            var sPathResize = System.Web.HttpContext.Current.Request.Form["sPathResize"];
            if (pic.ContentLength > 0)
            {
                var fileName = Path.GetFileName(pic.FileName);
                var _ext = Path.GetExtension(pic.FileName);

                _imgname = Guid.NewGuid().ToString();
                var _comPath = Server.MapPath(sPath) + _imgname + _ext;
                _imgname = _imgname + _ext;

                ViewBag.Msg = _comPath;
                var path = _comPath;

                // Saving Image in Original Mode
                pic.SaveAs(path);

                if (sPathResize != null)
                {
                    path = Server.MapPath(sPathResize) + "m-" + _imgname + _ext;
                    pic.SaveAs(path);
                }

                // end resize
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Get/Add/Edit/Delete Work Angular

        public ActionResult GetLayOutWork()
        {
            return View();
        }

        public JsonResult GetWorks()
        {
            
            List<Work> listWork = db.Works.ToList();
            
            var viewModel = listWork.Select(x => new
            {
                ID = x.ID,
                WorksTitle = x.WorksTitle,
                WorksInfo = x.WorksInfo,
                WorksDetail = x.WorksDetail.Substring(0, x.WorksDetail.Length >= 50 ? 50 : x.WorksDetail.Length),
                WorksDate = JsonConvert.SerializeObject(x.WorksDate, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" }).Trim(new Char[] { '"' }),
            });
            
            //var result = db.Information.ToList();
            return Json(viewModel, JsonRequestBehavior.AllowGet);
            //return test;
        }

        [HttpGet]
        public JsonResult GetbyID(int Id)
        {
            db = new tnguyenResumeEntities();
            var jSonWork = db.Works.Where(n=>n.ID == Id).ToList();
            var work = jSonWork.Select(x=> new {
                ID = x.ID,
                WorksTitle = x.WorksTitle,
                WorksInfo = x.WorksInfo,
                WorksDetail = x.WorksDetail,
                WorksDate = JsonConvert.SerializeObject(x.WorksDate, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" }).Trim(new Char[] { '"' }),
            }).FirstOrDefault();
            return Json(work, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddWork(int sID, string sWorksTitle,string sWorksInfo,string sWorksDetail, string sWorkDate)
        {
            bool status = false;
            db = new tnguyenResumeEntities();
            string sDate = sWorkDate.Trim(new Char[] { '"' });
            DateTime dtWorkDate = Convert.ToDateTime(sDate,
    System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            Work wk = db.Works.SingleOrDefault(n => n.ID == sID);
            if (wk == null)
            {
                //Add new item
                //DateTime dtWorkDate = DateTime.ParseExact(sDate, "MM-dd-yyyy", null);
                db.Works.Add(new Work() { WorksTitle = sWorksTitle, WorksInfo = sWorksInfo, WorksDetail = sWorksDetail, WorksDate = dtWorkDate, ID_User = 1 });
                db.SaveChanges();
                status = true;
            }
            else
            {
                //Update item
                if (ModelState.IsValid)
                {
                    wk.WorksTitle = sWorksTitle;
                    wk.WorksInfo = sWorksInfo;
                    wk.WorksDetail = sWorksDetail;
                    //DateTime dtWorkDate = (DateTime.Parse(sDate));
                    wk.WorksDate = dtWorkDate;
                    db.SaveChanges();
                    status = true;
                }
            }

            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult DeleteWork(Work delwork)
        {
            bool status = false;
            db = new tnguyenResumeEntities();
            var work = db.Works.Find(delwork.ID);
            db.Works.Remove(work);
            db.SaveChanges();
            status = true;
            return Json(new { success = status });
        }
        #endregion

        #region Get/Add/Edit/Delete Education MVC
        //Chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult EditInformation(int ID)
        {
            Information edu = db.Information.SingleOrDefault(n => n.ID == ID);
            if (edu == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(edu);
        }


        public ActionResult EducationAdmin()
        {
            return View(db.Educations.ToList());
        }

        //Chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult EditEducations(int ID)
        {
            Education edu = db.Educations.SingleOrDefault(n => n.ID == ID);
            if (edu == null)
            {
                Response.StatusCode = 404;
                  return null;
            }
           
            return View(edu);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditEducations(Education edu, FormCollection f)
        {
            Education eduDB = db.Educations.SingleOrDefault(n => n.ID == edu.ID);
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                eduDB.EducationTitle = edu.EducationTitle;
                eduDB.EducationInfo = edu.EducationInfo;
                eduDB.EducationDetail = edu.EducationDetail;
                eduDB.EducationDate = edu.EducationDate;
                //Thực hiện cập nhật trong model
                //db.Educations.Add(eduDB);
                db.SaveChanges();
            }

            return RedirectToAction("EducationAdmin");
        }

        //Xóa sản phẩm
        [HttpGet]
        public ActionResult Xoa(int ID)
        {
            //Lấy ra đối tượng sách theo mã 
            Education edu = db.Educations.SingleOrDefault(n => n.ID == ID);
            if (edu == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(edu);
        }
        [HttpPost, ActionName("Xoa")]

        public ActionResult XacNhanXoa(int ID)
        {
            Education sach = db.Educations.SingleOrDefault(n => n.ID == ID);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Educations.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("EducationAdmin");

        }
        [HttpDelete]
        public ActionResult DeleteAjax(int id)
        {
            Delete(id);
            return RedirectToAction("EducationAdmin");
        }

        public bool Delete(int id)
        {
            try
            {
                var edu = db.Educations.Find(id);
                db.Educations.Remove(edu);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        #endregion

    }
}
