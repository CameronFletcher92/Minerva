namespace MinervaService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hierarchy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "ParentId", c => c.Long());
            CreateIndex("dbo.Equipments", "ParentId");
            AddForeignKey("dbo.Equipments", "ParentId", "dbo.Equipments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "ParentId", "dbo.Equipments");
            DropIndex("dbo.Equipments", new[] { "ParentId" });
            DropColumn("dbo.Equipments", "ParentId");
        }
    }
}
