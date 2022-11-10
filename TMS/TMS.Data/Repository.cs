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
        public DbSet<T> Table { get; }

        public Repository(TMSDbContext context)
        {
            _context = context;
            Table = _context.Set<T>();
        }

        public T? Get(Expression<Func<T, bool>> expression) => Table.Where(expression).FirstOrDefault();

        public List<T> GetList(Expression<Func<T, bool>> expression) => Table.Where(expression).ToList();

        public T Add(T entity) => EntrySaveChanges(entity, EntityState.Added);

        public T Update(T entity) => EntrySaveChanges(entity, EntityState.Modified);

        public bool Delete(int id)
        {
            var entity = Table.Find(id);
            if (entity == null)
                throw new NotFoundException($"{Table.EntityType.DisplayName()} not found");
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
