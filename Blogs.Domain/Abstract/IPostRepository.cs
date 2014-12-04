using Blogs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain.Abstract
{
    public interface IPostRepository
    {
        IQueryable<Post> Post { get; }
        void CreatePost(Post post);
        void SavePost(Post post);
        void DeletePost(Post post);
    }
}
