namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserId1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Review", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.Review", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Review", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Review", "Phone", c => c.String());
            AlterColumn("dbo.Review", "Email", c => c.String());
            AlterColumn("dbo.Review", "UserName", c => c.String());
        }
    }
}
