using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blogs.Domain.Entities
{
   public class Comments
    {
       [HiddenInput(DisplayValue = false)]
        [Key]
        public int IdComments { get; set; }

       [HiddenInput(DisplayValue = false)]
        public int IdPost { get; set; }

       [HiddenInput(DisplayValue = false)]
       public int IdUsers { get; set; }

        public string Content { get; set; }

       [HiddenInput(DisplayValue = false)]
        public DateTime create_time { get; set; }
    }
}
