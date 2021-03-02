using System;

namespace SiteMercado.Desafio.Repositories.Interfaces
{
    /// <summary>
    /// Interface to be implemented by every Unit of Work class.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Method that tries to commit all database operations withis the current transaction.
        /// </summary>
        void Commit();
    }

}
