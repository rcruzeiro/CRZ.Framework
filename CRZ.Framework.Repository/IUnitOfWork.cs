using System;
using System.Collections.Generic;
using CRZ.Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRZ.Framework.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        IEnumerable<T> Get<T>(Func<T, bool> predicate = null)
            where T : class, IAggregationRoot;

        IEnumerable<T> Get<T>(ISpecification<T> specification)
            where T : class, IAggregationRoot;

        T GetOne<T>(Func<T, bool> predicate = null)
            where T : class, IAggregationRoot;

        T GetOne<T>(ISpecification<T> specification)
            where T : class, IAggregationRoot;

        T Find<T>(params object[] keyValues)
            where T : class, IAggregationRoot;

        T Add<T>(T entity)
            where T : class, IAggregationRoot;

        void Add<T>(IEnumerable<T> entities)
            where T : class, IAggregationRoot;

        T Update<T>(T entity)
            where T : class, IAggregationRoot;

        void Update<T>(IEnumerable<T> entities)
            where T : class, IAggregationRoot;

        void Remove<T>(T entity)
            where T : class, IAggregationRoot;

        void Remove<T>(IEnumerable<T> entities)
            where T : class, IAggregationRoot;

        int SaveChanges();

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}
