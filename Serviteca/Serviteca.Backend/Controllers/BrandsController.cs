using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Backend.UnitsOfWork.Interfaces;

namespace Serviteca.Backend.Controllers;

[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class BrandsController : GenericController<Brand>
{
    private readonly IBrandsRepository _brandRepository;

    public BrandsController(IGenericUnitOfWork<Brand> unitOfWork, IBrandsRepository brandRepository) : base(unitOfWork)
    {
        _brandRepository = brandRepository;
    }

    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _brandRepository.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("totalRecordsPaginated")]
    public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _brandRepository.GetTotalPagesAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }

    [AllowAnonymous]
    [HttpGet("combo")]
    public async Task<IActionResult> GetComboAsync()
    {
        return Ok(await _brandRepository.GetComboAsync());
    }
}