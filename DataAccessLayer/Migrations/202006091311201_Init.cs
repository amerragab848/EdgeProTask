namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Age = c.Double(nullable: false),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        Address = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SSN = c.Int(nullable: false),
                        ManagerId = c.Int(),
                        AddedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ManagerId)
                .Index(t => t.ManagerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "ManagerId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeDatas", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            DropIndex("dbo.EmployeeDatas", new[] { "EmployeeId" });
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeDatas");
        }
    }
}
