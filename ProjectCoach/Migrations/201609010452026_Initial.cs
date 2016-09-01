namespace ProjectCoach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campeonatoes",
                c => new
                    {
                        CampeonatoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        DFB = c.String(),
                    })
                .PrimaryKey(t => t.CampeonatoID);
            
            CreateTable(
                "dbo.Equipoes",
                c => new
                    {
                        EquipoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.EquipoID);
            
            CreateTable(
                "dbo.Partidoes",
                c => new
                    {
                        PartidoID = c.Int(nullable: false, identity: true),
                        Jornada = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Ubicacion = c.String(),
                        Equipo1ID = c.Int(),
                        Equipo2ID = c.Int(),
                        CampeonatoID = c.Int(),
                        Resultado1 = c.Int(nullable: false),
                        Resultado2 = c.Int(nullable: false),
                        Equipo_EquipoID = c.Int(),
                    })
                .PrimaryKey(t => t.PartidoID)
                .ForeignKey("dbo.Campeonatoes", t => t.CampeonatoID)
                .ForeignKey("dbo.Equipoes", t => t.Equipo1ID)
                .ForeignKey("dbo.Equipoes", t => t.Equipo2ID)
                .ForeignKey("dbo.Equipoes", t => t.Equipo_EquipoID)
                .Index(t => t.Equipo1ID)
                .Index(t => t.Equipo2ID)
                .Index(t => t.CampeonatoID)
                .Index(t => t.Equipo_EquipoID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EquipoID = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipoes", t => t.EquipoID)
                .Index(t => t.EquipoID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EquipoCampeonatoes",
                c => new
                    {
                        Equipo_EquipoID = c.Int(nullable: false),
                        Campeonato_CampeonatoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Equipo_EquipoID, t.Campeonato_CampeonatoID })
                .ForeignKey("dbo.Equipoes", t => t.Equipo_EquipoID, cascadeDelete: true)
                .ForeignKey("dbo.Campeonatoes", t => t.Campeonato_CampeonatoID, cascadeDelete: true)
                .Index(t => t.Equipo_EquipoID)
                .Index(t => t.Campeonato_CampeonatoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "EquipoID", "dbo.Equipoes");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Partidoes", "Equipo_EquipoID", "dbo.Equipoes");
            DropForeignKey("dbo.Partidoes", "Equipo2ID", "dbo.Equipoes");
            DropForeignKey("dbo.Partidoes", "Equipo1ID", "dbo.Equipoes");
            DropForeignKey("dbo.Partidoes", "CampeonatoID", "dbo.Campeonatoes");
            DropForeignKey("dbo.EquipoCampeonatoes", "Campeonato_CampeonatoID", "dbo.Campeonatoes");
            DropForeignKey("dbo.EquipoCampeonatoes", "Equipo_EquipoID", "dbo.Equipoes");
            DropIndex("dbo.EquipoCampeonatoes", new[] { "Campeonato_CampeonatoID" });
            DropIndex("dbo.EquipoCampeonatoes", new[] { "Equipo_EquipoID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "EquipoID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Partidoes", new[] { "Equipo_EquipoID" });
            DropIndex("dbo.Partidoes", new[] { "CampeonatoID" });
            DropIndex("dbo.Partidoes", new[] { "Equipo2ID" });
            DropIndex("dbo.Partidoes", new[] { "Equipo1ID" });
            DropTable("dbo.EquipoCampeonatoes");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Partidoes");
            DropTable("dbo.Equipoes");
            DropTable("dbo.Campeonatoes");
        }
    }
}
