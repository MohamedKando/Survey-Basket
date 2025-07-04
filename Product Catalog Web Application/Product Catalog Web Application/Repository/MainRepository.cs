using Product_Catalog_Web_Application.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Product_Catalog_Web_Application.Data;
namespace Product_Catalog_Web_Application.Repository
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        public MainRepository(AppDbContext context)
        {
            this.context = context;
        }
        protected AppDbContext context { get; set; }
        void IRepository<T>.AddOne(T myItem)
        {
            context.Set<T>().Add(myItem);
            context.SaveChanges();
        }

        async Task  IRepository<T>. AddOneAsync(T myItem)
        {
           await context.Set<T>().AddAsync(myItem);
           await context.SaveChangesAsync();
        }

        void IRepository<T>.DeleteOne(T myItem)
        {
            context.Set<T>().Remove(myItem);
            context.SaveChanges();
        }

        async Task IRepository<T>.DeleteOneAsync(T myItem)
        {
            context.Set<T>().Remove(myItem);
            await context.SaveChangesAsync();
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
          return  context.Set<T>().ToList();
        }

        IEnumerable<T> IRepository<T>.GetAll(params string[] eagrs)
        {
            IQueryable<T> query = context.Set<T>();
            if (eagrs.Length > 0)
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

     

        T IRepository<T>.GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        async Task<T> IRepository<T>.GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        T IRepository<T>.SelectOne(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().SingleOrDefault(match);
        }

        void IRepository<T>.UpdateOne(T myItem)
        {
            context.Set<T>().Update(myItem);
            context.SaveChanges();
        }

        async Task IRepository<T>.UpdateOneAsync(T myItem)
        {
            context.Set<T>().Update(myItem);
            await context.SaveChangesAsync();
        }
    }
}
