namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleepage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "IsShowMenu", c => c.Boolean(nullable: false));
            DropTable("dbo.Page");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Page",
                c => new
                    {
                        PageId = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 300),
                        Name = c.String(nullable: false, maxLength: 100),
                        Content = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.PageId);
            
            DropColumn("dbo.Category", "IsShowMenu");
        }
    }
}
