namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceNotNullPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "PriceSale", c => c.Double(nullable: false));
            DropColumn("dbo.Product", "ListedPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "ListedPrice", c => c.Double(nullable: false));
            DropColumn("dbo.Product", "PriceSale");
        }
    }
}
