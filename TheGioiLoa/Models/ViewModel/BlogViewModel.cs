using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGioiLoa.Models.ViewModel
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(300)]
        public string Title { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string BlogContent { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Url { get; set; }
        public int Status { get; set; }
        public string StatusLabel { get; set; }
        public string Cover { get; set; }

        public int? BlogCategoryId { get; set; }

        public int Type { get; set; }

        public BlogCategory BlogCategory { get; set; }

        public List<BlogCategory> CategoryList { get; set; }

        public List<StatusEnum> StatusList { get; set; }
    }

}