using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blogs.Domain.Entities
{
    public class Post
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Enter a title of the post")]
        [StringLength(100, ErrorMessage = "This fiels must be less the {0} symbols")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter a preview text of the post")]
        [StringLength(250, ErrorMessage = "This fiels must be less the {0} symbols")]
        public string Preview { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter a text of the post")]
        public string Content { get; set; }

        [StringLength(100, ErrorMessage = "This fiels must be less the {0} symbols")]
        public string Tags { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreateTime { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime UpdateTime { get; set; }
    }
}
