using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Blogs.Domain.Entities;

namespace Blogs.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { private get; set; }
        public int ItemsPerPage { private get; set; }
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
        public int Id { get; set; }
        public virtual  Users User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public IEnumerable<Comments> Comments { get; set; }
        public int CountLike { get; set; }
     }

    public class CreatePostModel
    {
        public Users User { get; set; }

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

        [Required(ErrorMessage = "Enter a tags of the post")]
        [StringLength(100, ErrorMessage = "This fiels must be less the {0} symbols")]
        public string Tags { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}