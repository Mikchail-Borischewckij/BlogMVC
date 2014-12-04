using Blogs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogs.WebUI.Models
{
    public class CommentCreateViewModel
    {
        public int idComment { get; set; }
        public int IdPost { get; set; }
        public int UsersId { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter a text of the post")]
        public string Content { get; set; }

        public DateTime create_time { get; set; }
    }

    public class CommentsViewModel
    {
        public int IdPost { get; set; }
        //public List<CommentViewModel> comments { get; set; }
        public IEnumerable<Comments> comments { get; set; }
    }
}