using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;

namespace tnguyen_Resume.Controllers
{
    public class AdminController : Controller
    {
        //    //
        //    // GET: /Admin/
        private readonly IWorkDAL _iWorkDAL;
        private readonly IEducationDAL _iEducationDAL;
        private readonly ISkillDAL _iSkillDAL;
        private readonly ITestimonialDAL _iTestimonialDAL;
        private readonly IinformationDAL _iInformationDAL;

        public AdminController(IWorkDAL iWorkDAL, IEducationDAL iEducationDAL, ISkillDAL iSkillDAL,
            ITestimonialDAL iTestimonialDAL, IinformationDAL iInformationDAL)
        {
            _iWorkDAL = iWorkDAL;
            _iEducationDAL = iEducationDAL;
            _iSkillDAL = iSkillDAL;
            _iTestimonialDAL = iTestimonialDAL;
            _iInformationDAL = iInformationDAL;
        }

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
            Information listInfor = _iInformationDAL.GetInformation();
            listInfor.About = listInfor.About.Substring(0, listInfor.About.Length >= 50 ? 50 : listInfor.About.Length) + "...";

            var viewModel = listInfor;
            //});
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetInforByID(Guid Id)
        {
            var jSonWork = _iInformationDAL.GetInformationById(Id);
            return Json(jSonWork, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditInfor(Guid sID, string sFullName, string sName, string sPhone, string sAddress, string sCity, string sEmail, string sAbout, string sImage)
        {
            bool status = false;
            Information wk = _iInformationDAL.GetInformationById(sID);

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
                status = _iInformationDAL.Update(wk);
            }

            return Json(new { success = status });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string guidImage = string.Empty;
            var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
            var sPath = System.Web.HttpContext.Current.Request.Form["sPath"];
            var sPathResize = System.Web.HttpContext.Current.Request.Form["sPathResize"];
            if (pic.ContentLength > 0)
            {
                var fileName = Path.GetFileName(pic.FileName);
                var _ext = Path.GetExtension(pic.FileName);

                guidImage = Guid.NewGuid().ToString();
                var _comPath = Server.MapPath(sPath) + guidImage + _ext;
                guidImage = guidImage + _ext;

                ViewBag.Msg = _comPath;
                var path = _comPath;

                // Saving Image in Original Mode
                pic.SaveAs(path);

                if (sPathResize != null)
                {
                    path = Server.MapPath(sPathResize) + "m-" + guidImage + _ext;
                    pic.SaveAs(path);
                }

                // end resize
            }
            return Json(Convert.ToString(guidImage), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Get/Add/Edit/Delete Work Angular

        public ActionResult GetLayOutWork()
        {
            return View();
        }

        public JsonResult GetWorks()
        {
            var listWork = _iWorkDAL.GetAllWorks().ToList();

            var viewModel = listWork.Select(x => new
            {
                ID = x.ID,
                WorksTitle = x.WorksTitle,
                WorksInfo = x.WorksInfo,
                WorksDetail = x.WorksDetail.Substring(0, x.WorksDetail.Length >= 50 ? 50 : x.WorksDetail.Length),
                WorksDate = JsonConvert.SerializeObject(x.WorksDate, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" }).Trim(new Char[] { '"' }),
            });

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetWorkById(Guid Id)
        {
            var jSonWork = _iWorkDAL.GetWorkById(Id);
            //DateTime dt = DateTime.ParseExact(jSonWork.WorksDate.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            string s = jSonWork.WorksDate.Value.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

            //jSonWork.WorksDate = DateTime.ParseExact(jSonWork.WorksDate.ToString(),
            //                             "ddMMyyyy",
            //                             CultureInfo.InvariantCulture);
            //jSonWork.WorksDate = JsonConvert.SerializeObject(jSonWork.WorksDate, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" }).Trim(new Char[] { '"' }) ?? null;
            //var work = jSonWork.Select(x => new
            //{
            //    ID = x.ID,
            //    WorksTitle = x.WorksTitle,
            //    WorksInfo = x.WorksInfo,
            //    WorksDetail = x.WorksDetail,
            //    WorksDate = JsonConvert.SerializeObject(x.WorksDate, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" }).Trim(new Char[] { '"' }),
            //}).FirstOrDefault();
            //var jsonData = new[] { jSonWork, s };
            return Json(jSonWork, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddWork(Guid sID, string sWorksTitle, string sWorksInfo, string sWorksDetail, string sWorkDate)
        {
            bool status = false;
            string sDate = sWorkDate.Trim(new Char[] { '"' });
            DateTime dtWorkDate = Convert.ToDateTime(sDate,
                    CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            Work wk = _iWorkDAL.GetWorkById(sID);
            string userID = User.Identity.GetUserId();
            if (wk == null)
            {
                //Add new item
                //DateTime dtWorkDate = DateTime.ParseExact(sDate, "MM-dd-yyyy", null);
                wk = new Work()
                {
                    WorksTitle = sWorksTitle,
                    WorksInfo = sWorksInfo,
                    WorksDetail = sWorksDetail,
                    WorksDate = dtWorkDate,
                    ID_User = new Guid(userID)
                };
                Guid Id = _iWorkDAL.Insert(wk);
                if(Id != Guid.Empty)
                {
                    status = true;
                }
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
                    _iWorkDAL.Update(wk);
                    status = true;
                }
            }

            return Json(new { success = status });
        }

        [HttpPost]
        public JsonResult DeleteWork(Work delwork)
        {
            bool status = false;
            status = _iWorkDAL.Delete(delwork);
            return Json(new { success = status });
        }
        #endregion

        //    #region Get/Add/Edit/Delete Education MVC
        //    //Chỉnh sửa sản phẩm
        //    [HttpGet]
        //    public ActionResult EditInformation(Guid ID)
        //    {
        //        Information edu = db.Information.SingleOrDefault(n => n.ID == ID);
        //        if (edu == null)
        //        {
        //            Response.StatusCode = 404;
        //            return null;
        //        }

        //        return View(edu);
        //    }


        //    public ActionResult EducationAdmin()
        //    {
        //        return View(db.Educations.ToList());
        //    }

        //    //Chỉnh sửa sản phẩm
        //    [HttpGet]
        //    public ActionResult EditEducations(Guid ID)
        //    {
        //        Education edu = db.Educations.SingleOrDefault(n => n.ID == ID);
        //        if (edu == null)
        //        {
        //            Response.StatusCode = 404;
        //              return null;
        //        }

        //        return View(edu);
        //    }

        //    [HttpPost]
        //    [ValidateInput(false)]
        //    public ActionResult EditEducations(Education edu, FormCollection f)
        //    {
        //        Education eduDB = db.Educations.SingleOrDefault(n => n.ID == edu.ID);
        //        //Thêm vào cơ sở dữ liệu
        //        if (ModelState.IsValid)
        //        {
        //            eduDB.EducationTitle = edu.EducationTitle;
        //            eduDB.EducationInfo = edu.EducationInfo;
        //            eduDB.EducationDetail = edu.EducationDetail;
        //            eduDB.EducationDate = edu.EducationDate;
        //            //Thực hiện cập nhật trong model
        //            //db.Educations.Add(eduDB);
        //            db.SaveChanges();
        //        }

        //        return RedirectToAction("EducationAdmin");
        //    }

        //    //Xóa sản phẩm
        //    [HttpGet]
        //    public ActionResult Xoa(Guid ID)
        //    {
        //        //Lấy ra đối tượng sách theo mã 
        //        Education edu = db.Educations.SingleOrDefault(n => n.ID == ID);
        //        if (edu == null)
        //        {
        //            Response.StatusCode = 404;
        //            return null;
        //        }

        //        return View(edu);
        //    }
        //    [HttpPost, ActionName("Xoa")]

        //    public ActionResult XacNhanXoa(Guid ID)
        //    {
        //        Education sach = db.Educations.SingleOrDefault(n => n.ID == ID);
        //        if (sach == null)
        //        {
        //            Response.StatusCode = 404;
        //            return null;
        //        }
        //        db.Educations.Remove(sach);
        //        db.SaveChanges();
        //        return RedirectToAction("EducationAdmin");

        //    }
        //    [HttpDelete]
        //    public ActionResult DeleteAjax(int id)
        //    {
        //        Delete(id);
        //        return RedirectToAction("EducationAdmin");
        //    }

        //    public bool Delete(int id)
        //    {
        //        try
        //        {
        //            var edu = db.Educations.Find(id);
        //            db.Educations.Remove(edu);
        //            db.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }

        //    }
        //    #endregion

    }
}
