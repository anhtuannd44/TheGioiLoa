namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 300),
                        Title = c.String(nullable: false, maxLength: 300),
                        BlogContent = c.String(storeType: "ntext"),
                        Author = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        BlogCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.BlogCategory", t => t.BlogCategoryId)
                .Index(t => t.BlogCategoryId);
            
            CreateTable(
                "dbo.BlogCategory",
                c => new
                    {
                        BlogCategoryId = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 300),
                        Name = c.String(nullable: false, maxLength: 300),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BlogCategoryId);
            
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 300),
                        Name = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 300),
                        Name = c.String(nullable: false, maxLength: 500),
                        Description = c.String(storeType: "ntext"),
                        Price = c.Double(),
                        BrandId = c.Int(),
                        ListedPrice = c.Double(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Promotion = c.String(maxLength: 100, unicode: false),
                        Characteristics = c.String(storeType: "ntext"),
                        Details = c.String(storeType: "ntext"),
                        Videos = c.String(maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Brand", t => t.BrandId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.ProductId })
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 300),
                        Name = c.String(nullable: false, maxLength: 300),
                        CategoryParentId = c.Int(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Product_Image",
                c => new
                    {
                        ImageId = c.String(nullable: false, maxLength: 128, unicode: false),
                        ProductId = c.Int(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.Page",
                c => new
                    {
                        PageId = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 300),
                        Name = c.String(nullable: false, maxLength: 100),
                        Content = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.PageId);
            
            CreateTable(
                "dbo.Product_Tag",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.TagId })
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product_Tag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.Product_Tag", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product_Image", "ProductId", "dbo.Product");
            DropForeignKey("dbo.CategoryProducts", "ProductId", "dbo.Product");
            DropForeignKey("dbo.CategoryProducts", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Product", "BrandId", "dbo.Brand");
            DropForeignKey("dbo.Blog", "BlogCategoryId", "dbo.BlogCategory");
            DropIndex("dbo.Product_Tag", new[] { "TagId" });
            DropIndex("dbo.Product_Tag", new[] { "ProductId" });
            DropIndex("dbo.Product_Image", new[] { "ProductId" });
            DropIndex("dbo.CategoryProducts", new[] { "ProductId" });
            DropIndex("dbo.CategoryProducts", new[] { "CategoryId" });
            DropIndex("dbo.Product", new[] { "BrandId" });
            DropIndex("dbo.Blog", new[] { "BlogCategoryId" });
            DropTable("dbo.Product_Tag");
            DropTable("dbo.Page");
            DropTable("dbo.Tag");
            DropTable("dbo.Product_Image");
            DropTable("dbo.Category");
            DropTable("dbo.CategoryProducts");
            DropTable("dbo.Product");
            DropTable("dbo.Brand");
            DropTable("dbo.BlogCategory");
            DropTable("dbo.Blog");
        }
    }
}
