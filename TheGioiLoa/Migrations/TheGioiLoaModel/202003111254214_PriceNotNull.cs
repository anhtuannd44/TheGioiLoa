namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Product", "ListedPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "ListedPrice", c => c.Double());
            AlterColumn("dbo.Product", "Price", c => c.Double());
        }
    }
}
