#region Using namespaces

using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

#endregion

namespace FoundersPC.Persistence.Migrations.Extensions;

internal static class CreateTableExtensions
{
    public static ICreateTableWithColumnSyntax AddAuditableColumns(this ICreateTableWithColumnSyntax builder) =>
        builder.WithColumn("Created")
               .AsDateTime()
               .WithDefault(SystemMethods.CurrentDateTime)
               .WithColumn("CreatedById")
               .AsInt32()
               .ForeignKey("FK_Users_CreatedById", "Users", "Id")
               .WithColumn("LastModified")
               .AsDateTime()
               .WithDefault(SystemMethods.CurrentDateTime)
               .WithColumn("LastModifiedById")
               .AsInt32()
               .ForeignKey("FK_Users_LastModifiedById", "Users", "Id");

    public static ICreateTableWithColumnSyntax AddSoftDeleteColumns(this ICreateTableWithColumnSyntax builder) =>
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
            .ForeignKey("FK_Users_DeletedById", "Users", "Id")
            .Nullable();

    public static ICreateTableWithColumnSyntax WithIdentity(this ICreateTableWithColumnSyntax builder) =>
        builder.WithColumn("Id")
               .AsInt32()
               .PrimaryKey("PK_Id")
               .Identity()
               .Unique("IX_Id");

    public static ICreateTableWithColumnSyntax WithFullAuditableColumns(this ICreateTableWithColumnSyntax builder) =>
        builder.AddAuditableColumns()
               .AddSoftDeleteColumns();
}