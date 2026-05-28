using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface;

public interface IUsesRepository
{
    Task<IEnumerable<Use>> GetComboAsync();

    Task<ActionResponse<IEnumerable<Use>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
}