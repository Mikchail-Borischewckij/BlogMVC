using Blogs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blogs.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }
    }
    public class PostListViewModel
    {
        public IEnumerable<Post> Post { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class FullPostViewModel
    {
        public int idPost { get; set; }
        public int IdUser { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] tags { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public IEnumerable<Comments> comments { get; set; }
    }

    public class CreatePostModel
    {
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

        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
    }
}