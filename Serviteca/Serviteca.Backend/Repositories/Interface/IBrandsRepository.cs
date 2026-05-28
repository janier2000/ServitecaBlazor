using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface;

public interface IBrandsRepository
{
    Task<IEnumerable<Brand>> GetComboAsync();

    Task<ActionResponse<IEnumerable<Brand>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
}