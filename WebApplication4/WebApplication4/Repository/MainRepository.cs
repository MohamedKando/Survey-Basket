using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication4.Data;
using WebApplication4.Repository.Base;

namespace WebApplication4.Repository
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        public MainRepository(AppDbContext context)
        {
            this.context = context;
        }
        protected AppDbContext context { get; set; }
        public T GetById(int id)
        {

            return context.Set<T>().Find(id);
        }
        public T SelectOne(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().SingleOrDefault(match);
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

         public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        IEnumerable<T> IRepository<T>.GetAll(params string[] eagrs)
        {
            IQueryable<T> query = context.Set<T>();
            if (eagrs.Length>0)
            {
                foreach (var e in eagrs)
                {
                    query = query.Include(e);
                }    
            }
            return query.ToList();
        }

        async Task<IEnumerable<T>> IRepository<T>.GetAllAsync(params string[] eagrs)
        {
            IQueryable<T> query = context.Set<T>();
            if (eagrs.Length > 0)
            {
                foreach (var e in eagrs)
                {
                    query = query.Include(e);
                }
            }
            return await query.ToListAsync();
        }

        void IRepository<T>.AddOne(T myItem)
        {
            context.Set<T>().Add(myItem);
            context.SaveChanges();
        }

        void IRepository<T>.AddMany(IEnumerable<T> myItems)
        {
            context.Set<T>().AddRange(myItems);
            context.SaveChanges();
        }

        void IRepository<T>.UpdateOne(T myItem)
        {
            context.Set<T>().Update(myItem);
            context.SaveChanges();
        }

        void IRepository<T>.UpdateMany(IEnumerable<T> myItems)
        {
            context.Set<T>().UpdateRange    (myItems);
            context.SaveChanges();
        }

        void IRepository<T>.DeleteOne(T myItem)
        {
            context.Set<T>().Remove(myItem);
            context.SaveChanges();
        }

        void IRepository<T>.DeleteMany(IEnumerable<T> myItems)
        {
            context.Set<T>().RemoveRange (myItems);
            context.SaveChanges ();
        }
    }
}

