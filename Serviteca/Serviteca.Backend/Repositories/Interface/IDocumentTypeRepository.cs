using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Interface
{
    public interface IDocumentTypeRepository
    {
        Task<IEnumerable<DocumentType>> GetComboAsync();
        Task<ActionResponse<IEnumerable<DocumentType>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
