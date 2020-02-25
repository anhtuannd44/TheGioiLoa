namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ahihModeladd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Cover", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Cover");
        }
    }
}
