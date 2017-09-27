namespace AWS_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Interaction",
                c => new
                    {
                        InteractionId = c.Long(nullable: false, identity: true),
                        date = c.DateTime(nullable: false, precision: 0),
                        text = c.String(unicode: false),
                        mark = c.Single(),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Reciever_UserId = c.Long(),
                        Travler_UserId = c.Long(),
                    })
                .PrimaryKey(t => t.InteractionId)
                .ForeignKey("dbo.User", t => t.Reciever_UserId)
                .ForeignKey("dbo.User", t => t.Travler_UserId)
                .Index(t => t.Reciever_UserId)
                .Index(t => t.Travler_UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Password = c.String(unicode: false),
                        phone = c.Int(nullable: false),
                        JoinDate = c.DateTime(nullable: false, precision: 0),
                        delevery_address = c.String(unicode: false),
                        address = c.String(unicode: false),
                        departure_date = c.String(unicode: false),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        size = c.Int(nullable: false),
                        category = c.Int(nullable: false),
                        name = c.String(unicode: false),
                        User_UserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        LoginProvider = c.String(unicode: false),
                        ProviderKey = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IdentityRole_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.IdentityRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Interaction", "Travler_UserId", "dbo.User");
            DropForeignKey("dbo.Interaction", "Reciever_UserId", "dbo.User");
            DropForeignKey("dbo.Item", "User_UserId", "dbo.User");
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Item", new[] { "User_UserId" });
            DropIndex("dbo.Interaction", new[] { "Travler_UserId" });
            DropIndex("dbo.Interaction", new[] { "Reciever_UserId" });
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.Item");
            DropTable("dbo.User");
            DropTable("dbo.Interaction");
        }
    }
}
