namespace TheGioiLoa.Models
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

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string BlogContent { get; set; }

        [StringLength(128)]
        public string Author { get; set; }

        public string  Cover { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int Status { get; set; }

        public int Type { get; set; }
        
        public int? BlogCategoryId { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }
    }
}
