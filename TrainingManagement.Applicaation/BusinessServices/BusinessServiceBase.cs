using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TraningManagement.Infrastructure.RepositoryService;

namespace TrainingManagement.Application.BusinessServices
{
    public abstract class BusinessServiceBase<T> : IBusinessService<T>
        where T : class
    {
        protected readonly Func<IUnitOfWork> RepositoriesFactory;

        protected BusinessServiceBase(Func<IUnitOfWork> repositories)
        {
            RepositoriesFactory = repositories;
        }

        public virtual async Task<List<T>> GetAllAsync(int topCount = 0)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<T>().GetAllAsync(topCount);
            }
        }

        public virtual async Task<T> GetAsync<TKey>(TKey id)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<T>().FindAsync(id);
            }
        }

        public virtual async Task<List<T>> GetLatestAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0, Expression<Func<T, DateTime>> sortExp = null)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                var result = await repositories.GetRepositoryServiceSqlServerEf<T>().GetLatestAsync(whereCondition, topCount, sortExp);
                return result;
            }
        }
        public virtual async Task<List<T>> GetLatestWithLockAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0, Expression<Func<T, DateTime>> sortExp = null)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                var result = await repositories.GetRepositoryServiceSqlServerEf<T>().GetLatestWithLockAsync(whereCondition, topCount, sortExp);

                return result;
            }

        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<T>().GetAsync(whereCondition, topCount);
            }
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0, params string[] includeProperties)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<T>().GetAsync(whereCondition, topCount, includeProperties);
            }
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                repositories.GetRepositoryServiceSqlServerEf<T>().Add(entity);
                return await repositories.GetRepositoryServiceSqlServerEf<T>().SaveChangesAsync() > 0;
            }

        }

        public async Task<bool> AddAllAsync(List<T> entities)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                repositories.GetRepositoryServiceSqlServerEf<T>().AddAll(entities);
                return await repositories.GetRepositoryServiceSqlServerEf<T>().SaveChangesAsync() > 0;
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                repositories.GetRepositoryServiceSqlServerEf<T>().Update(entity);
                return await repositories.GetRepositoryServiceSqlServerEf<T>().SaveChangesAsync() > 0;
            }
        }

        public virtual async Task<bool> UpdateAsyncWithCommit(T entity)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                repositories.GetRepositoryServiceSqlServerEf<T>().UpdateWithCommit(entity);
                return await repositories.GetRepositoryServiceSqlServerEf<T>().SaveChangesAsync() > 0;
            }
        }

        public virtual async Task<bool> RemoveAsync<TKey>(TKey id)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                await repositories.GetRepositoryServiceSqlServerEf<T>().RemoveAsync(id);
                return await repositories.GetRepositoryServiceSqlServerEf<T>().SaveChangesAsync() > 0;
            }
        }

        public virtual async Task<bool> RemoveAsync(Expression<Func<T, bool>> whereCondition)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                await repositories.GetRepositoryServiceSqlServerEf<T>().RemoveAsync(whereCondition);
                return await repositories.GetRepositoryServiceSqlServerEf<T>().SaveChangesAsync() > 0;
            }
        }

        public async Task<int> GetCountAsync<TElement>(T entity,
            Expression<Func<T, ICollection<TElement>>> collectionProperty)
            where TElement : class
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<T>().GetCountAsync(entity, collectionProperty);
            }
        }
    }
}
