using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.BusinessServices;

namespace TrainingManagement.Application.ApplicationServices
{
    public abstract class ApplicationService<T> : IApplicationService<T>
        where T : class
    {
        private readonly IBusinessService<T> _businessService;

        protected ApplicationService(IBusinessService<T> businessService)
        {
            _businessService = businessService;
        }

        public virtual async Task<List<T>> GetAllAsync(int topCount = 0)
        {
            return await _businessService.GetAllAsync(topCount);
        }

        public virtual async Task<T> GetAsync<TKey>(TKey id)
        {
            return await _businessService.GetAsync(id);
        }

        public virtual async Task<List<T>> GetLatestAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0, Expression<Func<T, DateTime>> sortExp = null)
        {
            return await _businessService.GetLatestAsync(whereCondition, topCount, sortExp);
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0)
        {
            return await _businessService.GetAsync(whereCondition, topCount);
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> whereCondition, int topCount = 0, params string[] includeProperties)
        {
            return await _businessService.GetAsync(whereCondition, topCount, includeProperties);
        }

        public async Task<int> GetCountAsync<TElement>(T entity,
            Expression<Func<T, ICollection<TElement>>> collectionProperty) where TElement : class
        {
            return await _businessService.GetCountAsync(entity, collectionProperty);
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            return await _businessService.AddAsync(entity);
        }
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            return await _businessService.UpdateAsync(entity);
        }

        public virtual async Task<bool> DeleteAsync<TId>(TId id)
        {
            return await _businessService.RemoveAsync(id);
        }
    }
}
