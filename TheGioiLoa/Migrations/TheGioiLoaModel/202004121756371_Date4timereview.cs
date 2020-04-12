namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Date4timereview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Review", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Review", "DateCreated");
            DropColumn("dbo.Review", "Status");
        }
    }
}
