using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blogs.Domain.Concrete
{
    public class CommentsRepository: ICommentsRepository
    {
        private BlogDataBase data;

        public CommentsRepository()
        {
            data = new BlogDataBase();
        }

        public IQueryable<Comments> Comments
        {
            get
            {
                return data.Comments;
            }
        }

        public void CreateComments(Comments coment)
        {
            if (coment.IdComments == 0)
            {
                coment.create_time = DateTime.Now;
                data.Comments.Add(coment);
            }
            data.SaveChanges();
           
        }
        public void DeleteComment(Comments coment)
        {
            if (coment != null)
            {
                Comments comentForDelete = data.Comments.FirstOrDefault(p => p.IdComments == coment.IdComments);
                data.Comments.Remove(comentForDelete);
                data.SaveChanges();
            }
        }
      
    }
}
