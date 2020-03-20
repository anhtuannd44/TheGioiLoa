namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class social : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Information", "Facebook", c => c.String());
            AddColumn("dbo.Information", "Youtube", c => c.String());
            AddColumn("dbo.Information", "Zalo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Information", "Zalo");
            DropColumn("dbo.Information", "Youtube");
            DropColumn("dbo.Information", "Facebook");
        }
    }
}
