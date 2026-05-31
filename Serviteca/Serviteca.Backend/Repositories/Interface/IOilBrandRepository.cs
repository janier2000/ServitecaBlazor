using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface;

public interface IOilBrandRepository
{
    Task<IEnumerable<OilBrand>> GetComboAsync();

    Task<ActionResponse<IEnumerable<OilBrand>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
}