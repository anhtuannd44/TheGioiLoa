namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blogdescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "Description", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blog", "Description");
        }
    }
}
