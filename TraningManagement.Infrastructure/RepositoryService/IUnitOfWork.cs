using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TraningManagement.Infrastructure.RepositoryService
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryService<T> GetRepositoryService<T>() where T : class;
        //T GetRepositoryService<T, TModel>()
        //    where T : IRepositoryService<TModel>
        //    where TModel : class;
        object GetRepositoryService(Type entityType);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void AssignScope(IServiceScope scope);
        ISqlServerEfRepositoryService<T> GetRepositoryServiceSqlServerEf<T>() where T : class;
    }
}
