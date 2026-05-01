using Serviteca.Shared.DTOs;
using Serviteca.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Backend.UnitsOfWork.Interfaces;


namespace Serviteca.Backend.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UseVController : GenericController<VehicleUse>
    {
        private readonly IVehicleUseRepository _vehicleUseRepository;
        public UseVController(IGenericUnitOfWork<VehicleUse> unitOfWork, IVehicleUseRepository vehicleUseRepository) : base(unitOfWork)
        {
            _vehicleUseRepository = vehicleUseRepository;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _vehicleUseRepository.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _vehicleUseRepository.GetTotalPagesAsync(pagination);
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
            return Ok(await _vehicleUseRepository.GetComboAsync());
        }
    }
}