using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TheGioiLoa.Models
{
    public partial class TheGioiLoaModel : DbContext
    {
        public TheGioiLoaModel()
            : base("name=TheGioiLoaModels")
        {
        }

        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<BlogCategory> BlogCategory { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryProducts> CategoryProduct { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Product_Images> Product_Images { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Product_Tag> Product_Tag { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<ProductHomePage> ProductHomePage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .Property(e => e.Promotion)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_Images)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_Images>()
                .Property(e => e.ImageId)
                .IsUnicode(false);

        }
    }
}
