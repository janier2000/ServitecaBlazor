using Serviteca.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Serviteca.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Backend.UnitsOfWork.Interfaces;

namespace Serviteca.Backend.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class VehicleTypeController : GenericController<VehicleType>
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        public VehicleTypeController(IGenericUnitOfWork<VehicleType> unitOfWork, IVehicleTypeRepository vehicleTypeRepository) : base(unitOfWork)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _vehicleTypeRepository.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _vehicleTypeRepository.GetTotalPagesAsync(pagination);
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
            return Ok(await _vehicleTypeRepository.GetComboAsync());
        }
    }
}
