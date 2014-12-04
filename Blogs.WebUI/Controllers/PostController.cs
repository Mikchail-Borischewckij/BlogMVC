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
    public class PostController : Controller
    {
        //
        // GET: /Post/
        IPostRepository repo;
        ICommentsRepository repository;
        public int postsPerPage = 3;
        public PostController(IPostRepository repoParam, ICommentsRepository repoParametr)
        {
            repo = repoParam;
            this.repository = repoParametr;
        }

        [HttpGet]
        public ActionResult ListPost(int page = 1)
        {
            PostListViewModel model = CreatePostList(
               repo.Post.OrderBy(p => p.IdPost)
                         .Skip((page - 1) * postsPerPage)
                         .Take(postsPerPage)
                         .ToList<Post>(), page);
            return View(model);
        }

        [HttpGet]
        public ActionResult Error404()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllPost(int? idPost)
        {
            FullPostViewModel model = new FullPostViewModel();
            Post post = repo.Post.FirstOrDefault(p => p.IdPost == idPost);

            if (post == null)
                return RedirectToAction("Error404", "Post");
            else
            {
                model.idPost = Convert.ToInt32(idPost);
                model.Title = post.Title;
                model.IdUser = post.IdUser;
                model.Content = post.Content;
                model.create_time = post.create_time;
                model.update_time = post.update_time;
                model.tags = CreateArrayOfTags(post.tags);
                model.comments = repository.Comments.Where(p => p.IdPost == idPost);

                return View(model);
            }
        }

        [NonAction]
        public string[] CreateArrayOfTags(string str)
        {
            char separator = '#';
            int countOfTags = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == separator)
                    countOfTags++;
            }

            string[] tags = new string[countOfTags];

            int k = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == separator)
                {
                    do
                    {
                        if (i == str.Length - 1)
                        {
                            tags[k] += str[i];
                            break;
                        }

                        tags[k] += str[i];
                        i++;
                    } while (str[i] != separator);

                    tags[k].Trim();
                    k++;
                    i--;
                }
            }
            return tags;
        }

        [HttpGet]
        public ActionResult GetAllPostByTag(string tag)
        {
            List<Post> posts = repo.Post.Where(p => p.tags.Contains(tag)).ToList<Post>();
            if (posts == null)
                return RedirectToAction("Error404");
            else
            {
                PostListViewModel model = CreatePostList(posts);
                return View("ListPost", "Post", model);
            }
        }

        [HttpGet]
        public ActionResult GetAllPostByUser(int idUser)
        {
            List<Post> posts = repo.Post.Where(p => p.IdUser == idUser).ToList<Post>();
            if (posts == null)
                return RedirectToAction("Error404");
            else
            {
                PostListViewModel model = CreatePostList(posts);
                return View("ListPost","Post", model);
            }
        }

        [NonAction]
        public PostListViewModel CreatePostList(List<Post> posts, int page = 1)
        {
            PostListViewModel model = new PostListViewModel()
            {
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = postsPerPage,
                    TotalItems = repo.Post.Count()
                },
                Post = posts
            };

            return model;
        }

     
    }
}
