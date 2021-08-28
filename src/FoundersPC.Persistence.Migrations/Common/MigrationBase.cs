using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentMigrator;
using Microsoft.Data.SqlClient;
using RepoDb;

namespace FoundersPC.Persistence.Migrations.Common
{
    public abstract class MigrationBase : Migration
    {
        protected void CreateDatabaseIfNotExists()
        {
            using var connection = new SqlConnection(ConnectionString);

            var queryString = @$"IF NOT EXISTS (
                                   SELECT name
                                   FROM sys.databases
                                   WHERE name = N'{connection.Database}'
                                )
                                CREATE DATABASE [{connection.Database}]";

            connection.ExecuteNonQuery(queryString);
        }

        protected void DropDatabaseIfExists()
        {
            using var connection = new SqlConnection(ConnectionString);

            var queryString = @$"IF EXISTS (
                                   SELECT name
                                   FROM sys.databases
                                   WHERE name = N'{connection.Database}'
                                )
                                DROP DATABASE [{connection.Database}]";

            connection.ExecuteNonQuery(queryString);
        }

        protected bool Exists<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            using var connection = new SqlConnection(ConnectionString);

            return connection.Exists(criteria);
        }

        protected TEntity FindOne<TEntity>(string sql, object param) where TEntity : class
        {
            using var connection = new SqlConnection(ConnectionString);

            return connection.ExecuteQuery<TEntity>(sql, param)
                             .FirstOrDefault();
        }

        protected IEnumerable<TEntity> FindMany<TEntity>(string sql, object param) where TEntity : class
        {
            using var connection = new SqlConnection(ConnectionString);

            return connection.ExecuteQuery<TEntity>(sql, param);
        }
    }
}