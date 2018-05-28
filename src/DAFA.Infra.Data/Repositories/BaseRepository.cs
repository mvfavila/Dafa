using DAFA.Domain.Interfaces.Repository;
using DAFA.Infra.Data.Context;
using DAFA.Infra.Data.Interfaces;
using System;
using System.Data.Entity;

namespace DAFA.Infra.Data.Repositories
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class
        where TContext : IDbContext, new()
    {
        protected IDbSet<TEntity> DbSet;
        protected readonly IDbContext context;

        // TODO: contextManager must be initialized here
        private readonly ContextManager contextManager;

        public BaseRepository()
        {
            contextManager = new ContextManager();
            context = contextManager.GetContext();
            DbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Delete(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public virtual void Update(TEntity obj)
        {
            var entry = context.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
