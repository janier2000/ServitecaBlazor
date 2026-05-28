using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Data;
using Serviteca.Backend.Helpers;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;

namespace Serviteca.Backend.Repositories.Implementations;

public class InsurersRepository : GenericRepository<Insurer>, IInsurersRepository
{
    private readonly DataContext _context;

    public InsurersRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Insurer>> GetComboAsync()
    {
        return await _context.Insurers.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<ActionResponse<IEnumerable<Insurer>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Insurers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<Insurer>>
        {
            WasSuccess = true,
            Result = await queryable.OrderBy(x => x.Name).Paginate(pagination)
                                                         .ToListAsync()
        };
    }

    public async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
    {
        var queryable = _context.Insurers.AsQueryable();

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