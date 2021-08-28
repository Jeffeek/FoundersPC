using FluentMigrator;
using FluentMigrator.Builders.Alter.Table;

namespace FoundersPC.Persistence.Migrations.Extensions
{
    internal static class AlterTableExtensions
    {
        public static IAlterTableAddColumnOrAlterColumnSyntax AddAuditableColumns(this IAlterTableAddColumnOrAlterColumnSyntax builder) =>
            builder.AddColumn("Created").AsDateTime().WithDefault(SystemMethods.CurrentDateTime)
                   .AddColumn("CreatedById").AsInt32().ForeignKey("FK_Users_CreatedById", "Users", "Id")
                   .AddColumn("LastModified").AsDateTime().WithDefault(SystemMethods.CurrentDateTime)
                   .AddColumn("LastModifiedById").AsInt32().ForeignKey("FK_Users_LastModifiedById", "Users", "Id");

        public static IAlterTableAddColumnOrAlterColumnSyntax AddSoftDeleteColumns(this IAlterTableAddColumnOrAlterColumnSyntax builder) =>
            builder
                .AddColumn("IsDeleted").AsBoolean().WithDefaultValue(false).NotNullable()
                .AddColumn("Deleted").AsDateTime().Nullable()
                .AddColumn("DeletedById").AsInt32().ForeignKey("FK_Users_DeletedById", "Users", "Id").Nullable();

        public static IAlterTableAddColumnOrAlterColumnSyntax WithFullAuditableColumns(this IAlterTableAddColumnOrAlterColumnSyntax builder) =>
            builder.AddAuditableColumns()
                   .AddSoftDeleteColumns();
    }
}