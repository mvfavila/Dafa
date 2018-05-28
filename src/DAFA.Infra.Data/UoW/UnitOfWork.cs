using DAFA.Infra.Data.Interfaces;
using System;

namespace DAFA.Infra.Data.UoW
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : IDbContext, new()
    {
        private readonly IDbContext dbContext;
        private readonly IContextManager contextManager;
        private bool disposed;

        public UnitOfWork(IContextManager contextManager)
        {
            this.contextManager = contextManager;
            dbContext = contextManager.GetContext();
        }

        public void BeginTransaction()
        {
            disposed = false;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
