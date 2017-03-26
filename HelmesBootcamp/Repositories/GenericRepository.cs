using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HelmesBootcamp.Models;

namespace HelmesBootcamp.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        BookingContext context;

        DbSet<TEntity> dbSet;

        public GenericRepository(BookingContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> FindAllAsQueryable()
        {
            return this.dbSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> FindAllAsQueryable(Func<TEntity, bool> p)
        {
            return this.dbSet.Where(p).AsQueryable();
        }

        public virtual int Count()
        {
            return this.dbSet.Count();
        }

        public virtual int Count(Func<TEntity, bool> p)
        {
            return this.dbSet.Count(p);
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return this.dbSet.Find(keyValues);
        }

        public virtual TEntity FindById(object id)
        {
            return this.dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
            Save();
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            this.dbSet.AddRange(entities);
            Save();
        }

        public virtual void Update(TEntity entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.dbSet.Attach(entity);

            this.context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        protected void Save()
        {
            context.SaveChanges();
        }

        ~GenericRepository()
        {
            Save();
        }

        public virtual void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
            Save();
        }
    }
}