using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs.Domain.Abstract;
using Blogs.Domain.Entities;

namespace Blogs.Domain.Concrete
{
    public class UsersRepository : IUserRepository
    {
        private BlogDataBase blogDB = new BlogDataBase();

        public bool CreateUser(Users user)
        {
            if (user.UserId == 0)
                blogDB.Users.Add(user);

            blogDB.SaveChanges();
            return false;
        }

        public bool ChangeUser(Users user)
        {
            Users userOfDB = blogDB.Users.Find(user.UserId);
            if (userOfDB != null)
            {
                userOfDB.Login = user.Login;
                userOfDB.Password = user.Password;
                userOfDB.Email = user.Email;
            }
         
            blogDB.SaveChanges();
            return false;
        }
        /// <summary>
        /// Authentication users
        /// </summary>
        public bool AuthenticationOfUser(string userNameorEmail, string userPassword)
        {
            int hash = userPassword.GetHashCode();
            var user = from u in blogDB.Users where u.Login == userNameorEmail && u.Password == hash.ToString() select u;
            List<Users> users = user.ToList<Users>();
            if (users.Count == 0)
                return false;

            return true;
        }

        public bool CheckExistOfUser(string userLogin)
        {
            IQueryable<Users> list = blogDB.Users.AsQueryable<Users>().Where(m => m.Login.Equals(userLogin));
            List<Users> users = list.ToList<Users>();
            if (users.Count == 0)
                return false;

            return true;
        }

        public void DeleteUser(Users user)
        {
            if (user != null)
            {
                Users userForDelete = blogDB.Users.FirstOrDefault(p => p.UserId == user.UserId);
                blogDB.Users.Remove(userForDelete);
                blogDB.SaveChanges();
            }
        }

        public IQueryable<Users> Users
        {
            get
            {
                return blogDB.Users;
            }
        }
    }
}
