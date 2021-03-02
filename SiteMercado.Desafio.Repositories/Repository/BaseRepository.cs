using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SiteMercado.Desafio.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SiteMercado.Desafio.Entities.Models;
using SiteMercado.Desafio.Utils.Models;
using SiteMercado.Desafio.Utils.Services;

namespace SiteMercado.Desafio.Repositories.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbContext context;

        protected DbSet<T> dbSet;

        protected BaseRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public T Search(params object[] keyValues)
        {
            return context.Set<T>().Find(keyValues);
        }
        public T Add(T entity)
        {
            var data = context.Set<T>().Add(entity).Entity;
            this.SaveChanges();
            return data;
        }

        public T Update(T entity)
        {
            ////context.Entry(entity).State = EntityState.Modified;
            var data = context.Set<T>().Update(entity).Entity;
            this.SaveChanges();
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Delete(T entity)
        {
            T existing = context.Set<T>().Find(GetPrimaryKeyValue(entity));
            if (existing != null)
            {
                var data = context.Set<T>().Remove(existing).Entity;
                this.SaveChanges();
                return data;
            }
            else
                return existing;

        }

        public void Delete(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
            this.SaveChanges();
        }

        public void Delete(params T[] entities)
        {
            context.Set<T>().RemoveRange(entities);
            this.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public long TotalRows()
        {
            return context.Set<T>().LongCount();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IQueryable<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> predicate, ResultPagesModel pagination)
        {
            return context.Set<T>().Where(predicate).Skip(pagination.Actual.ToInt32()).Take(pagination.Offset.ToInt32());
        }

        public IQueryable<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> predicate, ResultPagesModel pagination, out long totalItems)
        {
            var query = context.Set<T>().Where(predicate);

            totalItems = query.LongCount();
            query = query.Skip(pagination.Actual.ToInt32()).Take(pagination.Offset.ToInt32());

            return query;
        }

        #region NotImplementedException
        public void Add(params T[] entities)
        {
            throw new NotImplementedException();
        }
        public void Add(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
        public IQueryable<T> Query(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }
        public T Single(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        #endregion

        public int SaveChanges()
        {
            OnBeforeSaving();
            return context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return (await context.SaveChangesAsync(true, cancellationToken));
        }

        private void OnBeforeSaving()
        {
            var modifiedEntities = context.ChangeTracker.Entries<AbstractModel>().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified || p.State == EntityState.Deleted).ToList();
            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKeyValue = GetPrimaryKeyValue(change.Entity);
                foreach (var prop in change.Entity.GetType().GetTypeInfo().DeclaredProperties)
                {
                    if (!prop.GetGetMethod().IsVirtual)
                    {
                        var currentValue = change.Property(prop.Name).CurrentValue;
                        var originalValue = change.Property(prop.Name).OriginalValue;
                        if (change.State == EntityState.Deleted || change.State == EntityState.Added || primaryKeyValue < 0 ||
                            (currentValue != null && originalValue != null && currentValue.ToString() != originalValue.ToString()) ||
                            ((currentValue != null && originalValue == null) || (currentValue == null && originalValue != null)))
                        {
                            ////var changeLoged = new LogSistema
                            ////{
                            ////    Propriedade = prop.Name,
                            ////    Entidade = entityName,
                            ////    EntityState = change.State,
                            ////    ChavePrimaria = primaryKeyValue,
                            ////    ValorAnterior = originalValue != null ? originalValue.ToString() : string.Empty,
                            ////    NovoValor = currentValue != null ? currentValue.ToString() : string.Empty,
                            ////    UsuarioId = Entities.Helpers.Gerenciamento.AppContext.Current != null ? (SessionHelper.Usuario.Id > 0 ? (long?)SessionHelper.Usuario.Id : null) : null
                            ////};
                            ////context.Set<LogSistema>().Add(changeLoged);
                        }
                        if (change.State == EntityState.Deleted)
                        {
                            change.State = EntityState.Modified;
                            change.Entity.IsDeleted = true;
                        }
                    }
                }
            }
        }

        protected virtual long GetPrimaryKeyValue<E>(E entity)
        {
            string keyName = context.Model.FindEntityType(entity.GetType()).FindPrimaryKey().Properties.Select(x => x.Name).Single();
            long result = (long)entity.GetType().GetProperty(keyName).GetValue(entity, null);
            if (result < 0)
                return -1;

            return result;
        }

    }
}
