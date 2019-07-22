using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TraningManagement.Infrastructure.RepositoryService
{
    public interface ISqlServerEfRepositoryService<TEntity> : IRepositoryService<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAllQueryable();
        int GetMax(Expression<Func<TEntity, int>> predicateExpression);
        int GetMax(PropertyInfo propertyInfo);
        new int GetCountAsync(Expression<Func<TEntity, bool>> whereExpression);
        List<TEntity> GetByRawQuery(string sqlQueryString);
        Task GetDataFact(object[] filter);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
