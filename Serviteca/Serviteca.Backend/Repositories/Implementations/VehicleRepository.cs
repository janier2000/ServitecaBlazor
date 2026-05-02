using Serviteca.Shared.DTOs;
using Serviteca.Backend.Data;
using Serviteca.Backend.Helpers;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Repositories.Interface;

namespace Serviteca.Backend.Repositories.Implementations
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly DataContext _context;
        public VehicleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Vehicle>>> GetAsync()
        {
            var customers = await _context.Vehicles
                                          //.Include(s => s.DocumentType!)
                                          .OrderBy(x => x.Plate)
                                          .ToListAsync();
            return new ActionResponse<IEnumerable<Vehicle>>
            {
                WasSuccess = true,
                Result = customers
            };
        }

        public override async Task<ActionResponse<IEnumerable<Vehicle>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Vehicles
                                    //.Include(s => s.DocumentType!)
                                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Plate.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Vehicle>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.Plate)
                                        .Paginate(pagination)
                                        .ToListAsync()
            };
        }

        public override async Task<ActionResponse<Vehicle>> GetAsync(int id)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(c => c.Id == id);
            if (vehicle == null)
            {
                return new ActionResponse<Vehicle>
                {
                    WasSuccess = false,
                    Message = "Vehiculo no existe"
                };
            }

            return new ActionResponse<Vehicle>
            {
                WasSuccess = true,
                Result = vehicle
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Vehicles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Plate.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public async Task<IEnumerable<Vehicle>> GetComboAsync()
        {
            return await _context.Vehicles
                                 .OrderBy(c => c.Plate)
                                 .ToListAsync();
        }
    }
}
