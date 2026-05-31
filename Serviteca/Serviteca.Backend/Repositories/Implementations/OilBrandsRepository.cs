using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Data;
using Serviteca.Backend.Helpers;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Implementations;

public class OilBrandsRepository : GenericRepository<OilBrand>, IOilBrandsRepository
{
    private readonly DataContext _context;

    public OilBrandsRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OilBrand>> GetComboAsync()
    {
        return await _context.OilBrands.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<ActionResponse<IEnumerable<OilBrand>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.OilBrands.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<OilBrand>>
        {
            WasSuccess = true,
            Result = await queryable.OrderBy(x => x.Name)
                                    .Paginate(pagination)
                                    .ToListAsync()
        };
    }

    public async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
    {
        var queryable = _context.OilBrands.AsQueryable();

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