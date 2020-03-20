namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topmenu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuTop",
                c => new
                    {
                        MenuTopId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        Url = c.String(),
                        Order = c.String(),
                    })
                .PrimaryKey(t => t.MenuTopId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MenuTop");
        }
    }
}
