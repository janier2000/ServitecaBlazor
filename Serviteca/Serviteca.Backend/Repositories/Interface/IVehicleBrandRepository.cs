using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface
{
    public interface IVehicleBrandRepository
    {
        Task<IEnumerable<VehicleBrand>> GetComboAsync();
        Task<ActionResponse<IEnumerable<VehicleBrand>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}