namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isslider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Image", "IsSlider", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Image", "IsSlider");
        }
    }
}
