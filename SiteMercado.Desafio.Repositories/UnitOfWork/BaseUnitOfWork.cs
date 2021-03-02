﻿using Microsoft.EntityFrameworkCore;
using SiteMercado.Desafio.Repositories.Interfaces;
using System;
using System.Data.SqlClient;

namespace SiteMercado.Desafio.Repositories.UnitOfWork
{
    /// <summary>
    /// Base Unit of Work class that defines all common methods.
    /// </summary>
    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        protected SqlConnection _connection;

        /// <summary>
        /// Property that flag that controls the object's distruction.
        /// </summary>
        protected bool _disposed;

        public DbContext _context;


        /// <summary>
        /// Class constructor
        /// </summary>
        public BaseUnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Internal Class distructor.
        /// </summary>
        protected void _Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Internal Abstract method that initializes all implementations repositories.
        /// </summary>
        protected abstract void _ResetRepositories();

        /// <summary>
        /// Class distructor.
        /// </summary>
        public void Dispose()
        {
            _Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// See <see cref="IUnitOfWork.Commit"/>.
        /// </summary>
        public void Commit()
        {
            try
            {
                _context.Database.CommitTransaction();
            }
            catch
            {
                _context.Database.RollbackTransaction();
                this.Dispose();
                throw;
            }
            finally
            {
                ////_context.Dispose();
                _ResetRepositories();
                _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            }
        }

        /// <summary>
        /// See <see cref="IUnitOfWork.Rollback"/>.
        /// </summary>
        public void Rollback()
        {
            try
            {
                _context.Database.RollbackTransaction();
            }
            catch
            {
                this.Dispose();
            }
            finally
            {
                _ResetRepositories();
                _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            }
        }

        /// <summary>
        /// Built-in distructor.
        /// </summary>
        ~BaseUnitOfWork()
        {
            _Dispose(false);
        }
    }
}
