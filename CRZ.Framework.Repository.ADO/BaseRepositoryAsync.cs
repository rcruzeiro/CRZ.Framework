using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CRZ.Framework.Repository.ADO
{
    public abstract class BaseRepositoryAsync<T> : BaseRepository<T>
        where T : class
    {
        protected BaseRepositoryAsync(IDbConnection dbConnection)
            : base(dbConnection)
        { }

        public async Task<IEnumerable<T>> QueryAsync(string sql, object param = null, CancellationToken cancellationToken = default)
        {
            var result = await DbConnection.QueryAsync<T>(sql, param);

            return result;
        }

        public async Task<IEnumerable<T>> QueryAsync<TMap>(string sql, Func<T, TMap, T> map, object param = null, CancellationToken cancellationToken = default)
            where TMap : class
        {
            var result = await DbConnection.QueryAsync(sql, map, param);

            return result;
        }

        public async Task<T> QueryFirstAsync(string sql, object param = null, CancellationToken cancellationToken = default)
        {
            var result = await DbConnection.QueryFirstAsync<T>(sql, param);

            return result;
        }

        public async Task<T> QuerySingleAsync(string sql, object param = null, CancellationToken cancellationToken = default)
        {
            var result = await DbConnection.QuerySingleAsync<T>(sql, param);

            return result;
        }

        public async Task<int> ExecuteAsync(string sql, IEnumerable<T> param = null, CancellationToken cancellationToken = default)
        {
            var result = await DbConnection.ExecuteAsync(sql, param);

            return result;
        }

        public async Task<int> ExecuteAsync(string sql, T param = null)
        {
            return await ExecuteAsync(sql, new[] { param });
        }
    }
}
