namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datetimenullnot : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "DateModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "DateModified", c => c.DateTime(nullable: false));
        }
    }
}
