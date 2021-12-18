using FluentMigrator;
using FoundersPC.Persistence.Migrations.Common;
using FoundersPC.Persistence.Migrations.Extensions;

namespace FoundersPC.Persistence.Migrations.Migrations._2021._12._14;

[FoundersPCMigration(2021_12_14_1, "Add Metadata Table")]
public class AddMetadataTable : Migration
{
    public override void Up()
    {
        Create.Table("Metadata")
              .WithFullAuditableColumns("Metadata")
              .WithIdentity("Metadata")
              .WithColumn("Name").AsString(512).NotNullable()
              .WithColumn("Type").AsInt32().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Metadata");
    }
}