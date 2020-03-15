namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notauthor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blog", "Author", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blog", "Author", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
