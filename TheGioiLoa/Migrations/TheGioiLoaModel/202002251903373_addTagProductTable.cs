namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTagProductTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ImageId = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
            AddColumn("dbo.Product_Image", "Image_ImageId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Product_Image", "Image_ImageId");
            AddForeignKey("dbo.Product_Image", "Image_ImageId", "dbo.Image", "ImageId");
            DropColumn("dbo.Product_Image", "IsMain");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product_Image", "IsMain", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Product_Image", "Image_ImageId", "dbo.Image");
            DropIndex("dbo.Product_Image", new[] { "Image_ImageId" });
            DropColumn("dbo.Product_Image", "Image_ImageId");
            DropTable("dbo.Image");
        }
    }
}
