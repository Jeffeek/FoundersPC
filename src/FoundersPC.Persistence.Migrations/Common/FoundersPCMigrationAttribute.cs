#region Using namespaces

using FluentMigrator;

#endregion

namespace FoundersPC.Persistence.Migrations.Common;

public class FoundersPCMigrationAttribute : MigrationAttribute
{
    public FoundersPCMigrationAttribute(int migrationNumber, string description) :
        base(migrationNumber, description) { }

    public FoundersPCMigrationAttribute(int migrationNumber,
                                        TransactionBehavior transactionBehavior = TransactionBehavior.Default,
                                        string description = null) : base(migrationNumber,
                                                                          transactionBehavior,
                                                                          description) { }
}