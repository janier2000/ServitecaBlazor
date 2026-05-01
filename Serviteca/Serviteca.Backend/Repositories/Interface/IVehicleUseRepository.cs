using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface
{
    public interface IVehicleUseRepository
    {
        Task<IEnumerable<VehicleUse>> GetComboAsync();
        Task<ActionResponse<IEnumerable<VehicleUse>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}