using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Backend.UnitsOfWork.Interfaces;
using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;

namespace Serviteca.Backend.Controllers;

public class OilBrandsController : GenericController<OilBrand>

{
    private readonly IOilBrandsRepository _oilBrandRepository;

    public OilBrandsController(IGenericUnitOfWork<OilBrand> unitOfWork, IOilBrandsRepository oilBrandRepository) : base(unitOfWork)
    {
        _oilBrandRepository = oilBrandRepository;
    }

    [HttpGet("Paginated")]
    public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _oilBrandRepository.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("TotalRecordsPaginated")]
    public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _oilBrandRepository.GetTotalPagesAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }

    [AllowAnonymous]
    [HttpGet("Combo")]
    public async Task<IActionResult> GetComboAsync()
    {
        return Ok(await _oilBrandRepository.GetComboAsync());
    }
}