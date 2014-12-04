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
    public class ChangePostController : Controller
    {
        //
        // GET: /ChangePost/

        IUserRepository userRepo;
        IPostRepository postRepo;

        public ChangePostController(IPostRepository postRepo, IUserRepository userRepo)
        {
            this.userRepo = userRepo;
            this.postRepo = postRepo;
        }

        [Authorize]
        public ActionResult Index()
        {
            Users user = userRepo.Users.FirstOrDefault(u => u.Login == User.Identity.Name);

            if (User.IsInRole("manager"))
                return View(postRepo.Post.Where(p => p.IdUser == user.UserId));
            else
            {
                if (User.IsInRole("administrator"))
                    return View(postRepo.Post);
                else return RedirectToAction("ListPost", "Post");
            }
        }

        [HttpGet]
        public ActionResult Edit(int IdPost)
        {
            Post post = postRepo.Post.FirstOrDefault(p => p.IdPost == IdPost);
            if (post == null)
                return RedirectToAction("Error404", "Post");
            else
                return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (!ModelState.IsValid)
                return View(post);
            else
            {
                post.IdUser = userRepo.Users.FirstOrDefault(u => u.Login == User.Identity.Name).UserId;
                postRepo.SavePost(post);

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(Post post)
        {
            if (post != null)
                postRepo.DeletePost(post);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(CreatePostModel model)
        {
            if (ModelState.IsValid)
            {

                Post post = new Post()
                {
                    Title = model.Title,
                    Preview = model.Preview,
                    Content = model.Content,
                    update_time = DateTime.Today,
                    tags = String.Concat('#', model.tags)

                };
                post.IdUser = userRepo.Users.FirstOrDefault(u => u.Login == User.Identity.Name).UserId;
                postRepo.CreatePost(post);
            }
            else
            {
                ModelState.AddModelError("", "For some reason you can not create a post");
                return View(model);
            }
            return RedirectToAction("Index");
        }

      




    }
}
