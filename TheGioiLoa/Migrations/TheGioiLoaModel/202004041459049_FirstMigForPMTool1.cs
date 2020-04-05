namespace TheGioiLoa.Migrations.TheGioiLoaModel
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigForPMTool1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Promotion", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Promotion", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
