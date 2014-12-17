using System.Linq;
using Blogs.Domain.Entities;

namespace Blogs.Domain.Abstract
{
    public interface ILikeRepository
    {
        void AddOrRemove(int idPost, int idUser);
        IQueryable<Likes> Get { get; }
    }
}
