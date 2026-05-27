using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface;

public interface IVehiclesRepository
{
    Task<ActionResponse<Vehicle>> CreateAsync(VehicleDTO vehicleDTO);

    Task<ActionResponse<Vehicle>> UpdateAsync(VehicleDTO vehicleDTO);

    Task<ActionResponse<Vehicle>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Vehicle>>> GetAsync();

    Task<ActionResponse<IEnumerable<Vehicle>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

    Task<IEnumerable<Vehicle>> GetComboAsync();
}