namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotNulluserIdOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "UserId", c => c.String(nullable: false));
        }
    }
}
