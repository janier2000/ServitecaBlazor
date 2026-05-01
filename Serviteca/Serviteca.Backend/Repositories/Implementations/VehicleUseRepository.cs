using Serviteca.Shared.DTOs;
using Serviteca.Backend.Data;
using Serviteca.Shared.Entities;
using Serviteca.Backend.Helpers;
using Serviteca.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Repositories.Interface;

namespace Serviteca.Backend.Repositories.Implementations
{
    public class VehicleUseRepository : GenericRepository<VehicleUse>, IVehicleUseRepository
    {
        private readonly DataContext _context;
        public VehicleUseRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleUse>> GetComboAsync()
        {
            return await _context.VehicleUses.OrderBy(c => c.Name).ToListAsync();
        }

        public override async Task<ActionResponse<IEnumerable<VehicleUse>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.VehicleUses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<VehicleUse>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.Name).Paginate(pagination)
                                                             .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.VehicleTypes.AsQueryable();

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