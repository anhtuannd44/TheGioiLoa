using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGioiLoa.Models
{
    [Table("ProductHomePage")]
    public partial class ProductHomePage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductHomePage()
        {

        }

        [Key]
        public string Id { get; set; }

        public int? CategoryId {get;set;}
        public int Position { get; set; }
        public string YoutubeLink { get; set; }
        public bool IsShow { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public virtual Category Category{ get; set; }
    }
}
