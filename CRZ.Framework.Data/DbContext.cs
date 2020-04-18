using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace CRZ.Framework.Data
{
    public abstract class DbContext : IDisposable
    {
        private bool disposedValue = false;

        protected internal readonly List<CommandDefinition> _commands = new List<CommandDefinition>();

        protected internal IDbConnection Connection { get; private set; }

        protected internal IDbTransaction Transaction { get; private set; }

        protected DbContext(IDbConnection dbConnection)
        {
            Connection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));

            Open();
        }

        ~DbContext()
        {
            Dispose(false);
        }

        private void Open()
        {
            if (Connection != null && Connection.State == ConnectionState.Closed) Connection.Open();
        }

        public DbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            var instance = new DbSet<TEntity>(this);

            return instance;
        }

        public virtual void Begin()
        {
            if (Transaction == null)
            {
                Open();
                Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            }
        }

        public virtual int SaveChanges()
        {
            int total = 0;

            Open();

            _commands.ForEach(async command =>
            {
                total += await Connection.ExecuteAsync(command);
            });

            if (Transaction != null) Transaction.Commit();

            _commands.Clear();

            return total;
        }

        public virtual void Rollback()
        {
            if (Transaction != null) Transaction.Rollback();

            _commands.Clear();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Transaction != null) Transaction.Dispose();

                    if (Connection != null)
                    {
                        if (Connection.State != ConnectionState.Closed)
                            Connection.Close();

                        Connection.Dispose();
                    }
                }

                Transaction = null;
                Connection = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
