using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;
using Blogs.WebUI.Models;

namespace Blogs.WebUI.Controllers
{
    public class UsersController : Controller
    {
        private IUserRepository _repo;

        public UsersController(IUserRepository repoParam)
        {
            _repo = repoParam;
        }

        //  User/RegisterUsers
        [HttpGet]
        public ViewResult RegisterUsers()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RegisterUsers(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid || _repo.CheckExist(model.Login))
            {
                ModelState.AddModelError("", "User with name already exists");
                return View(model);
            }
            var user = new Users()
            {
                Login = model.Login,
                Password = model.Password.GetHashCode().ToString(),
                Email = model.Email,
            };
            _repo.Create(user);
            Roles.AddUserToRole(user.Login, "fan");
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

        [HttpPost, ValidateInput(false)]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && _repo.Authentication(model.Login, model.Password))
                FormsAuthentication.RedirectFromLoginPage(model.Login, false);
            else
                ModelState.AddModelError("", "Incorrect login or password!");

            return View();
        }

        [HttpGet, Authorize]
        public ViewResult ChangeUsersPassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult ChangeUsersPassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Your old password incorrect. Try another time.");
                return View();
            }
            var oldPass = model.OldPassword.GetHashCode().ToString();
            var user = _repo.Get.FirstOrDefault(u => u.Login == User.Identity.Name && u.Password == oldPass);

            if (user == null)
            {
                ModelState.AddModelError("", "Your old password incorrect. Try another time.");
                return View();
            }
            user.Password = model.Password.GetHashCode().ToString();
            _repo.Update(user);

            return RedirectToAction("ListPost", "Post");
        }

        public string GetUserNameById(int? idUser)
        {
            if (idUser == null)
                return "User is deleted";
            var user = _repo.Get.FirstOrDefault(p => p.Id == idUser);
            return user != null ? user.Login : "User is deleted";
        }

        public ViewResult UsersAccount()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var defaultUser = new Users {Links = GetLinksOfAnonimusUsers()};
                return View(defaultUser);
            }

            var user = _repo.Get.FirstOrDefault(u => u.Login == User.Identity.Name);
            if (user != null)
                user.Links = GetLinksOfAutorizireUsers();

            return View(user);
        }

        private static IEnumerable<Link> GetLinksOfAnonimusUsers()
        {
            return new List<Link>
            {
                new Link {Name = "Log in", Url = "/Login", NameController = "Users"},
                new Link {Name = "Sign up", Url = "/RegisterUsers", NameController = "Users"}
            };
        }

        private IEnumerable<Link> GetLinksOfAutorizireUsers()
        {
            var links = new List<Link>();
            if (User.IsInRole("administrator"))
                links.Add(new Link {Name = "Users", Url = "/AllUsersShow", NameController = "Admin"});

            if (!User.IsInRole("fan"))
            {
                links.Add(new Link {Name = "Edit posts", Url = "/Index", NameController = "ChangePost"});
                links.Add(new Link {Name = "Create post", Url = "/CreatePost", NameController = "ChangePost"});
            }

            links.Add(new Link {Name = "Change password", Url = "/ChangeUsersPassword", NameController = "Users"});
            links.Add(new Link {Name = "Log out", Url = "/Logout", NameController = "Users"});
            return links;
        }
    }
}
