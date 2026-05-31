using Microsoft.AspNetCore.Mvc;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Backend.UnitsOfWork.Interfaces;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;

namespace Serviteca.Backend.Controllers;

[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class SoatsController : GenericController<Soat>
{
    private readonly ISoatsRepository _soatsRepository;

    public SoatsController(IGenericUnitOfWork<Soat> unitOfWork, ISoatsRepository soatsRepository) : base(unitOfWork)
    {
        _soatsRepository = soatsRepository;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PostAsync(SoatDTO soatDTO)
    {
        var action = await _soatsRepository.CreateAsync(soatDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpPut("Edit")]
    public async Task<IActionResult> PutAsync(SoatDTO soatDTO)
    {
        var action = await _soatsRepository.UpdateAsync(soatDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpGet("Full")]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _soatsRepository.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("Paginated")]
    public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
    {
        var response = await _soatsRepository.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("TotalRecordsPaginated")]
    public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _soatsRepository.GetTotalPagesAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _soatsRepository.GetAsync(id);
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
        return Ok(await _soatsRepository.GetComboAsync());
    }
}