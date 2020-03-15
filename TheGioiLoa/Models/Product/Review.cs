using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web.Mvc;

namespace TheGioiLoa.Models
{
    [Table("Review")]
    public partial class Review
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Review()
        {
            
        }

        [Key]
        public int ReviewId { get; set; }

        public string Comment { get; set; }

        public int StarCount { get; set; }

        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
