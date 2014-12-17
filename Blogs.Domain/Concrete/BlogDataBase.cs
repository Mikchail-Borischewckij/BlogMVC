using System.Data.Entity;
using System.Data.Entity.SqlServer;
using Blogs.Domain.Entities;

namespace Blogs.Domain.Concrete
{
    class BlogDataBase : DbContext
    {
         public DbSet<Users> Users { get; set; }
         public DbSet<Post> Posts { get; set; }
         public DbSet<Comments> Comments { get; set; }
         public DbSet<Likes> Likes { get; set; }

         static BlogDataBase()
        {
            var ensureDllIsCopied = SqlProviderServices.Instance;
        }
    }
}

