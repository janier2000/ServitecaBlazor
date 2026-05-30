using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Data;
using Serviteca.Backend.Helpers;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;
using System.Text.RegularExpressions;

namespace Serviteca.Backend.Repositories.Implementations;

public class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
{
    private readonly DataContext _context;

    public CustomersRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ActionResponse<Customer>> CreateAsync(CustomerDTO customerDTO)
    {
        //var admin = await _usersRepository.GetUserAsync(customerDTO.AdminId);
        //if (admin == null)
        //{
        //    return new ActionResponse<Customer>
        //    {
        //        WasSuccess = false,
        //        Message = "ERR013"// usuario no existe
        //    };
        //}

        var documentType = await _context.DocumentTypes.FindAsync(customerDTO.DocumentTypeId);
        if (documentType == null)
        {
            return new ActionResponse<Customer>
            {
                WasSuccess = false,
                Message = "Tipo de documento no existe"
            };
        }

        var customerENT = new Customer
        {
            ClientSince = customerDTO.ClientSince.ToString(),
            DocumentType = documentType,
            Document = customerDTO.Document,
            Email = customerDTO.Email,
            FirstName = customerDTO.FirstName,
            LastName = customerDTO.LastName,
            gender = customerDTO.gender,
            phone = customerDTO.phone,
        };

        _context.Add(customerENT);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Customer>
            {
                WasSuccess = true,
                Result = customerENT
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Customer>
            {
                WasSuccess = false,
                Message = "Ya existe el cliente que estas intentando crear"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Customer>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    public async Task<ActionResponse<Customer>> UpdateAsync(CustomerDTO customerDTO)
    {
        try
        {
            var customerENT = await _context.Customers.FindAsync(customerDTO.Id);
            if (customerENT == null)
            {
                return new ActionResponse<Customer>
                {
                    WasSuccess = false,
                    Message = "El Cliente no es válido."//
                };
            }

            var documentType = await _context.DocumentTypes.FindAsync(customerDTO.DocumentTypeId);
            if (documentType == null)
            {
                return new ActionResponse<Customer>
                {
                    WasSuccess = false,
                    Message = "Tipo de documento no existe"
                };
            }

            customerENT.phone = customerDTO.phone;
            customerENT.Email = customerDTO.Email;
            customerENT.FirstName = customerDTO.FirstName;
            customerENT.LastName = customerDTO.LastName;
            customerENT.gender = customerDTO.gender;
            customerENT.ClientSince = customerDTO.ClientSince.ToString();
            customerENT.DocumentType = documentType;
            customerENT.phone = customerDTO.phone;

            _context.Update(customerENT);
            await _context.SaveChangesAsync();
            return new ActionResponse<Customer>
            {
                WasSuccess = true,
                Result = customerENT
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Customer>
            {
                WasSuccess = false,
                Message = "Error al actualizar el registro"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Customer>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    public override async Task<ActionResponse<IEnumerable<Customer>>> GetAsync()
    {
        var customers = await _context.Customers.Include(s => s.DocumentType!)
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
        var queryable = _context.Customers.Include(s => s.DocumentType!).AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            string filter = pagination.Filter.ToLower();
            //nombre
            var lstCustomer = queryable.Where(x => x.FirstName.ToLower().Contains(filter) ||
                                                   x.LastName.ToLower().Contains(filter) ||
                                                   x.Document.ToLower().Contains(filter) ||
                                                   x.Email.ToLower().Contains(filter) ||
                                                   x.phone.ToLower().Contains(filter) ||
                                                   x.DocumentType!.Name.ToLower().Contains(filter));
            return new ActionResponse<IEnumerable<Customer>>
            {
                WasSuccess = true,
                Result = await lstCustomer.OrderBy(x => x.FirstName)
                                          .Paginate(pagination)
                                          .ToListAsync()
            };
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
        var customer = await _context.Customers.Include(s => s.DocumentType!)
                                               .FirstOrDefaultAsync(c => c.Id == id);
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
        return await _context.Customers.OrderBy(c => c.FirstName)
                                       .ToListAsync();
    }
}