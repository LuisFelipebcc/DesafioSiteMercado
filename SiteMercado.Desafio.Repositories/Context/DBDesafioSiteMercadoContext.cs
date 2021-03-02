using Microsoft.EntityFrameworkCore;
using SiteMercado.Desafio.Entities.Models;
using SiteMercado.Desafio.Utils.Services;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SiteMercado.Desafio.Repositories.Context
{
    public class DBDesafioSiteMercadoContext : DbContext
    {
        public DBDesafioSiteMercadoContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(Setting.ConnectionStringGerenciamento);
                //switch (Setting.Tipo)
                //{
                //    case Enumerators.ConexaoTipo.MySql:
                //        options.UseMySql(Setting.ConnectionString);
                //        break;
                //    case Enumerators.ConexaoTipo.SqlServer:
                //        options.UseSqlServer(Setting.ConnectionString);
                //        break;
                //    case Enumerators.ConexaoTipo.PostgreSql:
                //        options.UseNpgsql(Setting.ConnectionString);
                //        break;
                //    default:
                //        options.UseMySql(Setting.ConnectionString);
                //        break;
                //}
            }
        }

        public DbSet<Produto> Produto { get; set; }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        private void OnBeforeSaving()
        {
            var modifiedEntities = ChangeTracker.Entries<AbstractModel>().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified || p.State == EntityState.Deleted).ToList();
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
                            ////    UsuarioId = AppContext.Current != null ? (SessionHelper.Usuario.Id > 0 ? (long?)SessionHelper.Usuario.Id : null) : null
                            ////};
                            ////LogSistema.Add(changeLoged);
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

        protected virtual long GetPrimaryKeyValue<T>(T entity)
        {
            var keyName = Model.FindEntityType(entity.GetType()).FindPrimaryKey().Properties.Select(x => x.Name).Single();
            var result = (long)entity.GetType().GetProperty(keyName).GetValue(entity, null);
            if (result < 0)
                return -1;

            return result;
        }
    }
}
