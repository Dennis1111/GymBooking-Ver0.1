namespace GymBooking_Ver0._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttendingClasses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "GymClass_Id", "dbo.GymClasses");
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "GymClass_Id" });
            CreateTable(
                "dbo.ApplicationUserGymClasses",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        GymClass_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.GymClass_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.GymClasses", t => t.GymClass_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.GymClass_Id);
            
            DropColumn("dbo.AspNetUsers", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "GymClass_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "GymClass_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ApplicationUserGymClasses", "GymClass_Id", "dbo.GymClasses");
            DropForeignKey("dbo.ApplicationUserGymClasses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserGymClasses", new[] { "GymClass_Id" });
            DropIndex("dbo.ApplicationUserGymClasses", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserGymClasses");
            CreateIndex("dbo.AspNetUsers", "GymClass_Id");
            CreateIndex("dbo.AspNetUsers", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "GymClass_Id", "dbo.GymClasses", "Id");
            AddForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
