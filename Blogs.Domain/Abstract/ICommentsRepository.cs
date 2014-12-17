using System.Linq;
using Blogs.Domain.Entities;

namespace Blogs.Domain.Abstract
{
    public interface ICommentsRepository
    {
        IQueryable<Comments> Get { get; }
        void Create(Comments coment);
        void Delete(int? idComment);
    }
}
