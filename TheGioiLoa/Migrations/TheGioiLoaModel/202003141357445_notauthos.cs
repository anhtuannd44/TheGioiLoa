namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notauthos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blog", "Type");
        }
    }
}
