namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producthomepage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductHomePage",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CategoryId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                        YoutubLink = c.String(),
                        IsShow = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductHomePage", "CategoryId", "dbo.Category");
            DropIndex("dbo.ProductHomePage", new[] { "CategoryId" });
            DropTable("dbo.ProductHomePage");
        }
    }
}
