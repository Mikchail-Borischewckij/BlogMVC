using System;
using System.ComponentModel.DataAnnotations;
using Blogs.Domain.Entities;

namespace Blogs.WebUI.Models
{
    public class CommentCreateViewModel
    {
        public int Id { get; set; }
        public virtual Post Post { get; set; }
        public virtual  Users User{ get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter a text of the post")]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }
}