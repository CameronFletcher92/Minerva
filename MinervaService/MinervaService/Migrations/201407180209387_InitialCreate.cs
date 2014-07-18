namespace MinervaService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DowntimeEvents",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        EquipmentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
                .Index(t => t.EquipmentId);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DowntimeEvents", "EquipmentId", "dbo.Equipments");
            DropIndex("dbo.DowntimeEvents", new[] { "EquipmentId" });
            DropTable("dbo.Equipments");
            DropTable("dbo.DowntimeEvents");
        }
    }
}
