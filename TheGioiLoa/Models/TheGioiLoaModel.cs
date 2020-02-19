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
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Product_Image> Product_Image { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .Property(e => e.Promotion)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Videos)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_Image)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Tag)
                .WithMany(e => e.Product)
                .Map(m => m.ToTable("Product_Tag").MapLeftKey("ProductId").MapRightKey("TagId"));

            modelBuilder.Entity<Product_Image>()
                .Property(e => e.ImageId)
                .IsUnicode(false);

        }
    }
}
