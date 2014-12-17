using System.Linq;
using System.Web.Mvc;
using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;
using Blogs.WebUI.Models;

namespace Blogs.WebUI.Controllers
{
    public class CommentsController : Controller
    {
        private IPostRepository _postRepos;
        private ICommentsRepository _repository;
        private IUserRepository _userRepo;

        public CommentsController(ICommentsRepository repoParam, IUserRepository userRepo, IPostRepository postRepos)
        {
            _repository = repoParam;
            _userRepo = userRepo;
            _postRepos = postRepos;
        }

        //
        // GET: /Comments/

        [HttpGet]
        public ViewResult CreateComment()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateComment(CommentCreateViewModel model, int? idPost)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "For some reason you can not create a comment");
                return View(model);
            }

            if (!User.Identity.IsAuthenticated)
                return PartialView("CommentError");

            var firstOrDefault = _userRepo.Get.FirstOrDefault(u => u.Login == User.Identity.Name);
            if (firstOrDefault == null)
            {
                ModelState.AddModelError("", "For some reason you can not create a comment");
                return View(model);
            }
            var comment = new Comments
            {
                Content = model.Content,
                IdPost = idPost ?? 0,
                IdUsers = firstOrDefault.Id,
            };
            _repository.Create(comment);
            model.CreateTime = comment.CreateTime;
            model.User = firstOrDefault;
            model.Post = _postRepos.Get.FirstOrDefault(p => p.Id == idPost);

            if (Request.IsAjaxRequest())
                return PartialView("Comment", model);

            return RedirectToAction("AllPost", "Post", new {idPost});
        }

        [HttpPost]
        public ActionResult DeleteComment(int? idComment, int? idPost)
        {
            if (!User.IsInRole("administrator") || idComment == 0)
                return RedirectToAction("AllPost", "Post", new {idPost});
            if (idComment != null)
                _repository.Delete(idComment);
            return RedirectToAction("AllPost", "Post", new {idPost});
        }
    }
}
