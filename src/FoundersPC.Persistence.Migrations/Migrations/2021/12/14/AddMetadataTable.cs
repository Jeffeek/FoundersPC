using FluentMigrator;
using FluentMigrator.SqlServer;
using FoundersPC.Persistence.Migrations.Common;
using FoundersPC.Persistence.Migrations.Extensions;

namespace FoundersPC.Persistence.Migrations.Migrations._2021._12._14;

[FoundersPCMigration(2021_12_14_1, "Add Metadata Table")]
public class AddMetadataTable : Migration
{
    public override void Up()
    {
        Create.Table("Metadata")
              .WithColumn("Id").AsInt32().NotNullable().Identity(1, 1).PrimaryKey("PK_Metadata")
              .WithColumn("Name").AsString(512).NotNullable()
              .WithColumn("Type").AsInt32().NotNullable()
              .WithFullAuditableColumns("Metadata");
    }

    public override void Down()
    {
        Delete.Table("Metadata");
    }
}