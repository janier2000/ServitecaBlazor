using Serviteca.Shared.DTOs;
using Serviteca.Backend.Data;
using Serviteca.Backend.Helpers;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Repositories.Interface;

namespace Serviteca.Backend.Repositories.Implementations;

public class VehicleTypesRepository : GenericRepository<TypeV>, ITypesRepository
{
    private readonly DataContext _context;

    public VehicleTypesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TypeV>> GetComboAsync()
    {
        return await _context.Types.OrderBy(c => c.Name).ToListAsync();
    }

    public override async Task<ActionResponse<IEnumerable<TypeV>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Types.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<TypeV>>
        {
            WasSuccess = true,
            Result = await queryable.OrderBy(x => x.Name).Paginate(pagination)
                                                         .ToListAsync()
        };
    }

    public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
    {
        var queryable = _context.Types.AsQueryable();

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