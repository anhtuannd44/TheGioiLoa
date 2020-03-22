namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slider : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        SliderId = c.Int(nullable: false, identity: true),
                        ImageId = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.SliderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Slider");
        }
    }
}
