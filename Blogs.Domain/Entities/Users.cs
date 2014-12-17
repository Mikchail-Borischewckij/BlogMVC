using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogs.Domain.Entities
{
    public class Users
    {
       [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<Link> Links { get; set; }

    }
}
