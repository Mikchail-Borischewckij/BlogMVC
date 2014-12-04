using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;


namespace Blogs.Domain.Entities
{
    public class Post
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int IdPost { get; set; }

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
        public string tags { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime create_time { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime update_time { get; set; }
    }
}
