using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace SiteMercado.Desafio.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> Query(string sql, params object[] parameters);

        T Search(params object[] keyValues);

        T Single(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);

        T Add(T entity);

        void Add(params T[] entities);

        void Add(IEnumerable<T> entities);

        T Update(T entity);

        T Delete(T entity);

        void Delete(object id);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
