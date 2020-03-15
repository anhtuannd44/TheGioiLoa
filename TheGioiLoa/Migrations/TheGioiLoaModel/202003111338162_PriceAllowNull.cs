namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceAllowNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Price", c => c.Double());
            AlterColumn("dbo.Product", "PriceSale", c => c.Double());
            AlterColumn("dbo.OrderDetails", "Price", c => c.Double());
            AlterColumn("dbo.OrderDetails", "SalePrice", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "SalePrice", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderDetails", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Product", "PriceSale", c => c.Double(nullable: false));
            AlterColumn("dbo.Product", "Price", c => c.Double(nullable: false));
        }
    }
}
