using FluentMigrator;
using FoundersPC.Persistence.Migrations.Common;

namespace FoundersPC.Persistence.Migrations.Migrations._2022._01._16;

[FoundersPCMigration(2022_01_16_1, "Add IsIntegrated to HardwareMetadata")]
public class AddIsIntegratedToHardwareMetadata : Migration
{
    public override void Up()
    {
        Alter.Table("HardwareMetadata")
             .AddColumn("IsIntegrated").AsBoolean().Nullable();
    }

    public override void Down()
    {
        Delete.Column("IsIntegrated")
              .FromTable("HardwareMetadata");
    }
}