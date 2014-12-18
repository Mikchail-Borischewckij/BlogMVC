using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;
using Blogs.WebUI.Models;

namespace Blogs.WebUI.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Get/
        private IPostRepository _repo;
        private ICommentsRepository _repository;
        private ILikeRepository _likeRep;
        private IUserRepository _userRepos;
        private int PostsPerPage = 3;

        public PostController(IPostRepository repoParam, ICommentsRepository repoParametr, ILikeRepository likeRepos,
            IUserRepository users)
        {
            _repo = repoParam;
            _repository = repoParametr;
            _likeRep = likeRepos;
            _userRepos = users;
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult ListPost(int page = 1, string search = "")
        {
            ViewBag.Page = true;
            if (!String.IsNullOrEmpty(search))
            {
                ViewBag.Page = false;
                var modelOfSearch = SearchPost(page, search);
                if (!modelOfSearch.Post.Any())
                    return RedirectToAction("SearchNotMatch");
                return View(modelOfSearch);
            }
            var model = CreatePostList(
                _repo.Get.OrderBy(p => p.Id)
                    .Skip((page - 1)*PostsPerPage)
                    .Take(PostsPerPage)
                    .ToList(), page);

            if (!model.Post.Any())
                return RedirectToAction("SearchNotMatch");
            return View(model);
        }


        private PostListViewModel SearchPost(int page, string search, PostListViewModel model = null)
        {
            if (CheckStringIsTag(search))
            {
                var post = _repo.Get.Where(p => p.Tags.Contains(search));
                model = CreatePostList(post.ToList(), page);
                return model;
            }

            model = CreatePostList(
                _repo.Get.Where(p => p.Title.Contains(search.Trim()))
                    .OrderBy(p => p.Id)
                    .ToList(), page);
            return model;
        }

        [HttpGet]
        public ActionResult AllComment()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchNotMatch()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllPost(int? idPost)
        {
            const char separator = '#';
            var model = new FullPostViewModel();
            var post = _repo.Get.FirstOrDefault(p => p.Id == idPost);
            if (post == null)
                return RedirectToAction("Error404");

            model.Id = Convert.ToInt32(idPost);
            model.Title = post.Title;
            model.User = _userRepos.Get.First(u => u.Id == post.IdUser);
            model.Content = post.Content;
            model.CreateTime = post.CreateTime;
            model.UpdateTime = post.UpdateTime;
            model.Tags = post.Tags.Trim().Split(separator);
            model.Comments = _repository.Get.Where(p => p.IdPost == idPost);
            model.CountLike = _likeRep.Get.Count(p => p.IdPost == idPost);
            return View(model);
        }

        [HttpGet]
        public ActionResult GetAllPostByTag(string tag)
        {
            ViewBag.Page = false;
            var posts = _repo.Get.Where(p => p.Tags.Contains(tag)).ToList();
            var model = CreatePostList(posts);
            return View("ListPost", model);
        }

        [HttpGet]
        public ActionResult GetAllPostByUser(int idUser)
        {
            ViewBag.Page = false;
            var posts = _repo.Get.Where(p => p.IdUser == idUser).ToList();
            var model = CreatePostList(posts);
            return View("ListPost", model);
        }

        [NonAction]
        public PostListViewModel CreatePostList(List<Post> posts, int page = 1)
        {
            var model = new PostListViewModel()
            {
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PostsPerPage,
                    TotalItems = _repo.Get.Count()
                },
                Post = posts
            };
            return model;
        }

        private static bool CheckStringIsTag(string str)
        {
            const char separator = '#';
            var countOfTags = str.Count(t => t == separator);
            return countOfTags > 0;
        }

        [HttpPost]
        public ActionResult LikePost(int idPost)
        {
            if (!User.Identity.IsAuthenticated)
                return PartialView("LikeError");

            var user = _userRepos.Get.FirstOrDefault(p => p.Login == User.Identity.Name);
            if (user != null) _likeRep.AddOrRemove(Convert.ToInt32(idPost), user.Id);

            var model = new FullPostViewModel()
            {
                CountLike = _likeRep.Get.Count(p => p.IdPost == idPost)
            };

            return Request.IsAjaxRequest() ? PartialView("LikePartial", model) : PartialView("LikeError");
        }

        public ActionResult Error404()
        {
            return View();
        }
    }
}
