namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixorderMenu : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MenuTop", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MenuTop", "Order", c => c.String());
        }
    }
}
