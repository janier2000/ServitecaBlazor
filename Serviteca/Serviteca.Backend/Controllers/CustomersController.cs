using Microsoft.AspNetCore.Mvc;
using Serviteca.Backend.Repositories.Implementations;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Backend.UnitsOfWork.Interfaces;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;

namespace Serviteca.Backend.Controllers;

[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class CustomersController : GenericController<Customer>
{
    private readonly ICustomersRepository _customersRepository;

    public CustomersController(IGenericUnitOfWork<Customer> unitOfWork, ICustomersRepository customersRepository) : base(unitOfWork)
    {
        _customersRepository = customersRepository;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync(CustomerDTO customerDTO)
    {
        var action = await _customersRepository.CreateAsync(customerDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpPut("Edit")]
    public async Task<IActionResult> PutAsync(CustomerDTO customerDTO)
    {
        var action = await _customersRepository.UpdateAsync(customerDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpGet("full")]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _customersRepository.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("GetByFilter")]
    public async Task<IActionResult> GetByFilterAsync(string searchText)
    {
        var response = await _customersRepository.GetByFilterAsync(searchText);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _customersRepository.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    [HttpGet("Paginated")]
    public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
    {
        var response = await _customersRepository.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("TotalRecordsPaginated")]
    public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _customersRepository.GetTotalPagesAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }

    //[AllowAnonymous] // estos sirve para colocar los metodos anonymos
    [HttpGet("combo")]
    public async Task<IActionResult> GetComboAsync()
    {
        return Ok(await _customersRepository.GetComboAsync());
    }
}