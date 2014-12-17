using System;
using System.ComponentModel.DataAnnotations;

namespace Blogs.Domain.Entities
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        public int IdPost { get; set; }
        public int IdUsers { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
