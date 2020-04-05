namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogDateModifine : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blog", "DateModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blog", "DateModified", c => c.DateTime(nullable: false));
        }
    }
}
