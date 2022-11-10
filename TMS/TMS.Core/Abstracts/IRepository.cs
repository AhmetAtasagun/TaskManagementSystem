using System.Linq.Expressions;

namespace TMS.Core.Abstracts
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> expression);
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> expression = null, bool noTracking = false);
        List<T> GetList(Expression<Func<T, bool>> expression = null, bool noTracking = false);
        T Add(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
