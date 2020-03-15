namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blogInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blog", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blog", "Status", c => c.Boolean(nullable: false));
        }
    }
}
