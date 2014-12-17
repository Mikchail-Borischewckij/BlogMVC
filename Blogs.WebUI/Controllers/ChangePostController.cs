using System;
using System.Linq;
using System.Web.Mvc;
using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;
using Blogs.WebUI.Models;

namespace Blogs.WebUI.Controllers
{
    public class ChangePostController : Controller
    {
        //
        // GET: /ChangePost/

        private IUserRepository _userRepo;
        private IPostRepository _postRepo;

        public ChangePostController(IPostRepository postRepo, IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _postRepo = postRepo;
        }

        [Authorize]
        public ActionResult Index()
        {
            var user = _userRepo.Get.FirstOrDefault(u => u.Login == User.Identity.Name);
            if (User.IsInRole("manager"))
                return View(_postRepo.Get.Where(p => p.IdUser == user.Id));

            if (User.IsInRole("administrator"))
                return View(_postRepo.Get);

            return RedirectToAction("ListPost", "Post");
        }

        [HttpGet]
        public ActionResult Edit(int idPost)
        {
            var post = _postRepo.Get.FirstOrDefault(p => p.Id == idPost);
            if (post == null)
                return RedirectToAction("Error404", "Post");

            return View(post);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Post post)
        {
            if (!ModelState.IsValid)
                return View(post);

            var firstOrDefault = _userRepo.Get.FirstOrDefault(u => u.Login == User.Identity.Name);
            if (firstOrDefault != null)
                post.IdUser = firstOrDefault.Id;
            _postRepo.Update(post);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int idPost)
        {
            _postRepo.Delete(_postRepo.Get.FirstOrDefault(p => p.Id == idPost));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult CreatePost()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreatePost(CreatePostModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "For some reason you can not create a post");
                return View(model);
            }

            var firstOrDefault = _userRepo.Get.FirstOrDefault(u => u.Login == User.Identity.Name);
            if (firstOrDefault == null)
                return RedirectToAction("Error404", "Post");

            var post = new Post
            {
                Title = model.Title,
                Preview = model.Preview,
                Content = model.Content,
                UpdateTime = DateTime.Today,
                Tags = String.Concat('#', model.Tags),
                IdUser = firstOrDefault.Id
            };
            _postRepo.Create(post);
            return RedirectToAction("Index");
        }
    }
}
