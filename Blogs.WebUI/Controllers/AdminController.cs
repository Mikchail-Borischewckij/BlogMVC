using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;
using Blogs.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blogs.WebUI.Controllers
{
    public class AdminController : Controller
    {
          IUserRepository repo;

          public AdminController(IUserRepository repoParam)
        {
            repo = repoParam;
        }
        //
        // GET: /Admin/

        [Authorize(Roles = "administrator")]
        public ActionResult AllUsersShow()
        {
            var user = repo.Users.FirstOrDefault(u => u.Login == User.Identity.Name);

            return View(repo.Users);
        }

        [HttpGet]
        public ActionResult EditUser(int? IdUser)
        {
            Users user = repo.Users.FirstOrDefault(u => u.UserId == IdUser);
            ViewBag.Role = Roles.GetRolesForUser(user.Login)[Roles.GetRolesForUser(user.Login).Length-1];

            if (user == null)
                return RedirectToAction("Error404", "Post");

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(Users user,string nameRole)
        {
            string curentRole = Roles.GetRolesForUser(GetUserNameById(user.UserId))[Roles.GetRolesForUser(GetUserNameById(user.UserId)).Length - 1];
            if(!ModelState.IsValid)
                return View(user);
            else
            {
                if (nameRole != string.Empty && curentRole != nameRole)
                {
                    Roles.AddUserToRole(user.Login, nameRole);
                    Roles.RemoveUserFromRole(user.Login, curentRole);
                }
                user.Password = user.Password.GetHashCode().ToString();
                repo.ChangeUser(user);
                
                return RedirectToAction("AllUsersShow");
            }
        }

        public ActionResult Delete(Users user)
        {
            if (user != null)
            {
                RemoveRoleOfUser(user);
                repo.DeleteUser(user);
            }

            return RedirectToAction("AllUsersShow");
        }

        private void RemoveRoleOfUser(Users user)
        {
            string curentRole = Roles.GetRolesForUser(GetUserNameById(user.UserId))[Roles.GetRolesForUser(GetUserNameById(user.UserId)).Length - 1];
            Roles.RemoveUserFromRole(GetUserNameById(user.UserId), curentRole);
        }

        private string GetUserNameById(int? IdUser)
        {
            Users user = repo.Users.FirstOrDefault(p => p.UserId == IdUser);

            return user != null ? user.Login : "User is deleted";

        }

    }
}
