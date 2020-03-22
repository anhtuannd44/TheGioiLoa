namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Youtubeas102 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProductHomePage");
            AlterColumn("dbo.ProductHomePage", "Id", c => c.Int(nullable: false, identity: false));
            AddPrimaryKey("dbo.ProductHomePage", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProductHomePage");
            AlterColumn("dbo.ProductHomePage", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ProductHomePage", "Id");
        }
    }
}