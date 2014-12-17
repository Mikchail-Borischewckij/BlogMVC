using System.ComponentModel.DataAnnotations;

namespace Blogs.Domain.Entities
{
    public class Link
    {   
        [Key]
        public string Name { get; set; }
        public string Url { get; set; }
        public string NameController { get; set; }
    }
}
