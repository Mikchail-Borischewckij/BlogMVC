using System.Linq;
using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;

namespace Blogs.Domain.Concrete
{
    public class LikeRepository : ILikeRepository
    {

        private BlogDataBase data;

        public LikeRepository()
        {
            data = new BlogDataBase();
        }

        public void AddOrRemove(int idPost, int idUser)
        {
            var likeofUser = (from like in data.Likes where like.IdPost == idPost where like.IdUser == idUser select like).ToList();

            if (!likeofUser.Any())
            {
                var likes = new Likes()
                {
                    IdPost = idPost,
                    IdUser = idUser
                };
                data.Likes.Add(likes);
            }
            else
            {
                var likeForDelete = data.Likes.FirstOrDefault(p => p.IdPost == idPost && p.IdUser == idUser);
                data.Likes.Remove(likeForDelete);
            }
            data.SaveChanges();
        }

        public IQueryable<Likes> Get
        {
            get
            {
                return data.Likes;
            }
        }
    }
}
