using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using TrainingManagement.Application.EntityModels;
using TraningManagement.Infrastructure.RepositoryService;

namespace TrainingManagement.Application.RepositoryService
{
    public class TrainingManagementUnitOfWork:ITrainingManagementUnitOfWork
    {
        private TrainingManagementDbContext _dbContext;
        private IDictionary<Type, object> _repositories;
        private IServiceScope _scope;
        bool _disposed;

        public TrainingManagementUnitOfWork(TrainingManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            _repositories = new Dictionary<Type, object>();
            _dbContext.Database.SetCommandTimeout(180);
        }

        public IRepositoryService<T> GetRepositoryService<T>() where T : class
        {
            return GetRepositoryServiceSqlServerEf<T>();
        }

        public object GetRepositoryService(Type entityType)
        {
            var getRepositoryServiceExpression = Expression.Lambda(
                Expression.Block(
                    Expression.Call(Expression.Constant(this),
                        GetType().GetMethod("GetRepositoryServiceSqlServerEf",
                                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                            .MakeGenericMethod(entityType)
                    ))
            );

            var getRepositoryInstanceInvoker = (Func<object>)getRepositoryServiceExpression.Compile();
            return getRepositoryInstanceInvoker.Invoke();
        }

        public ISqlServerEfRepositoryService<T> GetRepositoryServiceSqlServerEf<T>() where T : class
        {
            if (!_repositories.TryGetValue(typeof(T), out var repository))
            {
                repository = new TrainingManagementSqlServerEfRepositoryService<T>(_dbContext);
                _repositories.Add(typeof(T), repository);
            }

            return (ISqlServerEfRepositoryService<T>)repository;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        private void DetachAllTrackedEntities()
        {
            var entities = _dbContext.ChangeTracker.Entries().ToList();

            foreach (EntityEntry dbEntityEntry in entities)
            {
                if (dbEntityEntry.Entity != null)
                {
                    dbEntityEntry.State = EntityState.Detached;
                }
            }
        }

        public void AssignScope(IServiceScope scope)
        {
            if (_scope != null)
            {
                throw new InvalidOperationException("There is a scope already exist");
            }
            _scope = scope;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _repositories?.Clear();
        //        _repositories = null;
        //        _dbContext?.Dispose();
        //        _scope?.Dispose();
        //    }
        //}
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _repositories?.Clear();
                _repositories = null;
                if (_dbContext != null)
                {
                    DetachAllTrackedEntities();
                    _dbContext?.Dispose();
                    _dbContext = null;
                }

                _scope?.Dispose();
            }

            _disposed = true;
        }

        public ISqlServerEfRepositoryService<T> GetSqlServerEfRepositoryService<T>() where T : class
        {
            if (!_repositories.TryGetValue(typeof(T), out var repository))
            {
                repository = new TrainingManagementSqlServerEfRepositoryService<T>(_dbContext);
                _repositories.Add(typeof(T), repository);
            }
            return (ISqlServerEfRepositoryService<T>) repository;
        }
    }
}
