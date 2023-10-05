using FluentMigrator;

namespace SFC.Notifications.Infrastructure.DbMigrations
{
  [Migration(202310031622)]
  public class M2_AddEmailFrom : Migration
  {
    public override void Up()
    {
      Alter.Table("Notifications")
        .InSchema("Notifications")
        .AddColumn("EmailFrom").AsString();
    }

    public override void Down()
    {
      Delete.Column("EmailFrom").FromTable("Notifications").InSchema("Notifications");
    }
  }
}
