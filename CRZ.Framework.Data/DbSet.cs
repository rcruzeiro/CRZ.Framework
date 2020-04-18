using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace CRZ.Framework.Data
{
    public class DbSet<TEntity>
        where TEntity : class
    {
        private readonly DbContext _context;

        protected internal DbSet(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync(CommandDefinition command)
        {
            return await _context.Connection.QueryAsync<TEntity>(command);
        }

        public virtual async Task<TEntity> QueryFirstAsync(CommandDefinition command)
        {
            return await _context.Connection.QueryFirstAsync<TEntity>(command);
        }

        public virtual void Execute(CommandDefinition command)
        {
            _context._commands.Add(command);
        }
    }
}
