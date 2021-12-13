#region Using namespaces

using System;
using FluentMigrator;

#endregion

namespace FoundersPC.Persistence.Migrations.Common;

public class FoundersPCMigrationAttribute : MigrationAttribute
{
    public FoundersPCMigrationAttribute(int migrationNumber, string description) :
        base(GetVersion(migrationNumber), description) { }

    public FoundersPCMigrationAttribute(int migrationNumber,
                                        TransactionBehavior transactionBehavior = TransactionBehavior.Default,
                                        string description = null) : base(GetVersion(migrationNumber),
                                                                          transactionBehavior,
                                                                          description) { }

    private static long GetVersion(int migrationNumber) => Int64.Parse(DateTime.UtcNow.ToString("ddMMyyyy") + migrationNumber);
}