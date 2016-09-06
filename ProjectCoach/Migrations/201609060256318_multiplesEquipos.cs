namespace ProjectCoach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class multiplesEquipos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "EquipoID", "dbo.Equipoes");
            DropIndex("dbo.AspNetUsers", new[] { "EquipoID" });
            AddColumn("dbo.Campeonatoes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Equipoes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Campeonatoes", "ApplicationUser_Id");
            CreateIndex("dbo.Equipoes", "ApplicationUser_Id");
            AddForeignKey("dbo.Campeonatoes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Equipoes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "EquipoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "EquipoID", c => c.Int());
            DropForeignKey("dbo.Equipoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Campeonatoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Equipoes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Campeonatoes", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Equipoes", "ApplicationUser_Id");
            DropColumn("dbo.Campeonatoes", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "EquipoID");
            AddForeignKey("dbo.AspNetUsers", "EquipoID", "dbo.Equipoes", "EquipoID");
        }
    }
}
