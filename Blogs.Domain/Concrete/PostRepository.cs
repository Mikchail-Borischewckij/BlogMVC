using System;
using System.Linq;
using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;

namespace Blogs.Domain.Concrete
{
    public class PostRepository : IPostRepository
    {
        private BlogDataBase data;

        public PostRepository()
        {
            data = new BlogDataBase();
        }

        public IQueryable<Post> Get
        {
            get
            {
                return data.Posts;
            }
        }

        public bool Create(Post post)
        {
            if (post.Id != 0)
                return false;

            post.CreateTime = DateTime.Now;
            data.Posts.Add(post);
            data.SaveChanges();
            return true;
        }

        public bool Update(Post post)
        {
            var findPost = data.Posts.Find(post.Id);
            if (findPost == null)
                return false;

            findPost.Title = post.Title;
            findPost.Preview = post.Preview;
            findPost.Content = post.Content;
            findPost.Tags = post.Tags;
            findPost.UpdateTime = DateTime.Now;
            data.SaveChanges();
            return true;
        }

        public bool Delete(Post post)
        {
            if (post == null)
                return false;

            var postForDelete = data.Posts.FirstOrDefault(p => p.Id == post.Id);
            data.Posts.Remove(postForDelete);
            data.SaveChanges();
            return true;
        }
    }

}
