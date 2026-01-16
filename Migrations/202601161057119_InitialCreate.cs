namespace SubsrciptionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DomainEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DomainName = c.String(),
                        Registrar = c.String(),
                        RegisteredDate = c.DateTime(nullable: false),
                        RenewalDate = c.DateTime(nullable: false),
                        NameServer1 = c.String(),
                        NameServer2 = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AutoRenew = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        Company = c.String(),
                        Password = c.String(),
                        StorageGB = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SoftwareEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SoftwareName = c.String(),
                        PlanType = c.String(),
                        Category = c.String(),
                        Email = c.String(),
                        SubscribedDate = c.DateTime(nullable: false),
                        RenewalDate = c.DateTime(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SoftwareEntities");
            DropTable("dbo.EmailEntities");
            DropTable("dbo.DomainEntities");
        }
    }
}
