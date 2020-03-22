namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class csdfhrfa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Information", "FooterCopyright", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Information", "FooterCopyright");
        }
    }
}
