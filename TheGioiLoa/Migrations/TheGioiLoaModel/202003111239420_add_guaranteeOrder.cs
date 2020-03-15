namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_guaranteeOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Guarantee", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "Guarantee");
        }
    }
}
