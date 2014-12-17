using System.Linq;
using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;

namespace Blogs.Domain.Concrete
{
    public class UsersRepository : IUserRepository
    {
        private BlogDataBase _blogDb = new BlogDataBase();

        public bool Create(Users user)
        {
            if (user.Id == 0)
                _blogDb.Users.Add(user);

            _blogDb.SaveChanges();
            return false;
        }

        public bool Update(Users user)
        {
            var userOfDb = _blogDb.Users.Find(user.Id);
            if (userOfDb == null)
                return false;

            userOfDb.Login = user.Login;
            userOfDb.Password = user.Password;
            userOfDb.Email = user.Email;

            _blogDb.SaveChanges();
            return true;
        }

        public bool Authentication(string userNameorEmail, string userPassword)
        {
            int hash = userPassword.GetHashCode();
            var user = from u in _blogDb.Users where u.Login == userNameorEmail && u.Password == hash.ToString() select u;
            var users = user.ToList();

            return users.Count != 0;
        }

        public bool CheckExist(string userLogin)
        {
            var list = _blogDb.Users.Where(m => m.Login.Equals(userLogin));
            var users = list.ToList();

            return users.Count != 0;
        }

        public bool Delete(Users user)
        {
            if (user == null)
                return false;

            Users userForDelete = _blogDb.Users.FirstOrDefault(p => p.Id == user.Id);
            _blogDb.Users.Remove(userForDelete);
            _blogDb.SaveChanges();
            return true;
        }

        public IQueryable<Users> Get
        {
            get
            {
                return _blogDb.Users;
            }
        }
    }
}
