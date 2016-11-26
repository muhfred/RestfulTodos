namespace TodoWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredTitle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Todoes", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Todoes", "Title", c => c.String());
        }
    }
}
