using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogs.Domain.Abstract;
using System.Web.Security;
using Blogs.Domain.Entities;
using Blogs.WebUI.Models;

namespace Blogs.WebUI.Controllers
{
    public class UsersController : Controller
    {
          IUserRepository repo;

        public UsersController(IUserRepository repoParam)
        {
            repo = repoParam;
        }

        //  User/RegisterUsers
        [HttpGet]
        public ViewResult RegisterUsers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUsers(UserRegisterViewModel model)
        {
            if (ModelState.IsValid && !repo.CheckExistOfUser(model.Login))
            {
                Users user = new Users()
                {
                    Login = model.Login,
                    Password = model.Password.GetHashCode().ToString(),
                    Email = model.Email,
              
                };
                repo.CreateUser(user);
                Roles.AddUserToRole(user.Login, "fan");
                //Roles.AddUserToRole(user.Login, "administrator");
               
            }
            else
            {
                ModelState.AddModelError("", "User with " + model.Login + " name already exists");
                return View(model);
            }

            return RedirectToAction("Login");
        }

        //  User/login
        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ListPost", "Post");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && repo.AuthenticationOfUser(model.Login, model.Password))
                FormsAuthentication.RedirectFromLoginPage(model.Login, false);
            else
                ModelState.AddModelError("", "Incorrect login or password!");

            return View();
        }

        [Authorize]
        [HttpGet]
        public ViewResult ChangeUsersPassword()
        {
            return View();
        }

       [Authorize]
        [HttpPost]
        public ActionResult ChangeUsersPassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string oldPass = model.OldPassword.GetHashCode().ToString();
                Users user = repo.Users.FirstOrDefault(u => u.Login == User.Identity.Name && u.Password == oldPass);
                user.Password = model.Password.GetHashCode().ToString();
                if (user == null)
                    ModelState.AddModelError("", "Your old password incorrect. Try another time.");
                else
                    repo.ChangeUser(user);
            }
            return RedirectToAction("ListPost", "Post"); ;
        }

        public string GetUserNameById(int? IdUser)
        {
            Users user = repo.Users.FirstOrDefault(p => p.UserId == IdUser);

            return user != null ? user.Login : "User is deleted";
            
        }

        public ViewResult UsersAccount()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                Users user = repo.Users.FirstOrDefault(u => u.Login == User.Identity.Name);
                if (user == null)
                 ViewBag.IsAuth = false; 
                else ViewBag.IsAuth = true;

                if (User.IsInRole("manager") || User.IsInRole("administrator"))
                    ViewBag.IsChange = true;
                else ViewBag.IsChange = false;

                return View(user);
            }
            else
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }
        }

    }
}
