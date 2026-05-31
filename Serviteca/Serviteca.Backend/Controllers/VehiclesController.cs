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
public class VehiclesController : GenericController<Vehicle>
{
    private readonly IVehiclesRepository _vehicleRepository;

    public VehiclesController(IGenericUnitOfWork<Vehicle> unitOfWork, IVehiclesRepository customersRepository) : base(unitOfWork)
    {
        _vehicleRepository = customersRepository;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync(VehicleDTO vehicleDTO)
    {
        var action = await _vehicleRepository.CreateAsync(vehicleDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpPut("Edit")]
    public async Task<IActionResult> PutAsync(VehicleDTO vehicleDTO)
    {
        var action = await _vehicleRepository.UpdateAsync(vehicleDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpGet("Full")]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _vehicleRepository.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("Paginated")]
    public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
    {
        var response = await _vehicleRepository.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("GetByFilter")]
    public async Task<IActionResult> GetByFilterAsync(string searchText)
    {
        var response = await _vehicleRepository.GetByFilterAsync(searchText);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("TotalRecordsPaginated")]
    public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _vehicleRepository.GetTotalPagesAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _vehicleRepository.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    //[AllowAnonymous] // estos sirve para colocar los metodos anonymos
    [HttpGet("Combo")]
    public async Task<IActionResult> GetComboAsync()
    {
        return Ok(await _vehicleRepository.GetComboAsync());
    }
}