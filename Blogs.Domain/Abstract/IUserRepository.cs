using System.Linq;
using Blogs.Domain.Entities;

namespace Blogs.Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<Users> Get { get; }
        bool Update(Users user);
        bool Create(Users user);
        bool Authentication(string userName, string userPassword);
        bool CheckExist(string userLogin);
        bool Delete(Users user);
   
    }
}
