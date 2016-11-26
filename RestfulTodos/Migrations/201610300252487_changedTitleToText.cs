namespace TodoWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedTitleToText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todoes", "Text", c => c.String(nullable: false));
            DropColumn("dbo.Todoes", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Todoes", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Todoes", "Text");
        }
    }
}
