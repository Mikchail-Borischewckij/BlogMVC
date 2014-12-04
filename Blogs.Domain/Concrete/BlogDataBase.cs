using System;
using System.Data.Entity;
using Blogs.Domain.Entities;


namespace Blogs.Domain.Concrete
{
    class BlogDataBase : DbContext
    {
         public DbSet<Users> Users { get; set; }
         public DbSet<Post> Posts { get; set; }
         public DbSet<Comments> Comments { get; set; }

         static BlogDataBase()
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}

