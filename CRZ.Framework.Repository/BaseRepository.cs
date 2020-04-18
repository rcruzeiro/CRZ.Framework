using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CRZ.Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRZ.Framework.Repository
{
    public abstract class BaseRepository<T> : IRepositoryAsync<T>
        where T : class, IAggregationRoot
    {
        readonly IUnitOfWorkAsync _unitOfWork;

        protected DbContext Context => _unitOfWork.Context;

        protected BaseRepository(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public virtual IEnumerable<T> Get(Func<T, bool> predicate = null)
        {
            return _unitOfWork.Get(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate = null, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.GetAsync(predicate, cancellationToken);
        }

        public virtual IEnumerable<T> Get(ISpecification<T> specification)
        {
            return _unitOfWork.Get(specification);
        }

        public virtual async Task<IEnumerable<T>> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.GetAsync(specification, cancellationToken);
        }

        public virtual T GetOne(Func<T, bool> predicate = null)
        {
            return _unitOfWork.GetOne(predicate);
        }

        public virtual T GetOne(ISpecification<T> specification)
        {
            return _unitOfWork.GetOne(specification);
        }

        public virtual async Task<T> GetOneAsync(Func<T, bool> predicate = null, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.GetOneAsync(predicate, cancellationToken);
        }

        public virtual async Task<T> GetOneAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.GetOneAsync(specification, cancellationToken);
        }

        public virtual T Find(params object[] keyValues)
        {
            return _unitOfWork.Find<T>(keyValues);
        }

        public virtual async Task<T> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.FindAsync<T>(keyValues, cancellationToken);
        }

        public virtual T Add(T entity)
        {
            return _unitOfWork.Add(entity);
        }

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.AddAsync(entity, cancellationToken);
        }

        public virtual void Add(IEnumerable<T> entities)
        {
            _unitOfWork.Add(entities);
        }

        public virtual async Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.AddAsync(entities, cancellationToken);
        }

        public virtual T Update(T entity)
        {
            return _unitOfWork.Update(entity);
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            _unitOfWork.Update(entities);
        }

        public virtual void Remove(T entity)
        {
            _unitOfWork.Remove(entity);
        }

        public virtual void Remove(IEnumerable<T> entities)
        {
            _unitOfWork.Remove(entities);
        }

        public virtual int SaveChanges()
        {
            return _unitOfWork.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _unitOfWork.Dispose();
        }
    }
}
