namespace CloudataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAnswersReports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAnswersReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionId = c.Int(nullable: false),
                        ReportUri = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
        }
    }
}
