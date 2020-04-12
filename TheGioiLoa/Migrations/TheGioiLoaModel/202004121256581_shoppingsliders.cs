namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shoppingsliders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Image", "Type", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Image", "Type");
        }
    }
}
