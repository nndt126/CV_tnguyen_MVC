using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using tnguyen_Resume.Models;

namespace tnguyen_Resume.Controllers
{
    public class AccountController : Controller
    {
        
        //
        // GET: /Account/
        tnguyenResumeEntities db = new tnguyenResumeEntities();

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["username"] == null)
            {
                Account acc = checkCookie();
                if (acc == null)
                    return View();
                else
                {
                    //Session["username"] = acc.UserName;
                    if (acc.Role != null && acc.Role.Equals("Admin"))
                    {
                        //return RedirectToAction("Index", "Home");
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
            }
            else
                return RedirectToAction("Index", "Home");
           
        }

        public Account checkCookie()
        {
            Account account = null;
            string username = string.Empty, password = string.Empty;
            string bRemember = string.Empty;
            if (Response.Cookies["username"] != null)
                username = Request.Cookies["username"].Value;
            if (Response.Cookies["password"] != null)
                password = Request.Cookies["password"].Value;
            if (Response.Cookies["remember"] != null)
                bRemember = Request.Cookies["remember"].Value;
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
                account = new Account { UserName = username, Password = password, Role = bRemember };
            return account;
        }


        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string sTaiKhoan = f["username"].ToString();
            string sMatKhau = f.Get("password").ToString();
            bool bRemember = Convert.ToBoolean(f["remember"].Split(',')[0]); ;
            Account acc = db.Accounts.SingleOrDefault(n => n.UserName == sTaiKhoan && n.Password == sMatKhau);
            if (acc != null)
            {
                Information info = db.Information.Find(acc.ID);
                ViewBag.ThongBao = "Login Successfull!";
                Session["username"] = acc.UserName;
                Session["imageUSer"] = info.Image;

                if (bRemember)
                {
                    HttpCookie ckUserName = new HttpCookie("username");
                    ckUserName.Expires = DateTime.Now.AddSeconds(3600);
                    ckUserName.Value = acc.UserName;
                    Response.Cookies.Add(ckUserName);

                    HttpCookie ckPassword = new HttpCookie("password");
                    ckPassword.Expires = DateTime.Now.AddSeconds(3600);
                    ckPassword.Value = acc.Password;
                    Response.Cookies.Add(ckPassword);

                    HttpCookie ckRemember = new HttpCookie("remember");
                    ckRemember.Expires = DateTime.Now.AddSeconds(3600);
                    ckRemember.Value = acc.Role;
                    Response.Cookies.Add(ckRemember);
                }
                if (acc.Role.Equals("Admin"))
                {
                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "Admin");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Enter any username and password.";
                ViewBag.ErrorUser = "Username is required.";
                ViewBag.ErrorPassword = "Password is required.";
                return View();
            }
            
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult LoginFB()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
                {
                    client_id = ConfigurationManager.AppSettings["FbAppId"],
                    client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                    redirect_uri = RedirectUri.AbsoluteUri,
                    response_type = "code",
                    scope = "email",
                });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            if(code ==null )
                return RedirectToAction("Login", "Account");
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email; 
                string firstName = me.first_name;
                //string middleName = me.middle_name;
                string lastName = me.last_name;
                string userName = firstName + " " + lastName;

                var user = new Account();
                user.UserName = userName;
                user.Role = "User";
                //var resultThem = ThemTaiKhoanFaceBook(user);
                if (user != null)
                {
                    ViewBag.ThongBao = "Login Successfull!";
                    Session["username"] = user.UserName;
                    if (user.Role.Equals("Admin"))
                    {
                        //return RedirectToAction("Index", "Home");
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public long ThemTaiKhoanFaceBook(Account acc)
        {
            var _acc = db.Accounts.SingleOrDefault(x => x.UserName == acc.UserName);
            if (acc != null)
            {
                db.Accounts.Add(acc);
                db.SaveChanges();
                return acc.ID;
            }
            else
                return acc.ID;
        }
    }
}
