using InfrastructureLab4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfrastructureLab4.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext DbContext;
        protected DbSet<T> Entities => DbContext.Set<T>();

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public T First()
        {
            return Entities.First();
        }
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? Entities.FirstOrDefault(predicate) : First();
        }
        public void Add(T entity)
        {
            Entities.Add(entity);
        }
        public void Delete(T entity)
        {
            if (Entities.Contains(entity))
                Entities.Remove(entity);
        }
        public List<T> GetAll()
        {
            var entities = Entities.ToList();
            return entities;
        }
        public List<T> GetAll(Expression<Func<T, object>>[] includePredicats)
        {
            var ChangedEntityes = Entities.AsQueryable();
            foreach (var item in includePredicats)
            {
                ChangedEntityes = ChangedEntityes.Include(item);
            }
            return ChangedEntityes.ToList();
        }
        public List<T> Where(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? Entities.Where(predicate).ToList() : new List<T>();
        }
        public IQueryable<T> GetQuery(Expression<Func<T, object>>[] includePredicats)
        {
            var query = Entities.AsQueryable();
            foreach (var item in includePredicats)
            {
                query = query.Include(item);
            }
            return query;
        }

        public List<T> Where(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>>[] includePredicats)
        {
            var query = Entities.AsQueryable();
            foreach (var item in includePredicats)
            {
                query = query.Include(item);
            }
            return predicate != null ? query.Where(predicate).ToList() : new List<T>();
        }
    }
}
