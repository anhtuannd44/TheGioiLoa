namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageLinkToSlider : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Slider", "ImageId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Slider", "ImageId");
            AddForeignKey("dbo.Slider", "ImageId", "dbo.Image", "ImageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slider", "ImageId", "dbo.Image");
            DropIndex("dbo.Slider", new[] { "ImageId" });
            AlterColumn("dbo.Slider", "ImageId", c => c.Int(nullable: false));
        }
    }
}
