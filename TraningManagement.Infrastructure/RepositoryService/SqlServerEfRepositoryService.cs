using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TraningManagement.Infrastructure.RepositoryService
{
    public abstract class SqlServerEfRepositoryService<TEntity> : ISqlServerEfRepositoryService<TEntity>
        where TEntity : class
    {
        private readonly DbContext _context;

        protected SqlServerEfRepositoryService(DbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync(int topCount = 0)
        {
            try
            {
                if (topCount == 0)
                {
                    return await _context.Set<TEntity>().ToListAsync();
                }
                return await _context.Set<TEntity>()
                    .Take(topCount)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> whereCondition,
            int topCount = 0,
            params string[] includeProperties)
        {
            var entityDbSet = _context.Set<TEntity>()
                .Where(whereCondition);
            topCount = topCount == 0 ? 100000 : topCount;
            entityDbSet = entityDbSet.Take(topCount);
            entityDbSet = entityDbSet.Select(x => x);
            includeProperties?.ToList().ForEach(property => entityDbSet = entityDbSet.Include(property));

            return await entityDbSet.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetLatestAsync(Expression<Func<TEntity, bool>> whereCondition,
            int topCount = 0, Expression<Func<TEntity, DateTime>> sortExp = null)
        {
            var entityDbSet = _context.Set<TEntity>()
                .Where(whereCondition);
            if (sortExp != null) entityDbSet = entityDbSet.OrderByDescending(sortExp);
            topCount = topCount == 0 ? 100000 : topCount;
            entityDbSet = entityDbSet.Take(topCount);
            entityDbSet = entityDbSet.Select(x => x);

            return await entityDbSet.ToListAsync();
        }

        public Task<TEntity> FindAsync<TId>(TId id)
        {
            return _context.Set<TEntity>().FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddAll(List<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateAll(List<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
        }

        public void UpdateWithCommit(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            if (_context.Database.CurrentTransaction != null) _context.Database.CurrentTransaction.Commit();

        }

        public async Task<IEnumerable<TEntity>> RemoveAsync(Expression<Func<TEntity, bool>> whereCondition)
        {
            var itemsToBeRemoved = await _context.Set<TEntity>()
                .Where(whereCondition)
                .ToListAsync();

            _context.Set<TEntity>().RemoveRange(itemsToBeRemoved);
            return itemsToBeRemoved;
        }

        public async Task<TEntity> RemoveAsync<TKey>(TKey id)
        {
            var itemToBeRemoved = await _context.Set<TEntity>().FindAsync(id);

            _context.Set<TEntity>().Remove(itemToBeRemoved);
            return itemToBeRemoved;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int GetCountAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            var predicate = whereExpression.Compile();
            return _context.Set<TEntity>().Where(predicate).Count();
        }

        public IQueryable<TEntity> GetAllQueryable()
        {
            IQueryable<TEntity> entity = _context.Set<TEntity>();
            return entity;
        }

        public int GetMax(Expression<Func<TEntity, int>> propertyExpression)
        {
            if (_context.Set<TEntity>().Any())
                return _context.Set<TEntity>().Max(propertyExpression);
            else return 0;
        }

        public int GetMax(PropertyInfo propertyInfo)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            var body = Expression.Property(parameter, propertyInfo.Name);
            var lambda = Expression.Lambda<Func<TEntity, int>>(body, parameter);
            var result = _context.Set<TEntity>().Max(lambda);
            return result;
        }

        public TEntity RemoveAsync(TEntity entity)
        {
            var entityFromContext = _context.Set<TEntity>().Attach(entity);

            _context.Set<TEntity>().Remove(entity);
            return entityFromContext.Entity;
        }

        public List<TEntity> GetByRawQuery(string sqlQueryString)
        {
            List<TEntity> blogNames = _context.Query<TEntity>().FromSql(sqlQueryString).ToList();
            return blogNames;
        }

        public async Task<int> BulkInsert(List<TEntity> entities, int batchSize = 1)
        {
            if (!entities.Any()) return 0;
            var response = 0;
            var count = 0;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            //_context.ChangeTracker.ValidateOnSaveEnabled = false;
            var addedEntities = new List<TEntity>();
            foreach (var entity in entities)
            {
                count++;
                addedEntities.Add(entity);
                if (count >= batchSize)
                {
                    _context.Set<TEntity>().AddRange(addedEntities);
                    response = await _context.SaveChangesAsync();
                    addedEntities.Clear();
                    count = 0;
                }
            }
            if (count > 0)
            {
                _context.Set<TEntity>().AddRange(addedEntities);
                response = await _context.SaveChangesAsync();
            }
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
            //_context.Configuration.ValidateOnSaveEnabled = true;
            return response;
        }

        public Task<List<TEntity>> GetLatestWithLockAsync(Expression<Func<TEntity, bool>> whereCondition, int topCount = 0, Expression<Func<TEntity, DateTime>> sortExp = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> whereCondition, int topCount = 0, params string[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync<TElement>(TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> collectionProperty, Expression<Func<TElement, bool>> whereExpression = null) where TElement : class
        {
            throw new NotImplementedException();
        }

        public Task GetDataFact(object[] filter)
        {
            //Need to implement
            throw new NotImplementedException();
        }

    }
}
