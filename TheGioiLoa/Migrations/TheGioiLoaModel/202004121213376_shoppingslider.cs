namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shoppingslider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slider", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Slider", "Type");
        }
    }
}
