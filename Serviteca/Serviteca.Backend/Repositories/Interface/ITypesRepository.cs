using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface;

public interface ITypesRepository
{
    Task<IEnumerable<TypeV>> GetComboAsync();

    Task<ActionResponse<IEnumerable<TypeV>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
}