namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetImage : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Product_Image", newName: "Product_Images");
            AddColumn("dbo.Blog", "ImageId", c => c.String(maxLength: 128));
            AddColumn("dbo.Product", "Services", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Product", "ImageId", c => c.String(maxLength: 128));
            AddColumn("dbo.Product", "Contents", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Product", "Specifications", c => c.String(storeType: "ntext"));
            CreateIndex("dbo.Blog", "ImageId");
            CreateIndex("dbo.Product", "ImageId");
            AddForeignKey("dbo.Blog", "ImageId", "dbo.Image", "ImageId");
            AddForeignKey("dbo.Product", "ImageId", "dbo.Image", "ImageId");
            DropColumn("dbo.Blog", "Cover");
            DropColumn("dbo.Product", "Cover");
            DropColumn("dbo.Product", "Characteristics");
            DropColumn("dbo.Product", "Details");
            DropColumn("dbo.Product", "Videos");
            DropColumn("dbo.Image", "IsSlider");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Image", "IsSlider", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Videos", c => c.String(maxLength: 300, unicode: false));
            AddColumn("dbo.Product", "Details", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Product", "Characteristics", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Product", "Cover", c => c.String());
            AddColumn("dbo.Blog", "Cover", c => c.String());
            DropForeignKey("dbo.Product", "ImageId", "dbo.Image");
            DropForeignKey("dbo.Blog", "ImageId", "dbo.Image");
            DropIndex("dbo.Product", new[] { "ImageId" });
            DropIndex("dbo.Blog", new[] { "ImageId" });
            DropColumn("dbo.Product", "Specifications");
            DropColumn("dbo.Product", "Contents");
            DropColumn("dbo.Product", "ImageId");
            DropColumn("dbo.Product", "Services");
            DropColumn("dbo.Blog", "ImageId");
            RenameTable(name: "dbo.Product_Images", newName: "Product_Image");
        }
    }
}
