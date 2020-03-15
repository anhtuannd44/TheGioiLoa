namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewMoreCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "StarCount", c => c.Int(nullable: false));
            AddColumn("dbo.Review", "UserName", c => c.String());
            AddColumn("dbo.Review", "Email", c => c.String());
            AddColumn("dbo.Review", "Phone", c => c.String());
            DropColumn("dbo.Review", "StartCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Review", "StartCount", c => c.Int());
            DropColumn("dbo.Review", "Phone");
            DropColumn("dbo.Review", "Email");
            DropColumn("dbo.Review", "UserName");
            DropColumn("dbo.Review", "StarCount");
        }
    }
}
