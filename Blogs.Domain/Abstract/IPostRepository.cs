using System.Linq;
using Blogs.Domain.Entities;

namespace Blogs.Domain.Abstract
{
    public interface IPostRepository
    {
        IQueryable<Post> Get { get; }
        bool Create(Post post);
        bool Update(Post post);
        bool Delete(Post post);
    }
}
