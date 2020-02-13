namespace TheGioiLoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Page")]
    public partial class Page
    {
        [StringLength(300)]
        public string PageId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
