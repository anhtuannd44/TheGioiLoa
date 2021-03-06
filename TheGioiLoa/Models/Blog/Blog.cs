﻿namespace TheGioiLoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Blog")]
    public partial class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [StringLength(300)]
        public string Url { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(300)]
        public string Title { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string BlogContent { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(128)]
        public string Author { get; set; }

        public string  ImageId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        //Status: 1-Public : 2-Private
        public int Status { get; set; }

        //Type: 1-Blog : 2-Page
        public int Type { get; set; }
        
        public int? BlogCategoryId { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }
        public virtual Image Image { get; set; }
    }
}
