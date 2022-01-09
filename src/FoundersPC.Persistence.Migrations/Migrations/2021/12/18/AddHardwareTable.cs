using System.Data;
using FluentMigrator;
using FluentMigrator.SqlServer;
using FoundersPC.Persistence.Migrations.Common;
using FoundersPC.Persistence.Migrations.Extensions;

namespace FoundersPC.Persistence.Migrations.Migrations._2021._12._18;

[FoundersPCMigration(2021_12_18_1, "Add Hardware Table")]
public class AddHardwareTable : Migration
{
    public override void Up()
    {
        Create.Table("HardwareType")
              .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey("PK_HardwareType")
              .WithColumn("Name").AsString().NotNullable();

        Insert.IntoTable("HardwareType")
              .WithIdentityInsert()
              .Row(new { Id = 1, Name = "Case" })
              .Row(new { Id = 2, Name = "Hard Drive Disk" })
              .Row(new { Id = 3, Name = "Motherboard" })
              .Row(new { Id = 4, Name = "Power Supply" })
              .Row(new { Id = 5, Name = "Processor" })
              .Row(new { Id = 6, Name = "Random Access Memory" })
              .Row(new { Id = 7, Name = "Solid State Drive" })
              .Row(new { Id = 8, Name = "Video Card" });

        Create.Table("Hardware")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey("PK_Hardware")
              .WithFullAuditableColumns("Hardware")
              .WithColumn("HardwareTypeId").AsInt32().NotNullable().ForeignKey("FK_Hardware_HardwareType_HardwareTypeId", "HardwareType", "Id").OnDeleteOrUpdate(Rule.Cascade);
    }

    public override void Down()
    {
        Delete.FromTable("Hardware")
              .AllRows();

        Delete.FromTable("HardwareType")
              .AllRows();
    }
}