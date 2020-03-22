namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coverPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "Cover", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blog", "Cover");
        }
    }
}
