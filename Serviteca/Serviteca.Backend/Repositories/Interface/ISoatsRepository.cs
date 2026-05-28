using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface;

public interface ISoatsRepository
{
    Task<ActionResponse<Soat>> CreateAsync(SoatDTO soatDTO);

    Task<ActionResponse<Soat>> UpdateAsync(SoatDTO soatDTO);

    Task<ActionResponse<Soat>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Soat>>> GetAsync();

    Task<ActionResponse<IEnumerable<Soat>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

    Task<IEnumerable<Soat>> GetComboAsync();
}