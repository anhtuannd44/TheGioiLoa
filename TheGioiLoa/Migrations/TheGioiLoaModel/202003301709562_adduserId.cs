namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Review", "UserId");
        }
    }
}
