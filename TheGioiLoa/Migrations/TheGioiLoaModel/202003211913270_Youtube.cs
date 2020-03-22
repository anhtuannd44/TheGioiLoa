namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Youtube : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductHomePage", "YoutubeLink", c => c.String());
            DropColumn("dbo.ProductHomePage", "YoutubLink");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductHomePage", "YoutubLink", c => c.String());
            DropColumn("dbo.ProductHomePage", "YoutubeLink");
        }
    }
}
