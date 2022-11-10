using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TMS.Core.Abstracts
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        DbSet<T> Table { get; }
        T Get(Expression<Func<T, bool>> expression);
        List<T> GetList(Expression<Func<T, bool>> expression);
        T Add(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
