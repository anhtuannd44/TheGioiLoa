namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigForPMToola : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "ProductId", "dbo.OrderDetails");
            DropForeignKey("dbo.CategoryProducts", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product_Image", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product_Tag", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Review", "ProductId", "dbo.Product");
            DropIndex("dbo.Product", new[] { "ProductId" });
            DropPrimaryKey("dbo.Product");
            AlterColumn("dbo.Product", "ProductId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Product", "ProductId");
            CreateIndex("dbo.OrderDetails", "ProductId");
            AddForeignKey("dbo.OrderDetails", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.CategoryProducts", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.Product_Image", "ProductId", "dbo.Product", "ProductId");
            AddForeignKey("dbo.Product_Tag", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.Review", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product_Tag", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product_Image", "ProductId", "dbo.Product");
            DropForeignKey("dbo.CategoryProducts", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Product");
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropPrimaryKey("dbo.Product");
            AlterColumn("dbo.Product", "ProductId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Product", "ProductId");
            CreateIndex("dbo.Product", "ProductId");
            AddForeignKey("dbo.Review", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.Product_Tag", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.Product_Image", "ProductId", "dbo.Product", "ProductId");
            AddForeignKey("dbo.CategoryProducts", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.Product", "ProductId", "dbo.OrderDetails", "OrderDetailId");
        }
    }
}
