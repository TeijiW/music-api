using System;
using System.Linq;
using System.Linq.Expressions;

namespace musics_api.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();

        T GetById(Expression<Func<T, bool>> predicate);

        void Add(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}