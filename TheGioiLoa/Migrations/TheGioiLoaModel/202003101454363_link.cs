namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class link : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "OrderDetails_OrderDetailId", c => c.Int());
            CreateIndex("dbo.Product", "OrderDetails_OrderDetailId");
            AddForeignKey("dbo.Product", "OrderDetails_OrderDetailId", "dbo.OrderDetails", "OrderDetailId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "OrderDetails_OrderDetailId", "dbo.OrderDetails");
            DropIndex("dbo.Product", new[] { "OrderDetails_OrderDetailId" });
            DropColumn("dbo.Product", "OrderDetails_OrderDetailId");
        }
    }
}
