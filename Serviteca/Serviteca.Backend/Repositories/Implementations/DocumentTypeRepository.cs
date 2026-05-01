using Serviteca.Shared.DTOs;
using Serviteca.Backend.Data;
using Serviteca.Backend.Helpers;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Repositories.Interface;

namespace Serviteca.Backend.Repositories.Implementations
{
    public class DocumentTypeRepository : GenericRepository<DocumentType>, IDocumentTypeRepository
    {
        private readonly DataContext _context;
        public DocumentTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentType>> GetComboAsync()
        {
            return await _context.DocumentTypes.OrderBy(c => c.Name).ToListAsync();
        }

        public override async Task<ActionResponse<IEnumerable<DocumentType>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.DocumentTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<DocumentType>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.VehicleBrands.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

    }
}