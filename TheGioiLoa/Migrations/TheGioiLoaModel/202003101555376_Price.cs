namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Price : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "Price", c => c.Double());
        }
    }
}
