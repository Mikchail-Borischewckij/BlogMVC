using System;
using System.Linq;
using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;

namespace Blogs.Domain.Concrete
{
    public class CommentsRepository: ICommentsRepository
    {
        private BlogDataBase data;

        public CommentsRepository()
        {
            data = new BlogDataBase();
        }

        public IQueryable<Comments> Get
        {
            get
            {
                return data.Comments;
            }
        }

        public void Create(Comments coment)
        {
            if (coment.Id == 0)
            {
                coment.CreateTime = DateTime.Now;
                data.Comments.Add(coment);
            }
            data.SaveChanges();
           
        }
        public void Delete(int? idComment)
        {
            if (idComment != null)
            {
                Comments comentForDelete = data.Comments.FirstOrDefault(p => p.Id == idComment);
                data.Comments.Remove(comentForDelete);
                data.SaveChanges();
            }
        }

    }
}
