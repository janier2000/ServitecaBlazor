using Serviteca.Shared.DTOs;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ActionResponse<T>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<T>>> GetAsync();
        Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<ActionResponse<T>> AddAsync(T entity);
        Task<ActionResponse<T>> DeleteAsync(int id);
        Task<ActionResponse<T>> UpdateAsync(T entity);
    }
}
