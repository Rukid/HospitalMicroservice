using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApi.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
