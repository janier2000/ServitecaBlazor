using Serviteca.Shared.DTOs;
using Serviteca.Backend.Data;
using Serviteca.Shared.Entities;
using Serviteca.Backend.Helpers;
using Serviteca.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Repositories.Interface;

namespace Serviteca.Backend.Repositories.Implementations
{
    public class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
    {
        private readonly DataContext _context;
        public CustomersRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Customer>>> GetAsync()
        {
            var customers = await _context.Customers
                                          .Include(s => s.DocumentType!)
                                          .OrderBy(x => x.FirstName)
                                          .ToListAsync();
            return new ActionResponse<IEnumerable<Customer>>
            {
                WasSuccess = true,
                Result = customers
            };
        }

        public override async Task<ActionResponse<IEnumerable<Customer>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Customers
                                  .Include(s => s.DocumentType!)
                                              .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.FirstName.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Customer>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.FirstName)
                                        .Paginate(pagination)
                                        .ToListAsync()
            };
        }

        public override async Task<ActionResponse<Customer>> GetAsync(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return new ActionResponse<Customer>
                {
                    WasSuccess = false,
                    Message = "Usuario no existe"
                };
            }

            return new ActionResponse<Customer>
            {
                WasSuccess = true,
                Result = customer
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.FirstName.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public async Task<IEnumerable<Customer>> GetComboAsync()
        {
            return await _context.Customers
                                 .OrderBy(c => c.FirstName)
                                 .ToListAsync();
        }
    }
}