namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producthomepageIDnull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductHomePage", "CategoryId", "dbo.Category");
            DropIndex("dbo.ProductHomePage", new[] { "CategoryId" });
            AlterColumn("dbo.ProductHomePage", "CategoryId", c => c.Int());
            CreateIndex("dbo.ProductHomePage", "CategoryId");
            AddForeignKey("dbo.ProductHomePage", "CategoryId", "dbo.Category", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductHomePage", "CategoryId", "dbo.Category");
            DropIndex("dbo.ProductHomePage", new[] { "CategoryId" });
            AlterColumn("dbo.ProductHomePage", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductHomePage", "CategoryId");
            AddForeignKey("dbo.ProductHomePage", "CategoryId", "dbo.Category", "CategoryId", cascadeDelete: true);
        }
    }
}
