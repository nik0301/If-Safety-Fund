using IFLike.DAL.Context;
using IFLike.DAL.Interfaces;
using IFLike.Domain;
using Microsoft.EntityFrameworkCore;

namespace IFLike.DAL.Implementation
{
    public class RepositoryBase<TEntity, TIdentity> : IRepositoryBase<TEntity, TIdentity> where TEntity : EntityBaseId<TIdentity>
    {
        protected IFLikeContext Context;
        protected DbSet<TEntity> DbSet;

        public RepositoryBase(IFLikeContext iFLikeContext)
        {
            Context = iFLikeContext;
            DbSet = Context.Set<TEntity>();
        }

        public virtual TEntity GetById(TIdentity id)
        {
            return DbSet.Find(id);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            TEntity existingEntity = GetById(entity.Id);
            if (existingEntity != null)
            {
                Context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                DbSet.Attach(entity);
            }
        }
    }
}
