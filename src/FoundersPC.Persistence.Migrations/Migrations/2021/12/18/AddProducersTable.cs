using FluentMigrator;
using FoundersPC.Persistence.Migrations.Common;
using FoundersPC.Persistence.Migrations.Extensions;

namespace FoundersPC.Persistence.Migrations.Migrations._2021._12._18;

[FoundersPCMigration(2021_12_18_2, "Add Producers Table")]
public class AddProducersTable : Migration
{
    public override void Up()
    {
        Create.Table("Producers")
              .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey("PK_Producers")
              .WithFullAuditableColumns("Producers")
              .WithColumn("ShortName").AsString().Nullable()
              .WithColumn("FullName").AsString().NotNullable()
              .WithColumn("CountryId").AsInt32().Nullable().ForeignKey("FK_Producers_Metadata_CountryId", "Metadata", "Id")
              .WithColumn("Website").AsString().Nullable()
              .WithColumn("FoundationDate").AsDateTime().Nullable();
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Producers_Metadata_CountryId")
              .OnTable("Producers");

        Delete.Table("Producers");
    }
}