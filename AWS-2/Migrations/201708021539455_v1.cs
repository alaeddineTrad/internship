namespace AWS_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Interactions",
                c => new
                    {
                        TravlerId = c.Long(nullable: false),
                        RecieverId = c.Long(nullable: false),
                        date = c.DateTime(nullable: false, precision: 0),
                        text = c.String(unicode: false),
                        mark = c.Single(),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.TravlerId, t.RecieverId })
                .ForeignKey("dbo.Users", t => t.RecieverId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.TravlerId, cascadeDelete: true)
                .Index(t => t.TravlerId)
                .Index(t => t.RecieverId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        name = c.String(unicode: false),
                        phone = c.Int(nullable: false),
                        mail = c.String(unicode: false),
                        login = c.String(unicode: false),
                        password = c.String(unicode: false),
                        delevery_address = c.String(unicode: false),
                        address = c.String(unicode: false),
                        departure_date = c.String(unicode: false),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        size = c.Int(nullable: false),
                        category = c.Int(nullable: false),
                        name = c.String(unicode: false),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interactions", "TravlerId", "dbo.Users");
            DropForeignKey("dbo.Interactions", "RecieverId", "dbo.Users");
            DropForeignKey("dbo.Items", "User_Id", "dbo.Users");
            DropIndex("dbo.Items", new[] { "User_Id" });
            DropIndex("dbo.Interactions", new[] { "RecieverId" });
            DropIndex("dbo.Interactions", new[] { "TravlerId" });
            DropTable("dbo.Items");
            DropTable("dbo.Users");
            DropTable("dbo.Interactions");
        }
    }
}
