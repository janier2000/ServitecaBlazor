using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface
{
    public interface IVehicleTypeRepository
    {
        Task<IEnumerable<VehicleType>> GetComboAsync();
        Task<ActionResponse<IEnumerable<VehicleType>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}