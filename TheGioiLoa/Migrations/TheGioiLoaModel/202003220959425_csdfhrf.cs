namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class csdfhrf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Information", "FooterContact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Information", "FooterContact");
        }
    }
}
