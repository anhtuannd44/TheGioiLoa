using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGioiLoa.Models
{
    [Table("Information")]
    public partial class Information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Information()
        {
            
        }

        [Key]
        public string Id { get; set; }

        public string Logo { get; set; }

        public string PageName { get; set; }

        public string Hotline { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string Youtube { get; set; }
        public string Zalo { get; set; }
    }
}
