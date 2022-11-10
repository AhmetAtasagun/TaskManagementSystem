using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TMS.Core.Abstracts;
using TMS.Data.Context;
using TMS.Models.Exceptions;

namespace TMS.Data
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly TMSDbContext _context;
        private DbSet<T> _table { get; }

        public Repository(TMSDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public T? Get(Expression<Func<T, bool>> expression) => _table.Where(expression).FirstOrDefault();

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> expression = null, bool noTracking = false)
        {
            if (expression == null)
            {
                if (noTracking)
                    return _table.AsNoTracking().AsQueryable();
                else return _table.AsQueryable();
            }
            else
            {
                if (noTracking)
                    return _table.AsNoTracking().Where(expression);
                else return _table.Where(expression);
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> expression = null, bool noTracking = false)
        {
            if (expression == null)
            {
                if (noTracking)
                    return _table.AsNoTracking().ToList();
                else return _table.ToList();
            }
            else
            {
                if (noTracking)
                    return _table.AsNoTracking().Where(expression).ToList();
                else return _table.Where(expression).ToList();
            }
        }

        public T Add(T entity) => EntrySaveChanges(entity, EntityState.Added);

        public T Update(T entity) => EntrySaveChanges(entity, EntityState.Modified);

        public bool Delete(int id)
        {
            var entity = _table.Find(id);
            if (entity == null)
                throw new NotFoundException($"{_table.EntityType.DisplayName()} not found");
            return EntrySaveChanges(entity, EntityState.Deleted) != null;
        }

        private T EntrySaveChanges(T entity, EntityState state)
        {
            _context.Entry(entity).State = state;
            _context.SaveChanges();
            return entity;
        }

    }
}
