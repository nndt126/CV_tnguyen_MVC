using tnguyen_Resume.Common;
using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using tnguyen_Resume.Models;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;

namespace tnguyen_Resume.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/

        private readonly IAccountDAL _iAccountDAL;

        public AccountController(IAccountDAL iAccountDAL)
        {
            _iAccountDAL = iAccountDAL;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                // Verification.    
                if (this.Request.IsAuthenticated)
                {
                    // Info.
                    ViewBag.ReturnUrl = returnUrl;
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            ViewBag.ReturnUrl = returnUrl;
            // Info.    
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                // Verification.
                if (ModelState.IsValid)
                {
                    // Initialization.    
                    var loginInfo = _iAccountDAL.TryLogin(model.Username, Encryptor.MD5Hash(model.Password));
                    // Verification.    
                    if (loginInfo != null)
                    {
                        // Login In.    
                        this.SignInUser(loginInfo.UserName, loginInfo.ID, model.Remember);

                        ViewBag.ThongBao = "Login Successfull!";
                        Session["username"] = loginInfo.UserName;
                        //Session["imageUSer"] = info.Image;

                        //if (model.Remember)
                        //{
                        //    HttpCookie ckUserName = new HttpCookie("username");
                        //    ckUserName.Expires = DateTime.Now.AddSeconds(3600);
                        //    ckUserName.Value = loginInfo.UserName;
                        //    Response.Cookies.Add(ckUserName);

                        //    HttpCookie ckPassword = new HttpCookie("password");
                        //    ckPassword.Expires = DateTime.Now.AddSeconds(3600);
                        //    ckPassword.Value = loginInfo.Password;
                        //    Response.Cookies.Add(ckPassword);
                        //}
                        // Info.    
                        return this.RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        // Setting.    
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                // Info    
                Console.WriteLine(ex);
            }
            // If we got this far, something failed, redisplay form    
            return this.View(model);
        }

        #region Log Out method.    
        /// <summary>  
        /// POST: /Account/LogOff    
        /// </summary>  
        /// <returns>Return log off action</returns>  
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {
                // Setting.    
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign Out.    
                authenticationManager.SignOut();
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Login", "Account");
        }
        #endregion

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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.    
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.        
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Index", "Home");
        }

        private void SignInUser(string username, Guid userId, bool isPersistent)
        {
            // Initialization.    
            var claims = new List<Claim>();
            try
            {
                // Setting    
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign In.    
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
        }


        //[HttpPost]
        //public ActionResult Login(FormCollection f)
        //{
        //    //string sTaiKhoan = f["username"].ToString();
        //    //string sMatKhau = f.Get("password").ToString();
        //    //bool bRemember = Convert.ToBoolean(f["remember"].Split(',')[0]); ;
        //    //Account acc = db.Accounts.SingleOrDefault(n => n.UserName == sTaiKhoan && n.Password == sMatKhau);
        //    //if (acc != null)
        //    //{
        //    //    Information info = db.Information.Find(acc.ID);
        //    //    ViewBag.ThongBao = "Login Successfull!";
        //    //    Session["username"] = acc.UserName;
        //    //    Session["imageUSer"] = info.Image;

        //    //    if (bRemember)
        //    //    {
        //    //        HttpCookie ckUserName = new HttpCookie("username");
        //    //        ckUserName.Expires = DateTime.Now.AddSeconds(3600);
        //    //        ckUserName.Value = acc.UserName;
        //    //        Response.Cookies.Add(ckUserName);

        //    //        HttpCookie ckPassword = new HttpCookie("password");
        //    //        ckPassword.Expires = DateTime.Now.AddSeconds(3600);
        //    //        ckPassword.Value = acc.Password;
        //    //        Response.Cookies.Add(ckPassword);

        //    //        HttpCookie ckRemember = new HttpCookie("remember");
        //    //        ckRemember.Expires = DateTime.Now.AddSeconds(3600);
        //    //        ckRemember.Value = acc.Role;
        //    //        Response.Cookies.Add(ckRemember);
        //    //    }
        //    //    if (acc.Role.Equals("Admin"))
        //    //    {
        //    //        //return RedirectToAction("Index", "Home");
        //    //        return RedirectToAction("Index", "Admin");
        //    //    }
        //    //    else
        //    //        return RedirectToAction("Index", "Home");
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.ThongBao = "Enter any username and password.";
        //    //    ViewBag.ErrorUser = "Username is required.";
        //    //    ViewBag.ErrorPassword = "Password is required.";
        //    //    return View();
        //    //}
        //    return View();
        //}

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
            //var fb = new FacebookClient();
            //if(code ==null )
            //    return RedirectToAction("Login", "Account");
            //dynamic result = fb.Post("oauth/access_token", new
            //{
            //    client_id = ConfigurationManager.AppSettings["FbAppId"],
            //    client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
            //    redirect_uri = RedirectUri.AbsoluteUri,
            //    code = code
            //});
            //var accessToken = result.access_token;
            //if (!string.IsNullOrEmpty(accessToken))
            //{
            //    fb.AccessToken = accessToken;
            //    dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
            //    string email = me.email; 
            //    string firstName = me.first_name;
            //    //string middleName = me.middle_name;
            //    string lastName = me.last_name;
            //    string userName = firstName + " " + lastName;

            //    var user = new Account();
            //    user.UserName = userName;
            //    user.Role = "User";
            //    //var resultThem = ThemTaiKhoanFaceBook(user);
            //    if (user != null)
            //    {
            //        ViewBag.ThongBao = "Login Successfull!";
            //        Session["username"] = user.UserName;
            //        if (user.Role.Equals("Admin"))
            //        {
            //            //return RedirectToAction("Index", "Home");
            //            return RedirectToAction("Index", "Admin");
            //        }
            //        else
            //            return RedirectToAction("Index", "Home");
            //    }
            //}
            //return RedirectToAction("Index", "Home");
            return View();
        }

        //public Guid ThemTaiKhoanFaceBook(Account acc)
        //{
        //    var _acc = db.Accounts.SingleOrDefault(x => x.UserName == acc.UserName);
        //    if (acc != null)
        //    {
        //        db.Accounts.Add(acc);
        //        db.SaveChanges();
        //        return acc.ID;
        //    }
        //    else
        //        return acc.ID;
        //}
    }
}
