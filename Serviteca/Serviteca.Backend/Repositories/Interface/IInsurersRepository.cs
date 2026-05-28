using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface;

public interface IInsurersRepository
{
    Task<IEnumerable<Insurer>> GetComboAsync();

    Task<ActionResponse<IEnumerable<Insurer>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
}