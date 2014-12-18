using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;

namespace Blogs.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IUserRepository _repo;
        public AdminController(IUserRepository repoParam)
        {
            _repo = repoParam;
        }

        //
        // GET: /Admin/

        [Authorize(Roles = "administrator")]
        public ActionResult AllUsersShow()
        {
            return View(_repo.Get);
        }

        [HttpGet, Authorize(Roles = "administrator")]
        public ActionResult EditUser(int? idUser)
        {

            var user = _repo.Get.FirstOrDefault(u => u.Id == idUser);
            if (user == null)
                return RedirectToAction("Error404", "Post");
            ViewBag.Role = Roles.GetRolesForUser(user.Login)[Roles.GetRolesForUser(user.Login).Length - 1];

            var list = new List<SelectListItem>
            {
                new SelectListItem() {Text = null},
                new SelectListItem() {Text = "fan"},
                new SelectListItem() {Text = "manager"},
                new SelectListItem() {Text = "administrator"}
            };
            ViewBag.Datas = list;


            return View(user);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditUser(Users user, string nameRole)
        {
            var usContr = new UsersController(_repo);
            string userLogin = usContr.GetUserNameById(user.Id);
            string curentRole = Roles.GetRolesForUser(userLogin)[Roles.GetRolesForUser(userLogin).Length - 1];

            if (!ModelState.IsValid)
                return View(user);

            if (nameRole == string.Empty || curentRole == nameRole) return RedirectToAction("AllUsersShow");
            Roles.AddUserToRole(userLogin, nameRole);
            Roles.RemoveUserFromRole(userLogin, curentRole);
            return RedirectToAction("AllUsersShow");
        }

        public ActionResult Delete(Users user)
        {
            if (user == null)
                return RedirectToAction("AllUsersShow");
            RemoveRoleOfUser(user);
            _repo.Delete(user);

            return RedirectToAction("AllUsersShow");
        }

        private void RemoveRoleOfUser(Users user)
        {
            var usContr = new UsersController(_repo);
            var curentRole =
                Roles.GetRolesForUser(usContr.GetUserNameById(user.Id))[
                    Roles.GetRolesForUser(usContr.GetUserNameById(user.Id)).Length-1];
            Roles.RemoveUserFromRole(usContr.GetUserNameById(user.Id), curentRole);
        }

    }
}
