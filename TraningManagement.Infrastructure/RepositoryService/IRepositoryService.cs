using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TraningManagement.Infrastructure.RepositoryService
{
    public interface IRepositoryService<T>
        where T : class
    {
        Task<List<T>> GetAllAsync(int topCount = 0);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0, params string[] includeProperties);

        Task<List<T>> GetLatestAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0, Expression<Func<T, DateTime>> sortExp = null);
        Task<List<T>> GetLatestWithLockAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0, Expression<Func<T, DateTime>> sortExp = null);

        Task<T> FindAsync<TId>(TId id);
        void Add(T entity);
        void AddAll(List<T> entities);
        void Update(T entity);
        void UpdateAll(List<T> entities);
        void UpdateWithCommit(T entity);
        Task<IEnumerable<T>> RemoveAsync(Expression<Func<T, bool>> whereCondition);
        Task<T> RemoveAsync<TKey>(TKey id);
        Task<int> GetCountAsync<TElement>(T entity, Expression<Func<T, ICollection<TElement>>> collectionProperty,
            Expression<Func<TElement, bool>> whereExpression = null)
            where TElement : class;
        T RemoveAsync(T entity);
        int GetCountAsync(Expression<Func<T, bool>> whereExpression);
        Task<int> BulkInsert(List<T> entity, int batchSize = 1);

        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> whereCondition, int topCount = 0, params string[] includeProperties);
    }
}
