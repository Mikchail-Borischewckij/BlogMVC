using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain.Concrete
{
    public class PostRepository : IPostRepository
    {
        private BlogDataBase data;

        public PostRepository()
        {
            data = new BlogDataBase();
        }

        public IQueryable<Post> Post
        {
            get
            {
                return data.Posts;
            }
        }

        public void CreatePost(Post post)
        {
            if (post.IdPost == 0)
            {
                post.create_time = DateTime.Now;
                data.Posts.Add(post);
            }
            data.SaveChanges();
        }

        public void SavePost(Post post)
        {
            Post findPost = data.Posts.Find(post.IdPost);
            if (findPost != null)
            {
                findPost.Title = post.Title;
                findPost.Preview = post.Preview;
                findPost.Content = post.Content;
                findPost.tags = post.tags;
                findPost.update_time = DateTime.Now;
                data.SaveChanges();
            }
        }

        public void DeletePost(Post post)
        {
            if (post != null)
            {
                Post postForDelete = data.Posts.FirstOrDefault(p => p.IdPost == post.IdPost);
                data.Posts.Remove(postForDelete);
                data.SaveChanges();
            }
        }
    }

}
