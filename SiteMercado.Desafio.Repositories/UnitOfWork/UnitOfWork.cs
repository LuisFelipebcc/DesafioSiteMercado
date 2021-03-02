using Microsoft.EntityFrameworkCore;
using SiteMercado.Desafio.Repositories.Repository;

namespace SiteMercado.Desafio.Repositories.UnitOfWork
{
    public class UnitOfWork : BaseUnitOfWork
    {

        #region attributes

        private ProdutoRepository _produtoRepository;

        #endregion

        #region Repositories
        public ProdutoRepository ProdutoRepository => _produtoRepository ?? (_produtoRepository = new ProdutoRepository(_context));
        #endregion

        public UnitOfWork(DbContext context) : base(context)
        {
            _context = context;
            _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        }

        protected override void _ResetRepositories()
        {
            _produtoRepository = null;
        }

        /// <summary>
        /// Class distructor.
        /// </summary>
        ~UnitOfWork()
        {
            _Dispose(false);
        }
    }
}
