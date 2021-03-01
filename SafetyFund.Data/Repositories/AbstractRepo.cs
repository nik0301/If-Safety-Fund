using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SafetyFund.Data.Repositories
{
    public abstract class AbstractRepo<TEntity, TIdentity> where TEntity : class
    {
        protected DbContextOptions Options { get; }


        protected AbstractRepo(DbContextOptions options)
        {
            Options = options;
        }


        public virtual TEntity Get(TIdentity id)
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                return db.Set<TEntity>().Find(id);
            }
        }


        public virtual IList<TEntity> Get()
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                return db.Set<TEntity>().ToList();
            }
        }


        public virtual void Add(TEntity entity)
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                db.Set<TEntity>().Add(entity);
                db.SaveChanges();
            }
        }


        public virtual void Delete(TEntity entity)
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                db.Set<TEntity>().Remove(entity);
                db.SaveChanges();
            }
        }


        public virtual void Update(TEntity entity)
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                db.Set<TEntity>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
