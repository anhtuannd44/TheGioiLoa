namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeMenu : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MenuTop", newName: "Menu");
            AddColumn("dbo.Menu", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menu", "Type");
            RenameTable(name: "dbo.Menu", newName: "MenuTop");
        }
    }
}
