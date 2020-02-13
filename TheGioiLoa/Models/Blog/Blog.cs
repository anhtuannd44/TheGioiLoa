namespace TheGioiLoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        [StringLength(300)]
        public string BlogId { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string BlogContent { get; set; }

        [Required]
        [StringLength(128)]
        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public bool Status { get; set; }

        [StringLength(300)]
        public string BlogCategoryId { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }
    }
}
