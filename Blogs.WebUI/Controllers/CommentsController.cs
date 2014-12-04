using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;
using Blogs.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogs.WebUI.Controllers
{
    public class CommentsController : Controller
    {
        IPostRepository postRepos;
        ICommentsRepository repository;
        IUserRepository userRepo;

        public CommentsController(ICommentsRepository repoParam, IUserRepository userRepo, IPostRepository postRepos)
        {
            this.repository = repoParam;
            this.userRepo = userRepo;
            this.postRepos = postRepos;
        }

        //
        // GET: /Comments/

        public ActionResult ShowAllCommentsById(int? idPost)
        {

            CommentsViewModel model = new CommentsViewModel()
            {

                IdPost = idPost == null ? 0 : (int)idPost,
                comments = repository.Comments.Where(p => p.IdPost == idPost)
            };

            return View(model);
        }

        
        [HttpGet]
        public ViewResult CreateComment()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateComment(CommentCreateViewModel model, int? idPost)
        {
            if (ModelState.IsValid)
            {

                Comments comment = new Comments()
                {
                    Content = model.Content,
                    IdPost = idPost == null ? 0 : (int)idPost,

                };
                comment.IdUsers = userRepo.Users.FirstOrDefault(u => u.Login == User.Identity.Name).UserId;
                repository.CreateComments(comment);
            }
            else
            {
                ModelState.AddModelError("", "For some reason you can not create a comment");
                return View(model);
            }
            return RedirectToAction("AllPost", "Post", new { idPost = idPost });
        }
    }
}
