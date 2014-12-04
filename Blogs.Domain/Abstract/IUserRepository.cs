using Blogs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<Users> Users { get; }
        bool ChangeUser(Users user);
        bool CreateUser(Users user);
        bool AuthenticationOfUser(string userName, string userPassword);
        bool CheckExistOfUser(string userLogin);
        void DeleteUser(Users user);
   
    }
}
