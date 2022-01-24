#region Using namespaces

using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

#endregion

namespace FoundersPC.Persistence.Migrations.Extensions;

internal static class CreateTableExtensions
{
    public static ICreateTableWithColumnSyntax AddAuditableColumns(this ICreateTableWithColumnSyntax builder, string tableName) =>
        builder.WithColumn("Created")
               .AsDateTime()
               .WithDefault(SystemMethods.CurrentDateTime)
               .WithColumn("CreatedById")
               .AsInt32()
               .ForeignKey($"FK_{tableName}_Users_CreatedById", "Users", "Id")
               .WithColumn("LastModified")
               .AsDateTime()
               .WithDefault(SystemMethods.CurrentDateTime)
               .WithColumn("LastModifiedById")
               .AsInt32()
               .ForeignKey($"FK_{tableName}_Users_LastModifiedById", "Users", "Id");

    public static ICreateTableWithColumnSyntax AddSoftDeleteColumns(this ICreateTableWithColumnSyntax builder, string tableName) =>
        builder
            .WithColumn("IsDeleted")
            .AsBoolean()
            .WithDefaultValue(false)
            .NotNullable()
            .WithColumn("Deleted")
            .AsDateTime()
            .Nullable()
            .WithColumn("DeletedById")
            .AsInt32()
            .ForeignKey($"FK_{tableName}_Users_DeletedById", "Users", "Id")
            .Nullable();

    public static ICreateTableWithColumnSyntax WithFullAuditableColumns(this ICreateTableWithColumnSyntax builder, string tableName) =>
        builder.AddAuditableColumns(tableName)
               .AddSoftDeleteColumns(tableName);
}