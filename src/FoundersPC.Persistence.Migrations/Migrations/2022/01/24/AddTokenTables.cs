using FluentMigrator;
using FluentMigrator.SqlServer;
using FoundersPC.Persistence.Migrations.Common;

namespace FoundersPC.Persistence.Migrations.Migrations._2022._01._24;

[FoundersPCMigration(2022_01_24_1, "Add Token tables")]
public class AddTokenTables : Migration
{
    public override void Up()
    {
        Create.Table("AccessTokens")
              .WithColumn("Id").AsInt32().NotNullable().PrimaryKey("PK_AccessTokens_Id").Identity(1, 1)
              .WithColumn("Token").AsString(64).NotNullable()
              .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("FK_AccessTokens_Users", "Users", "Id")
              .WithColumn("Type").AsInt32().NotNullable()
              .WithColumn("StartEvaluationDate").AsDateTime().NotNullable()
              .WithColumn("ExpirationDate").AsDateTime().NotNullable()
              .WithColumn("IsBlocked").AsBoolean().NotNullable().WithDefaultValue(false);

        Create.Table("AccessTokensHistory")
              .WithColumn("Id").AsInt32().NotNullable().PrimaryKey("PK_AccessTokensHistory_Id").Identity(1, 1)
              .WithColumn("AccessTokenId").AsInt32().NotNullable().ForeignKey("FK_AccessTokensHistory_AccessTokens", "AccessTokens", "Id")
              .WithColumn("RequestUserId").AsInt32().NotNullable().ForeignKey("FK_AccessTokensHistory_Users", "Users", "Id")
              .WithColumn("RequestDate").AsDateTime().NotNullable();

        Create.Index("IX_AccessTokensHistory_AccessTokenId")
              .OnTable("AccessTokensHistory")
              .OnColumn("AccessTokenId")
              .Ascending()
              .WithOptions()
              .NonClustered();

        Create.Index("IX_AccessTokensHistory_RequestUserId_RequestDate")
              .OnTable("AccessTokensHistory")
              .OnColumn("RequestUserId")
              .Ascending()
              .WithOptions()
              .NonClustered()
              .Include("RequestDate");
    }

    public override void Down() { }
}