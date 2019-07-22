using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagement.Application.ApplicationServices
{
    public interface IApplicationService<T>
        where T : class
    {
        Task<List<T>> GetAllAsync(int topCount = 0);
        Task<T> GetAsync<TKey>(TKey id);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0);
        Task<List<T>> GetLatestAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0, Expression<Func<T, DateTime>> sortExp = null);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0, params string[] includeProperties);
        Task<int> GetCountAsync<TElement>(T entity, Expression<Func<T, ICollection<TElement>>> collectionProperty)
            where TElement : class;
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync<TId>(TId id);
    }
}
