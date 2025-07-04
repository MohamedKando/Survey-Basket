using System.Linq.Expressions;

namespace Product_Catalog_Web_Application.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        T SelectOne(Expression<Func<T, bool>> match);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params string[] eagrs);

        Task<T> GetByIdAsync(int id);
     
        Task<IEnumerable<T>> GetAllAsync(params string[] eagrs);

        void AddOne(T myItem);
        Task AddOneAsync(T myItem);

        void UpdateOne(T myItem);
        Task UpdateOneAsync(T myItem);
        void DeleteOne(T myItem);
        Task DeleteOneAsync(T myItem);
    }
}
