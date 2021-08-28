using System;
using FluentMigrator;

namespace FoundersPC.Persistence.Migrations.Common
{
    public class FoundersPCMigrationAttribute : MigrationAttribute
    {
        public FoundersPCMigrationAttribute(int migrationNumber, string description) :
            base(GetVersion(migrationNumber), description) { }

        private static long GetVersion(int migrationNumber) => Int64.Parse(DateTime.UtcNow.ToString("ddMMyyyy") + migrationNumber);

        public FoundersPCMigrationAttribute(int migrationNumber,
                                            TransactionBehavior transactionBehavior = TransactionBehavior.Default,
                                            string description = null) : base(GetVersion(migrationNumber),
                                                                              transactionBehavior,
                                                                              description) { }
    }
}
