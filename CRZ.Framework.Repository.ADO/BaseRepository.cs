using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace CRZ.Framework.Repository.ADO
{
    public abstract class BaseRepository<T> : IDisposable
        where T : class
    {
        protected IDbConnection DbConnection { get; }

        protected BaseRepository(IDbConnection dbConnection)
        {
            DbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public IEnumerable<T> Query(string sql, object param = null)
        {
            var result = DbConnection.Query<T>(sql, param);

            return result;
        }

        public IEnumerable<T> Query<TMap>(string sql, Func<T, TMap, T> map, object param = null)
            where TMap : class
        {
            var result = DbConnection.Query(sql, map, param);

            return result;
        }

        public T QueryFirst(string sql, object param = null)
        {
            var result = DbConnection.QueryFirst<T>(sql, param);

            return result;
        }

        public T QuerySingle(string sql, object param = null)
        {
            var result = DbConnection.QuerySingle<T>(sql, param);

            return result;
        }

        public int Execute(string sql, IEnumerable<T> param = null)
        {
            var result = DbConnection.Execute(sql, param);

            return result;
        }

        public int Execute(string sql, T param = null)
        {
            return Execute(sql, new[] { param });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbConnection != null)
                    DbConnection.Dispose();
            }
        }
    }
}
