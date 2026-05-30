using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Serviteca.Shared.Responses;
using System.Text.RegularExpressions;

namespace Serviteca.Backend.Repositories.Interface;

public interface ICustomersRepository
{
    Task<ActionResponse<Customer>> CreateAsync(CustomerDTO customerDTO);

    Task<ActionResponse<Customer>> UpdateAsync(CustomerDTO customerDTO);

    Task<ActionResponse<Customer>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Customer>>> GetAsync();

    Task<ActionResponse<IEnumerable<Customer>>> GetByFilterAsync(string searchText);

    Task<ActionResponse<IEnumerable<Customer>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

    Task<IEnumerable<Customer>> GetComboAsync();
}