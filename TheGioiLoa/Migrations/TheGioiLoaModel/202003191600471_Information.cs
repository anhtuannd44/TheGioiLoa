namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Information : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MenuTop", "Order");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MenuTop", "Order", c => c.Int(nullable: false));
        }
    }
}
