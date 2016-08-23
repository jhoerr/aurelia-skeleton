using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IGenericService<TEntity, TRequest>
        where TEntity : Entity
        where TRequest : EntityRequest

    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(long id);
        Task<TEntity> AddAsync(TRequest request);
        Task<TEntity> UpdateAsync(long id, TRequest request);
        Task DeleteAsync(long id);
    }
}