using System.Linq.Expressions;

namespace WebApplication4.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        T SelectOne(Expression<Func<T, bool>> match);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params string[] eagrs);


        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params string[] eagrs);

        void AddOne(T myItem);
        void AddMany(IEnumerable<T> myItems);
        void UpdateOne (T myItem);
        void UpdateMany(IEnumerable<T> myItems);
        void DeleteOne(T myItem); 
        void DeleteMany(IEnumerable<T> myItems);



    }
}
