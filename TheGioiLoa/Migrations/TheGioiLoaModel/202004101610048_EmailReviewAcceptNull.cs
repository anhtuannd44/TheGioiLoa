namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailReviewAcceptNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Review", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Review", "Email", c => c.String(nullable: false));
        }
    }
}
