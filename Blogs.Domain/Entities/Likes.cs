using System.ComponentModel.DataAnnotations;

namespace Blogs.Domain.Entities
{
    public class Likes
    {
        [Key]
        public int Id { get; set; }
        public int IdPost { get; set; }
        public int IdUser { get; set; }
    }
}
