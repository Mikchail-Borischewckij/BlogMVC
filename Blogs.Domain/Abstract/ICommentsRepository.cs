using Blogs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain.Abstract
{
    public interface ICommentsRepository
    {
        IQueryable<Comments> Comments { get; }
        void CreateComments(Comments coment);
        void DeleteComment(Comments coment);
    }
}
